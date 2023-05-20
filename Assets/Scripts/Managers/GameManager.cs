using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI roomIdText;

    [SerializeField]
    List<PlayerController> gamePlayers;

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
    }


    void OnRoomUpdate(string data){
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
        }
    }

    void OnDecisionTimer(string data)
    {
        SocketResponse<Timer> parsedData = JsonUtility.FromJson<SocketResponse<Timer>>(data);

        if(parsedData.success){
            Debug.Log($"{parsedData.data.timer} {parsedData.data.turn}");
            GameController.Turn = parsedData.data.turn;
            GameController.UpdatePlayerTimer(parsedData.data.timer);
        }
    }

    



    //Sending socket emits
    void GetRoomUpdate(){
        WebSocketManager.instance.EmitEvent("get-room-update","");
    }






}
