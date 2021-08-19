using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;
using Newtonsoft.Json;


public static class ServerManagement
{
    private static List<Client> clients = new List<Client>();
    private static int TotalPlayers = 0;

    public static void AddConnectedClient(Client client)
    {
        clients.Add(client);
        TotalPlayers += 1;
    }

    public static string GetServerStatusJsonString(Guid clientId)
    {
        string clientsJson = JsonConvert.SerializeObject(clients);
        return "{\"TotalPlayers\": \"" + TotalPlayers + "\", \"MyId\": \"" + clientId.ToString() + "\", \"ConnectedClients\": " + clientsJson + "}";
    }
}

public class Client
{
    public Guid Id { get; set; }
    public string Username { get; set; }
}
