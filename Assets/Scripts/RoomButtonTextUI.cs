using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButtonTextUI : MonoBehaviour
{
    private string buttonText = "";
    // Start is called before the first frame update
    void Start()
    {
        DataManager dm = new DataManager();
        Room room = dm.GetRoom();
        string playfabId = dm.GetMyPlayfabId();
        if (playfabId == room.HostPlayfabId)
        {
            this.GetComponent<Text>().color = Color.red;
            this.GetComponent<Text>().text = "Start";
        }
        else
        {
            this.GetComponent<Text>().color = Color.red;
            this.GetComponent<Text>().text = "Ready";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
