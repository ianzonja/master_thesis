using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBox1UsernameUI : MonoBehaviour
{
    private string username = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this != null)
        {
            DataManager dm = new DataManager();
            Room room = dm.GetRoom();
            if (room.Players.Length > 0)
                this.username = room.Players[0].Username;
            this.GetComponent<Text>().text = this.username;
        }
    }
}
