using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomGameTitleUI : MonoBehaviour
{
    private string roomTitle = "Game";
    // Start is called before the first frame update
    void Start()
    {
        string hostId = new DataManager().GetRoom().HostPlayfabId;
        PlayerData player = new DataManager().GetRoomPlayerByPlayfabId(hostId);
        if (player != null)
            this.roomTitle = player.Username + "'s Game";
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = this.roomTitle;
    }
}
