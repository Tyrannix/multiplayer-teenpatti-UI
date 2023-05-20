using System.Collections.Generic;
using System;

[Serializable]
public class Game
{
    public string roundId;
    
    public int pot;
    public int turn_order_no;

    public int minStake;

    public List<Player> players;

}