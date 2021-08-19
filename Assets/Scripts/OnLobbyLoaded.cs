using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLobbyLoaded : MonoBehaviour
{
    public UnityEngine.UI.Text Text;
    private void Update()
    {
        Debug.Log("u lobbyu same");
        Debug.Log("id: ");
        Debug.Log(MyData.onlinePlayers.MyId);
        Debug.Log("Ukupno igraca");
        Debug.Log(MyData.onlinePlayers.TotalPlayers);
        Debug.Log("Spojeni igraci");
        Debug.Log(MyData.onlinePlayers.ConnectedClients);
    }
}
