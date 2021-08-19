using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoginManager : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("Login");
    }

    public void OnJoinButtonClicked()
    {
        SceneManager.LoadScene("Lobby");
    }
}
