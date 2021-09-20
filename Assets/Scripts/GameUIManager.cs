using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string Text { get; set; }

    public string AuthorPlayfabId { get; set; }

    public string Date { get; set; }

    public string Time { get; set; }
}

public class RoundPoint
{
    public string Team1 { get; set; }
    public string Team2 { get; set; }
}

public class GameResponse
{
    public string responseId { get; set; }
    
    public Game MyData { get; set; }
}

public class GameUIManager : MonoBehaviour
{
    public GameObject chatBox;
    private Game game;
    private bool canPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        DataManager dm = new DataManager();
        Room room = dm.GetRoom();
        if(dm.GetMyPlayfabId() == room.HostPlayfabId)
        {
            TcpKlijent klijent = new TcpKlijent();
            klijent.PosaljiServeru("{\"commandId\":\"YOGAMESTARTED\", \"sessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"Jwt\":\"" + new DataManager().GetJwt() + "\", \"RoomId\":\"" + room.Id + "\"}");
            string odgovor = klijent.PrimiOdServera();
            GameResponse response = JsonConvert.DeserializeObject<GameResponse>(odgovor);
            this.game = response.MyData;
            dm.SetMyGame(this.game);
        }
        this.chatBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.game != null)
        {
            TcpKlijent klijent = new TcpKlijent();
            klijent.PosaljiServeru("{\"commandId\":\"YOGIVEMEGAMEINFO\", \"sessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"Jwt\":\"" + new DataManager().GetJwt() + "\", \"GameId\":\"" + game.GameId + "\"}");
            string odgovor = klijent.PrimiOdServera();
            GameResponse response = JsonConvert.DeserializeObject<GameResponse>(odgovor);
            if (response != null)
            {
                this.game = response.MyData;
                DataManager dm = new DataManager();
                if (this.game.Status == "STARTED")
                {
                    if (response.MyData.DealerPlayfabId == dm.GetMyPlayfabId())
                    {
                        var cards = this.GetComponent<CardManager>().DealCards(response.MyData);
                        this.game.ShuffledCards = cards;
                        dm.SetMyGame(game);
                        klijent.PosaljiServeru("{\"commandId\":\"YOISHUFFLED\", \"sessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"Jwt\":\"" + new DataManager().GetJwt() + "\", \"GameId\":\"" + game.GameId + "\"}");
                        odgovor = klijent.PrimiOdServera();
                        response = JsonConvert.DeserializeObject<GameResponse>(odgovor);
                        if(response != null)
                        {
                            if(response.MyData != null)
                                this.game = response.MyData;
                        }
                    }
                }
                else if (this.game.Status == "ROUND")
                {
                    for (int i = 0; i < this.game.Players.Length; i++)
                    {
                        if (this.game.Players[i].PlayfabId == dm.GetMyPlayfabId())
                        {
                            //DEALER SHUFFLED CARDS AND ROUND CAN START
                            if (dm.GetMyHandEmpty())
                            {
                                //HAVE TO INSTANTIATE NEW CARDS
                                this.GetComponent<CardManager>().InstatiateCards(this.game.Players[i].Hand, this.game.GameId.ToString());
                            }
                            //check if its my turn to play
                            if (response.MyData.PlayerIdToPlay == dm.GetMyPlayfabId())
                                this.GetComponent<CardManager>().CanPlay = true;
                            else
                                this.GetComponent<CardManager>().CanPlay = false;
                        }
                    }
                }
            }
        }
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
