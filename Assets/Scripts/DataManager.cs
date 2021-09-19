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
            return MyData.Instance.InGamePlayers;
        }
    }

    public void SetAllPlayers(List<PlayerData> players)
    {
        lock (_Lock)
        {
            MyData.Instance.InGamePlayers = players;
        }
    }

    public string GetMyUsername()
    {
        lock (_Lock)
        {
            return MyData.Instance.Username;
        }
    }

    public void SetMyUsername(string username)
    {
        lock (_Lock)
        {
            MyData.Instance.Username = username;
        }
    }

    public string GetMyPlayfabId()
    {
        lock (_Lock)
        {
            return MyData.Instance.MyPlayfabId;
        }
    }

    public void SetMyPlayfabId(string playfabId)
    {
        lock (_Lock)
        {
            MyData.Instance.MyPlayfabId = playfabId;
        }
    }

    public bool IsLoggedIn()
    {
        lock (_Lock)
        {
            return MyData.Instance.IsLoggedIn;
        }
    }

    public void SetPlayerLoggedIn(bool loggedInStatus)
    {
        lock (_Lock)
        {
            MyData.Instance.IsLoggedIn = loggedInStatus;
        }
    }

    public void SetMySessionTicket(string sessionTicket)
    {
        lock (_Lock)
        {
            MyData.Instance.SessionTicket = sessionTicket;
        }
    }

    public string GetMySessionTicket()
    {
        lock (_Lock)
        {
            return MyData.Instance.SessionTicket;
        }
    }

    public void SetRoom(Room room)
    {
        lock (_Lock)
        {
            MyData.Instance.Room = room;
        }
    }

    public Room GetRoom()
    {
        lock (_Lock)
        {
            return MyData.Instance.Room;
        }
    }

    public void SetLobbyRooms(List<Room> rooms)
    {
        lock (_Lock)
        {
            MyData.Instance.LobbyRooms = rooms;
        }
    }

    public List<Room> GetLobbyRooms()
    {
        lock (_Lock)
        {
            return MyData.Instance.LobbyRooms;
        }
    }

    public PlayerData GetRoomPlayerByPlayfabId(string playfabId)
    {
        PlayerData player = null;
        lock (_Lock)
        {
            PlayerData[] players = MyData.Instance.Room.Players;
            for(int i=0; i<players.Length; i++)
            {
                if (players[i].PlayfabId == playfabId)
                    player = players[i];
            }
        }
        if (player != null)
            return player;
        else
        {
            Debug.Log("Greska! Nisam pronasao igraca sa tim playfabIdem");
            return player;
        }


    }

    public void SetJwt(string token)
    {
        lock (_Lock)
        {
            MyData.Instance.Jwt = token;
        }
    }

    public string GetJwt()
    {
        lock (_Lock)
        {
            return MyData.Instance.Jwt;
        }
    }
}