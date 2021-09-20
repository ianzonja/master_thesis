using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string Wins { get; set; }
    public string Loses { get; set; }
    public string Draws { get; set; }

    public string PlayfabId { get; set; }

    public string Username { get; set; }

    public string InGameStatus { get; set; }

    public string Avatar { get; set; }

    public CardData[] Hand { get; set; }
}
