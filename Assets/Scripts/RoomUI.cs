using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DataManager dm = new DataManager();
        string username = dm.GetMyUsername();
        if (username != "" && username != null)
            GameObject.Find("GameTitle").GetComponent<Text>().text = username + "'s Game";
    }

    public void OnStartGameButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnRoomLoaded()
    {
    }
}
