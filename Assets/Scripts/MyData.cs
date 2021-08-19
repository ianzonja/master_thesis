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
}
