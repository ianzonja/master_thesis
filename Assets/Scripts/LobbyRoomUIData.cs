using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Newtonsoft.Json;

public class JoinLobbyRoomResponse
{
    public string ResponseId { get; set; }
    public Room MyData { get; set; }
}

public class LobbyRoomUIData : MonoBehaviour
{
    public Sprite OccupiedSeat;
    public Sprite EmptySeat;
    private Room room;
    public Guid RoomId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoomUIData(Room room)
    {
        this.room = room;
        for(int i=0; i<4; i++)
        {
            if(i == 0)
            {
                GameObject seat1 = GameObject.Find("RoomSpace_1");
                if (room.Players.Length > i)
                    seat1.GetComponent<Image>().sprite = OccupiedSeat;
                else
                    seat1.GetComponent<Image>().sprite = EmptySeat;
            }
            else if(i == 1)
            {
                GameObject seat2 = GameObject.Find("RoomSpace_2");
                if (room.Players.Length > i)
                    seat2.GetComponent<Image>().sprite = OccupiedSeat;
                else
                    seat2.GetComponent<Image>().sprite = EmptySeat;
            }
            else if (i == 2)
            {
                GameObject seat3 = GameObject.Find("RoomSpace_3");
                if (room.Players.Length > i)
                    seat3.GetComponent<Image>().sprite = OccupiedSeat;
                else
                    seat3.GetComponent<Image>().sprite = EmptySeat;
            }
            else if (i == 3)
            {
                GameObject seat4 = GameObject.Find("RoomSpace_4");
                if (room.Players.Length > i)
                    seat4.GetComponent<Image>().sprite = OccupiedSeat;
                else
                    seat4.GetComponent<Image>().sprite = EmptySeat;
            }
        }
        GameObject seatsTaken = GameObject.Find("SeatsTaken");
        seatsTaken.GetComponent<Text>().text = "Seats available: " + (4 - room.Players.Length) + "/4";
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            TcpKlijent klijent = new TcpKlijent();
            klijent.PosaljiServeru("{\"commandId\":\"YOIWANNAJOIN\", \"sessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"roomId\": \""+room.Id+"\"}");
            string odgovor = klijent.PrimiOdServera();
            Debug.Log(odgovor);
            JoinLobbyRoomResponse response = JsonConvert.DeserializeObject<JoinLobbyRoomResponse>(odgovor);
            DataManager dm = new DataManager();
            dm.SetRoom(response.MyData);
            SceneManager.LoadScene("Room");
        });
    }
}
