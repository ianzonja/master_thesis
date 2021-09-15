using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomUIResponse
{
    public string ResponseId { get; set; }
    public Room MyData { get; set; }
}

public class RoomUI : MonoBehaviour
{
    public GameObject playerBox1;
    public GameObject playerBox2;
    public GameObject playerBox3;
    public GameObject playerBox4;
    public GameObject readyStartButtonText;
    public GameObject playerStatus1;
    public GameObject playerStatus2;
    public GameObject playerStatus3;
    public GameObject playerStatus4;
    public Sprite PlayerReady;
    public Sprite PlayerNotReady;
    private int frameCounter = 0;
    private Room room;
    private bool roomUpdated = false;
    private bool playerInRoom = false;
    // Start is called before the first frame update
    void Start()
    {
        this.room = new DataManager().GetRoom();
    }

    public bool RoomDataChanged(Room newRoom)
    {
        var room1Json = JsonConvert.SerializeObject(this.room);
        var room2Json = JsonConvert.SerializeObject(newRoom);
        if (room1Json != room2Json)
            return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        this.playerInRoom = false;
        for(int i=0; i<this.room.Players.Length; i++)
        {
            if (this.room.Players[i].PlayfabId == new DataManager().GetMyPlayfabId())
                this.playerInRoom = true;
        }
        if (!playerInRoom)
            SceneManager.LoadScene("Lobby");
        frameCounter++;
        if (this.roomUpdated)
        {
            this.room = new DataManager().GetRoom();
            if (this.room != null)
                this.roomUpdated = false;
        }
        if(frameCounter % 120 == 0)
        {
            TcpKlijent klijent = new TcpKlijent();
            klijent.PosaljiServeru("{\"CommandId\":\"YOIMINROOM\", \"SessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"RoomID\": \"" + this.room.Id + "\", \"PlayfabId\": \""+MyData.Instance.MyPlayfabId+"\"}");
            string odgovor = klijent.PrimiOdServera();
            Debug.Log(odgovor);
            if(!odgovor.Contains("Netocni podaci"))
            {
                RoomUIResponse response = JsonConvert.DeserializeObject<RoomUIResponse>(odgovor);
                DataManager dm = new DataManager();
                if (response != null)
                {
                    if (response.MyData != null)
                    {
                        if (this.RoomDataChanged(response.MyData))
                        {
                            dm.SetRoom(response.MyData);
                            this.roomUpdated = true;
                        }
                    }
                }
            }
        }
        for(int i=0; i<this.room.Players.Length; i++)
        {
            if(i == 0)
            {
                if(this.playerBox1 != null)
                {
                    if (!this.playerBox1.activeSelf)
                        this.playerBox1.SetActive(true);
                    if (this.room.Players[i].InGameStatus == "READY")
                        this.playerStatus1.GetComponent<Image>().sprite = this.PlayerReady;
                    else
                        this.playerStatus1.GetComponent<Image>().sprite = this.PlayerNotReady;
                    if (room.Players.Length == i + 1)
                        DisableEmptyBoxesIfEnabled(room.Players.Length);
                }
            }
            else if(i == 1)
            {
                if (!this.playerBox2.activeSelf)
                    this.playerBox2.SetActive(true);
                if (this.room.Players[i].InGameStatus == "READY")
                    this.playerStatus2.GetComponent<Image>().sprite = this.PlayerReady;
                else
                    this.playerStatus2.GetComponent<Image>().sprite = this.PlayerNotReady;
                if (room.Players.Length == i + 1)
                    DisableEmptyBoxesIfEnabled(room.Players.Length);
            }
            else if(i == 2)
            {
                if (!this.playerBox3.activeSelf)
                    this.playerBox3.SetActive(true);
                if (this.room.Players[i].InGameStatus == "READY")
                    this.playerStatus3.GetComponent<Image>().sprite = this.PlayerReady;
                else
                    this.playerStatus3.GetComponent<Image>().sprite = this.PlayerNotReady;
                if (room.Players.Length == i + 1)
                    DisableEmptyBoxesIfEnabled(room.Players.Length);
            }
            else
            {
                if (!this.playerBox4.activeSelf)
                    this.playerBox4.SetActive(true);
                if (room.Players.Length == i + 1)
                    DisableEmptyBoxesIfEnabled(room.Players.Length);
                if (this.room.Players[i].InGameStatus == "READY")
                    this.playerStatus4.GetComponent<Image>().sprite = this.PlayerReady;
                else
                    this.playerStatus4.GetComponent<Image>().sprite = this.PlayerNotReady;
            }
        }
    }

    public void DisableEmptyBoxesIfEnabled(int numberOfPlayers)
    {
        if(numberOfPlayers == 1)
        {
            if (this.playerBox2.activeSelf)
                this.playerBox2.SetActive(false);
            if (this.playerBox3.activeSelf)
                this.playerBox3.SetActive(false);
            if (this.playerBox4.activeSelf)
                this.playerBox4.SetActive(false);
        }
        else if(numberOfPlayers == 2)
        {
            if (this.playerBox3.activeSelf)
                this.playerBox3.SetActive(false);
            if (this.playerBox4.activeSelf)
                this.playerBox4.SetActive(false);
        }
        else if(numberOfPlayers == 3)
        {
            if (this.playerBox4.activeSelf)
                this.playerBox4.SetActive(false);
        }
    }

    public void OnStartGameButtonClick()
    {
        if(this.readyStartButtonText.GetComponent<Text>().text == "Ready")
        {
            TcpKlijent klijent = new TcpKlijent();
            klijent.PosaljiServeru("{\"commandId\":\"YOIMREADY\", \"sessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"playfabId\":\"" + new DataManager().GetMyPlayfabId() + "\", \"roomId\":\"" + new DataManager().GetRoom().Id + "\"}");
            string odgovor = klijent.PrimiOdServera();
            RoomUIResponse response = JsonConvert.DeserializeObject<RoomUIResponse>(odgovor);
            if(response.ResponseId == "OK")
            {
                this.readyStartButtonText.GetComponent<Text>().color = Color.green;
                this.room = response.MyData;
            }

        }
        if (this.readyStartButtonText.GetComponent<Text>().text == "Start")
        {
            TcpKlijent klijent = new TcpKlijent();
            klijent.PosaljiServeru("{\"commandId\":\"YOSTARTGAME\", \"sessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"playfabId\":\"" + new DataManager().GetMyPlayfabId() + "\", \"roomId\":\"" + new DataManager().GetRoom().Id + "\"}");
            string odgovor = klijent.PrimiOdServera();
            RoomUIResponse response = JsonConvert.DeserializeObject<RoomUIResponse>(odgovor);
            if (response.ResponseId == "OK")
            {
                this.readyStartButtonText.GetComponent<Text>().color = Color.green;
                this.room = response.MyData;
                DataManager dm = new DataManager();
                dm.SetRoom(this.room);
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void OnKickPlayer2ButtonClick()
    {
        DataManager dm = new DataManager();
        if (this.room.HostPlayfabId == dm.GetMyPlayfabId())
        {
            string kickedPlayerId = this.room.Players[1].PlayfabId;
            TcpKlijent klijent = new TcpKlijent();
            string command = "{\"CommandId\":\"YOIMKICKIN\", \"SessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"RoomID\": \"" + this.room.Id + "\", \"PlayfabId\": \"" + dm.GetMyPlayfabId() + "\", \"KickedPlayerId\":\"" + kickedPlayerId + "\"}";
            klijent.PosaljiServeru(command);
            string odgovor = klijent.PrimiOdServera();
            RoomUIResponse response = JsonConvert.DeserializeObject<RoomUIResponse>(odgovor);
            if (response.ResponseId == "OK")
            {
                dm.SetRoom(response.MyData);
                this.room = response.MyData;
            }
        }
    }

    public void OnKickPlayer3ButtonClick()
    {
        DataManager dm = new DataManager();
        if (this.room.HostPlayfabId == dm.GetMyPlayfabId())
        {
            string kickedPlayerId = this.room.Players[3].PlayfabId;
            TcpKlijent klijent = new TcpKlijent();
            klijent.PosaljiServeru("{\"CommandId\":\"YOIMKICKIN\", \"SessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"RoomID\": \"" + this.room.Id + "\", \"PlayfabId\": \"" + dm.GetMyPlayfabId() + "\", \"KickedPlayerId\":\"" + kickedPlayerId + "\"}");
            string odgovor = klijent.PrimiOdServera();
            RoomUIResponse response = JsonConvert.DeserializeObject<RoomUIResponse>(odgovor);
            if (response.ResponseId == "OK")
            {
                dm.SetRoom(response.MyData);
                this.room = response.MyData;
            }
        }
    }

    public void OnKickPlayer4ButtonClick()
    {
        DataManager dm = new DataManager();
        if (this.room.HostPlayfabId == dm.GetMyPlayfabId())
        {
            string kickedPlayerId = this.room.Players[3].PlayfabId;
            TcpKlijent klijent = new TcpKlijent();
            klijent.PosaljiServeru("{\"CommandId\":\"YOIMKICKIN\", \"SessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"RoomID\": \"" + this.room.Id + "\", \"PlayfabId\": \"" + dm.GetMyPlayfabId() + "\", \"KickedPlayerId\":\"" + kickedPlayerId + "\"}");
            string odgovor = klijent.PrimiOdServera();
            RoomUIResponse response = JsonConvert.DeserializeObject<RoomUIResponse>(odgovor);
            if (response.ResponseId == "OK")
            {
                dm.SetRoom(response.MyData);
                this.room = response.MyData;
            }
        }
    }

    private void OnRoomLoaded()
    {
    }
}
