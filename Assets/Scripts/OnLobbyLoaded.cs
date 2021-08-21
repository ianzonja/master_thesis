using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnLobbyLoaded : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    bool userLogged = false;
    private void Update()
    {
        if (userLogged)
        {
            //Debug.Log("u lobbyu same");
            //Debug.Log("id: ");
            //Debug.Log(MyData.onlinePlayers.MyId);
            //Debug.Log("Ukupno igraca");
            //Debug.Log(MyData.onlinePlayers.TotalPlayers);
            //Debug.Log("Spojeni igraci");
            //Debug.Log(MyData.onlinePlayers.ConnectedClients);
        }
    }

    public void OnCreateRoomButtonClick()
    {
        Debug.Log("Kreiram sobu");
        SceneManager.LoadScene("RoomSetup");
    }
}
