using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController
{
    public static Room room;

    public static bool roundStarted = false;

    public static Player player;

    public static List<Player> players = new List<Player>(){};

    public static string winner;

    private static string turn; 

    public static string Turn 
    {
        get => turn;
        set
        {
            if(turn == value) return;
            turn = value;
            HideTimers();
        }
    }


    public static int potAmmount;

    public static int minStake;


    public static void AssignControllerToPlayers(List<PlayerController> playerControllers)
    {
       Player myPlayer = players.FirstOrDefault(p => p.playerID == GameController.player.playerID);
       myPlayer.assignPlayerController(playerControllers[0]);
       myPlayer.playerController.setPlayer(myPlayer);
       myPlayer.playerController.active = true;
       myPlayer.playerController.updateUserName();

       int numberOfSeats = 5;

        int offset = (myPlayer.order + 1) % players.Count;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerID == GameController.player.playerID)
            {
                continue;
            }

            // Calculate seat number maintaining the order but starting from seat 0
            var distance = numberOfSeats - myPlayer.order;
            var SeatNumber = (players[i].order + distance) % numberOfSeats;
            players[i].assignPlayerController(playerControllers[SeatNumber]);
            players[i].playerController.active = true;
            players[i].playerController.updateUserName();
            players[i].playerController.UpdateBalance();
        }

    }

    public static void updatePlayer(Player updatedPlayer){
        Player Player = players.FirstOrDefault(p => p.playerID == updatedPlayer.playerID);
        Player.updatePlayer(updatedPlayer.balance,updatedPlayer.order,updatedPlayer.playerName,updatedPlayer.blind,updatedPlayer.fold,updatedPlayer.cards);
    }
    
    public static void UpdatePlayerTimer(int timer){
        Player turnPlayer = players.FirstOrDefault(p => p.playerID == GameController.turn);
        turnPlayer.playerController.showTimer(timer);
    }

    public static int CountAlivePlayers()
    {
        if(players != null)
        {
            return players.Count((_player)=>!_player.fold);
        }
        return 0;
    }

    static void HideTimers(){
        players.ForEach((_player)=>{
            _player.playerController.HideTimer();
        });
    }

    public static void ResetGame()
    {
        players = new List<Player>(){};
        GameController.winner = null;
    }

    public static void ShowAllCards()
    {
        players.ForEach((_player)=>{
            _player.blind = false;
            _player.playerController.showCards();
        });
    }
    public static void ShowCardsOfPerticularPlayer(List<string> playerIds)
    {
        players.ForEach((_player)=>{
            if(playerIds.Contains(_player.playerID))
            {
                _player.playerController.showCards();
            }
        });
    }

    public static void StartStopWinningAnimation(bool start)
    {
      Player winningPlayer = players.Find((_player)=>_player.playerID == winner);
      if(winningPlayer != null && winningPlayer.playerController != null)
      {
        if(start) winningPlayer.playerController.startWinAnimation();
        else winningPlayer.playerController.stopWinAnimation();
      }
    }

}
