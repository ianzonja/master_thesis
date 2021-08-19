using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedClient
{
    public string Id;
    public string Username;
}
public class OnlinePlayers
{
    public string TotalPlayers { get; set; }
    public string MyId { get; set; }
    public List<ConnectedClient> ConnectedClients { get; set; }
}
