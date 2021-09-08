using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class MyData
{
    public static string Username { get; set; }
    
    public static bool IsLoggedIn { get; set; }
    public static bool IsDealer { get; set; }
    public static bool IsNewRound { get; set; }
    public static bool CardsDealt { get; set; }
    
    public static string SessionTicket { get; internal set; }

    public static string MyPlayfabId { get; set; }

    public static List<PlayerData> InGamePlayers { get; set; }

    public static Room Room { get; set; }

    public static List<Room> LobbyRooms { get; set; }
}

