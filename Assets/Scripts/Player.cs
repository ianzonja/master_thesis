using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Mirror;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Linq;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public int totalPlayers = 0;
    private Boolean newPlayerConnected = true;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Debug.Log("Novi igrac connected");
        Debug.Log("Moj id je: ");
        Debug.Log(MyData.id.ToString());
        if (MyData.id == Guid.Empty)
        {
            Debug.Log("Kreiram mu id");
            MyData.id = Guid.NewGuid();
        }
        if (!MyData.IsServer)
            Hola(MyData.id);
    }

    // Update is called once per frame
    void Update()
    {
        if (!MyData.IsServer && MyData.IsDealer)
            DealCardsCommand();
    }

    [ClientRpc]
    private void DealCardsCommand()
    {
        for(int i=0; i<10; i++)
        {

        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Server has been started!");
    }

    [Command]
    void Hola(Guid clientId)
    {
        Debug.Log("Received Hola from Client!");
        Debug.Log("Client id: " + clientId.ToString());
        Client client = new Client();
        client.Id = clientId;
        client.Username = "client_" + client.Id.ToString();
        ServerManagement.AddConnectedClient(client);
        ReplyHola(ServerManagement.GetServerStatusJsonString(clientId));
    }

    [ClientRpc]
    void ReplyHola(String jsonString)
    {
        Debug.Log("Reply Hola from Server!");
        OnlinePlayers op = JsonConvert.DeserializeObject<OnlinePlayers>(jsonString);
        Debug.Log(op.MyId);
        MyData.onlinePlayers = op;
        if(SceneManager.GetActiveScene().name == "Start")
        {
            Debug.Log("Idem na Login");
            SceneManager.LoadScene("Lobby");
        }
    }
}
