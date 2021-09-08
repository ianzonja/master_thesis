using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private readonly Object _Lock = new Object();

    public List<PlayerData> GetAllPlayers()
    {
        lock (_Lock)
        {
            return MyData.InGamePlayers;
        }
    }

    public void SetAllPlayers(List<PlayerData> players)
    {
        lock (_Lock)
        {
            MyData.InGamePlayers = players;
        }
    }

    public string GetMyUsername()
    {
        lock (_Lock)
        {
            return MyData.Username;
        }
    }

    public void SetMyUsername(string username)
    {
        lock (_Lock)
        {
            MyData.Username = username;
        }
    }

    public string GetMyPlayfabId()
    {
        lock (_Lock)
        {
            return MyData.MyPlayfabId;
        }
    }

    public void SetMyPlayfabId(string playfabId)
    {
        lock (_Lock)
        {
            MyData.MyPlayfabId = playfabId;
        }
    }

    public bool IsLoggedIn()
    {
        lock (_Lock)
        {
            return MyData.IsLoggedIn;
        }
    }

    public void SetPlayerLoggedIn(bool loggedInStatus)
    {
        lock (_Lock)
        {
            MyData.IsLoggedIn = loggedInStatus;
        }
    }

    public void SetMySessionTicket(string sessionTicket)
    {
        lock (_Lock)
        {
            MyData.SessionTicket = sessionTicket;
        }
    }

    public string GetMySessionTicket()
    {
        lock (_Lock)
        {
            return MyData.SessionTicket;
        }
    }

    public void SetRoom(Room room)
    {
        lock (_Lock)
        {
            MyData.Room = room;
        }
    }

    public Room GetRoom()
    {
        lock (_Lock)
        {
            return MyData.Room;
        }
    }

    public void SetLobbyRooms(List<Room> rooms)
    {
        lock (_Lock)
        {
            MyData.LobbyRooms = rooms;
        }
    }

    public List<Room> GetLobbyRooms()
    {
        lock (_Lock)
        {
            return MyData.LobbyRooms;
        }
    }
}