using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyResponse{
    public string NumberOfPlayers { get; set; }
    public List<PlayerData> Players { get; set; }
    public List<Room> Rooms { get; set; }
}

public class OnLobbyLoaded : MonoBehaviour
{
    public GameObject RoomUI;
    public InputField UsernameInput;
    public InputField PasswordInput;
    bool userLogged = false;
    int FrameCounter = 0;

    void OsluskujPort(object listen)
    {
        //DataManager dm = new DataManager("read");
        Debug.Log(new DataManager().IsLoggedIn());
        while (new DataManager().IsLoggedIn())
        {
            if (FrameCounter % 60 == 0)
            {
                TcpKlijent klijent = new TcpKlijent();
                klijent.PosaljiServeru("{\"commandId\":\"YOGIVEMEROOMINFO\", \"sessionTicket\":\"" + MyData.SessionTicket + "\"}");
                string odgovor = klijent.PrimiOdServera();
                //var sth = JsonConvert.DeserializeObject<object>(odgovor);
                Debug.Log("Primio sam odgovor:" + odgovor);
                LobbyResponse response = JsonConvert.DeserializeObject<LobbyResponse>(odgovor);
                DataManager dm = new DataManager();
                dm.SetLobbyRooms(response.Rooms);
                klijent.ZatvoriSocket();
            }
        }
    }

    private void Start()
    {
        Thread dretvaZaListen = new Thread(new ParameterizedThreadStart(OsluskujPort));
        dretvaZaListen.Start();
    }

    private void Update()
    {
        if(FrameCounter % 120 == 0)
        {
            DataManager dm = new DataManager();
            string username = dm.GetMyUsername();
            if (username != "" && username != null)
                GameObject.Find("Username").GetComponent<Text>().text = username;
            List<Room> rooms = dm.GetLobbyRooms();
            if (rooms != null)
            {
                foreach (var room in rooms)
                {
                    var roomObject = GameObject.Find(room.Id);
                    if (roomObject != null)
                        Destroy(roomObject);
                    GameObject contentUI = GameObject.Find("Content");
                    var newRoom = Instantiate(RoomUI, new Vector3(0, 0, 0), Quaternion.identity);
                    newRoom.transform.SetParent(contentUI.transform);
                    newRoom.name = room.Id;
                }
            }
        }
        FrameCounter++;
    }

    public void OnCreateRoomButtonClick()
    {
        Debug.Log("Kreiram sobu");
        SceneManager.LoadScene("RoomSetup");
    }
}
