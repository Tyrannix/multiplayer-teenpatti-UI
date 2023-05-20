
using System;

[Serializable]
public class Player
{
    public string playerID;

    public int balance;

    public int order;

    public string playerName;

    public bool blind;
    public bool fold;

    public string[] cards;



    public PlayerController playerController;


    public Player(string playerID,int balance)
    {
        this.playerID = playerID;
        this.balance = balance;
    }
    public Player(string playerID,int balance,int order)
    {
        this.playerID = playerID;
        this.balance = balance;
        this.order = order;
    }

    public void assignPlayerController(PlayerController playerController){
        this.playerController = playerController;
    }
    public void updatePlayer(int balance,int order)
    {
        this.balance = balance;
        this.order = order;
    }
    public void updatePlayer(int balance,int order,string playerName,bool blind,bool fold,string[] cards)
    {
        this.balance = balance;
        this.order = order;
        this.playerName = playerName;
        this.blind = blind;
        this.fold = fold;
        this.cards = cards;

        playerController.EnableCards();
    }
    

    
}