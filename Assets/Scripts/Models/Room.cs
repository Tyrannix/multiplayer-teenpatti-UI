using System.Collections.Generic;
using System;

[Serializable]
public class Room
{
    public string public_id;
    public string roomCode;

    public List<RoomPlayer> players;

    public int maxPlayers;
    public string createdBy;

}

[Serializable]
public class RoomPlayer {
   public string userId;
   
   public int balance;

   public int order;
}