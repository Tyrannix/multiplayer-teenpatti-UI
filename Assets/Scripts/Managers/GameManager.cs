using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI roomIdText;
    
    [SerializeField]
    TextMeshPro potAmount;

    [SerializeField]
    List<PlayerController> gamePlayers;

    [Serializable]
    public class TempRes
    {
        public string turn;
        public string sideShowRecTurn;
        public Game gameObj;
        public string decision;
        public string type;
        public string response;
        public string playerId;
    }

    public struct GameRoom
    {
        public string roomCode;
    }

    public struct PlayerDecision
    {
        public string decision;
        public string type;

        public string response;
    }

    // Start is called before the first frame update
    void Start()
    {
        initGame();
        ListenAllEvents();
        GetRoomUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initGame(){
        roomIdText.SetText($"Room ID : {GameController.room.roomCode}");
    }


    // Listing socket events
    void ListenAllEvents()
    {
        WebSocketManager.instance.ListenEvent("room-update",OnRoomUpdate);
        WebSocketManager.instance.ListenEvent("decision-timer",OnDecisionTimer);
        WebSocketManager.instance.ListenEvent("game-state",OnGameState);
        WebSocketManager.instance.ListenEvent("cards-seen",OnCardSeen);
        WebSocketManager.instance.ListenEvent("player-decision",OnPlayerAction);
        WebSocketManager.instance.ListenEvent("round-winner",OnWinner);
    }


    void OnRoomUpdate(string data)
    {
        Debug.Log("-----------------------------------Room Update---------------------------------------");
        Debug.Log(data);
        SocketResponse<Room> parsedData = JsonUtility.FromJson<SocketResponse<Room>>(data);
        parsedData.data.players.ForEach((_player)=>{
            Debug.Log($"playerID : {_player.userId} , order : {_player.order} , balance : {_player.balance}");
            Player p = GameController.players.Find((player)=>player.playerID == _player.userId);
            if(p != null)
            {
                p.updatePlayer(_player.balance,_player.order);
                if(p.playerID == GameController.player.playerID){
                    GameController.player.updatePlayer(_player.balance,_player.order);
                }
            }
            else
            {
                GameController.players.Add(new Player(_player.userId,_player.balance,_player.order));
            }
        });
        GameController.AssignControllerToPlayers(gamePlayers);

    }


    void OnGameState(string data)
    {
        Debug.Log(data);
        SocketResponse<Game> parsedData = JsonUtility.FromJson<SocketResponse<Game>>(data);
        if(parsedData.success){
            Game game = parsedData.data;
            game.players.ForEach((_player)=>{
                GameController.updatePlayer(_player);
            });
            GameController.potAmmount = game.pot;
            GameController.minStake = game.minStake;
            GameController.Turn = "";
            potAmount.SetText($"{GameController.potAmmount}");
            GameController.StartStopWinningAnimation(false);
            GameController.winner = null;
        }
    }

    void OnDecisionTimer(string data)
    {
        SocketResponse<Timer> parsedData = JsonUtility.FromJson<SocketResponse<Timer>>(data);

        if(parsedData.success){
            // Debug.Log($"{parsedData.data.timer} {parsedData.data.turn}");
            GameController.Turn = parsedData.data.turn;
            GameController.UpdatePlayerTimer(parsedData.data.timer);
        }
    }

    
    void OnCardSeen(string data)
    {
        Debug.Log("---------------------------- Card Seen --------------------------------------");
        Debug.Log(data);
        
        
        SocketResponse<TempRes> parsedData = JsonUtility.FromJson<SocketResponse<TempRes>>(data);

        if(parsedData.success){
            Game game = parsedData.data.gameObj;
             game.players.ForEach((_player)=>{
                GameController.updatePlayer(_player);
            });
            GameController.potAmmount = game.pot;
            potAmount.SetText($"{GameController.potAmmount}");
            GameController.minStake = game.minStake;
        }
    }

    void OnPlayerAction(string data)
    {
        Debug.Log("---------------------------- Player Action --------------------------------------");
        Debug.Log(data);
        
        
        SocketResponse<TempRes> parsedData = JsonUtility.FromJson<SocketResponse<TempRes>>(data);
        Debug.Log(parsedData.data.decision);

        if(parsedData.success){
            Game game = parsedData.data.gameObj;
             game.players.ForEach((_player)=>{
                GameController.updatePlayer(_player);
            });
            GameController.potAmmount = game.pot;
            potAmount.SetText($"{GameController.potAmmount}");
            GameController.minStake = game.minStake;
            if(parsedData.data.decision == "SIDESHOW" && parsedData.data.type == "REQUEST")
            {
                if(parsedData.data.sideShowRecTurn == GameController.player.playerID)
                {
                    UIController.instance.modal.ShowModal($"{parsedData.data.turn} requested side show do you want to accept?",AcceptSideShow,DeclineSideShow);
                }
                else
                {
                    UIController.instance.showToast($"{parsedData.data.turn} requested side show to {parsedData.data.sideShowRecTurn}");
                }
            }
            else if(parsedData.data.decision == "SIDESHOW" && parsedData.data.type == "RESPONSE")
            {
                if(parsedData.data.response == "YES")
                {
                    GameController.ShowCardsOfPerticularPlayer(new List<string>(){parsedData.data.turn,parsedData.data.sideShowRecTurn});
                    UIController.instance.showToast($"{parsedData.data.sideShowRecTurn} accepted the request of side show.");
                }
                else
                {
                    UIController.instance.showToast($"{parsedData.data.sideShowRecTurn} declined the request of side show.");
                }
            }
            else if(parsedData.data.decision == "SHOW")
            {
                GameController.ShowAllCards();
            }
        }
    }


    void OnWinner(string data)
    {
        Debug.Log("---------------------------- Winner --------------------------------------");
        Debug.Log(data);
        
        
        SocketResponse<TempRes> parsedData = JsonUtility.FromJson<SocketResponse<TempRes>>(data);
        if(parsedData.success)
        {
            Game game = parsedData.data.gameObj;
             game.players.ForEach((_player)=>{
                GameController.updatePlayer(_player);
            });
            GameController.potAmmount = game.pot;
            potAmount.SetText($"{GameController.potAmmount}");
            GameController.minStake = game.minStake;
            GameController.winner = parsedData.data.playerId;
            GameController.Turn = "";
            GameController.StartStopWinningAnimation(true);
        }
    }

    //Sending socket emits

    public void StartGame()
    {
        GameRoom gameRoom = new GameRoom(){
            roomCode = GameController.room.roomCode
        };
        WebSocketManager.instance.EmitEvent("start-game",gameRoom);
    }

    void GetRoomUpdate()
    {
        WebSocketManager.instance.EmitEvent("get-room-update","");
    }

    public void SeeCards()
    {
        WebSocketManager.instance.EmitEvent("see-cards","");
    }

    public void Chaal()
    {
        PlayerDecision playerDecision = new PlayerDecision(){
            decision = "CHAAL",
            type = "x",
        };
        WebSocketManager.instance.EmitEvent("player-decision",playerDecision);
    }

    public void Chaal2X()
    {
        PlayerDecision playerDecision = new PlayerDecision(){
            decision = "CHAAL",
            type = "2x",
        };
        WebSocketManager.instance.EmitEvent("player-decision",playerDecision);
    }

    public void Pack()
    {
        PlayerDecision playerDecision = new PlayerDecision(){
            decision = "PACK",
            type = "",
        };
        WebSocketManager.instance.EmitEvent("player-decision",playerDecision);
    }

    public void Show()
    {
        PlayerDecision playerDecision = new PlayerDecision(){
            decision = "SHOW",
            type = "",
        };
        WebSocketManager.instance.EmitEvent("player-decision",playerDecision);
    }

    public void SideShowRequest()
    {
        PlayerDecision playerDecision = new PlayerDecision(){
            decision = "SIDESHOW",
            type = "REQUEST",
        };
        WebSocketManager.instance.EmitEvent("player-decision",playerDecision);
    }

    public void AcceptSideShow()
    {
        PlayerDecision playerDecision = new PlayerDecision(){
            decision = "SIDESHOW",
            type = "RESPONSE",
            response = "YES"
        };
        WebSocketManager.instance.EmitEvent("player-decision",playerDecision);
    }

   public void DeclineSideShow()
    {
        PlayerDecision playerDecision = new PlayerDecision(){
            decision = "SIDESHOW",
            type = "RESPONSE",
            response = "NO"
        };
        WebSocketManager.instance.EmitEvent("player-decision",playerDecision);
    }

    public void ExitGame()
    {
        GameController.ResetGame();
        SceneManager.LoadScene("MainMenu");
    }

}
