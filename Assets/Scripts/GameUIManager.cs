using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject chatBox;
    // Start is called before the first frame update
    void Start()
    {
        this.chatBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSettingsButtonClick()
    {
        Debug.Log("Settings klik");
    }
    
    public void OnChatButtonClick()
    {
        Debug.Log("Chat klik");
        if (!this.chatBox.activeInHierarchy)
            this.chatBox.SetActive(true);
        else
            this.chatBox.SetActive(false);
    }

    public void OnScoreboardButtonClick()
    {
        Debug.Log("Scoreboard klik");
    }
}
