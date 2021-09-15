using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBox4UsernameUI : MonoBehaviour
{
    private string username = "";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DataManager dm = new DataManager();
        Room room = dm.GetRoom();
        if (room.Players.Length > 3)
            this.username = room.Players[3].Username;
        this.GetComponent<Text>().text = this.username;
    }
}
