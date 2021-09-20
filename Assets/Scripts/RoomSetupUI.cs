using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Linq;

public class RoomSetupResponse
{
    public string ResponseId { get; set; }
    public Room MyData { get; set; }
}
public class RoomSetupUI : MonoBehaviour
{
    public Toggle PublicRoomToggle;
    public InputField Password;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreateRoomButtonClick()
    {
        Debug.Log("Kreiram sobu");
        Debug.Log(PublicRoomToggle.isOn);
        Debug.Log(Password.ToString());
        TcpKlijent client = new TcpKlijent();
        string sessionTicket = new DataManager().GetMySessionTicket();
        if(sessionTicket != null)
        {
            client.PosaljiServeru("{\"commandId\":\"YOIWANNAHOST\", \"sessionTicket\":\"" + sessionTicket + "\", \"Jwt\":\"" + new DataManager().GetJwt() + "\"}");
            string odgovor = client.PrimiOdServera();
            Debug.Log(odgovor);
            RoomSetupResponse response = JsonConvert.DeserializeObject<RoomSetupResponse>(odgovor);
            DataManager dm = new DataManager();
            dm.SetRoom(response.MyData);
            dm.SetIsGameHost(true);
        }
        else
        {
            Debug.Log("No session ticket :(");
        }
        SceneManager.LoadScene("Room");
    }
}
