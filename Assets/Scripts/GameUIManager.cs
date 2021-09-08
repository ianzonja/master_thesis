using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void OnScoreboardButtonClick()
    {
        Debug.Log("Scoreboard klik");
    }
}
