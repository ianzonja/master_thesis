using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class GuestLoginResponse
{
    public string username { get; set; }
}

public class LoginUIManager : MonoBehaviour
{
    public InputField EmailInput;
    public InputField PasswordInput;
    private bool ChangeScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (new DataManager().IsLoggedIn())
            SceneManager.LoadScene("Lobby");
    }

    public void OnRegisterWithEmailButtonClick()
    {
        SceneManager.LoadScene("Register");
    }

    public void OnGuestLoginButtonClick()
    {
        Debug.Log("Logiram se kao gost");
        TcpKlijent klijent = new TcpKlijent();
        klijent.PosaljiServeru("{\"commandId\":\"IMMAGUESTAPE\", \"data\":{}}");
        string odgovor = klijent.PrimiOdServera();

        //GuestLoginResponse glr = JsonConvert.DeserializeObject<GuestLoginResponse>(odgovor);
        //Debug.Log("Primio sam odgovor:" + glr);
        klijent.ZatvoriSocket();
    }

    public void OnLoginWithEmailButtonClick()
    {
        this.GetComponent<PlayfabManager>().Login(EmailInput.text, PasswordInput.text);
    }
}
