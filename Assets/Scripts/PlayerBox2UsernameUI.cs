using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBox2UsernameUI : MonoBehaviour
{
    private string username = "";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            DataManager dm = new DataManager();
            Room room = dm.GetRoom();
            if (room.Players.Length > 1)
                this.username = room.Players[1].Username;
            this.GetComponent<Text>().text = this.username;
        }
    }
}
