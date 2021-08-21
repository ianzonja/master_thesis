using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class MyData
{
    public static string username { get; set; }
    public static Guid id = Guid.Empty;
    public static List<Client> lobbyOpponents = new List<Client>();
    public static OnlinePlayers onlinePlayers { get; set; }
    public static bool IsIngame { get; set; }
    public static bool IsDealer { get; set; }
    public static bool IsNewRound { get; set; }
    public static bool CardsDealt { get; set; }

    public static bool IsServer { get; set; }
    public static List<CardData> PlayerOneCards { get; set; }

    public static List<CardData> PlayerTwoCards { get; set; }

    public static List<CardData> PlayerThreeCards { get; set; }

    public static List<CardData> PlayerFourCards { get; set; }
}

