using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyData
{
    private static MyData instance = null;
    private static readonly object padlock = new object();

    private MyData()
    {
    }

    public static MyData Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MyData();
                }
                return instance;
            }
        }
    }

    public string Username { get; set; }

    public string Jwt { get; set; }

    public bool IsGameHost { get; set; }

    public bool IsLoggedIn { get; set; }
    public bool IsDealer { get; set; }
    public bool IsNewRound { get; set; }
    public bool CardsDealt { get; set; }

    public string SessionTicket { get; internal set; }

    public string MyPlayfabId { get; set; }

    public List<PlayerData> InGamePlayers { get; set; }

    public Room Room { get; set; }

    public List<Room> LobbyRooms { get; set; }

    public Game Game { get; set; }

    public bool HandEmpty { get; set; }

    public string IngameStatus { get; set; }
}

