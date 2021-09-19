using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ResponsePlayerData
{
    public string Jwt { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Wins { get; set; }
    public string Loses { get; set; }
    public string Draws { get; set; }
    public string PlayFabId { get; set; }
    public object InGameStatus { get; set; }
    public object Avatar { get; set; }
}

public class RegisterResponse
{
    public string ResponseId { get; set; }
    public ResponsePlayerData MyData { get; set; }
}
public class LoginResponse
{
    public string ResponseId { get; set; }
    public ResponsePlayerData MyData { get; set; }
}

public class PlayfabManager : MonoBehaviour
{
    private string Email;
    private string Password;
    private string Username;
    public bool UserLoggedIn = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Login(string email, string password)
    {
        PlayFabSettings.TitleId = "6D8B1";
        this.Email = email;
        this.Password = password;
        Debug.Log(email);
        Debug.Log(password);
        var login = new LoginWithEmailAddressRequest { Email=this.Email, Password=this.Password };
        PlayFabClientAPI.LoginWithEmailAddress(login, OnSuccess, OnError);
    }

    public void Register(string email, string username, string password)
    {
        PlayFabSettings.TitleId = "6D8B1";
        this.Email = email;
        this.Username = username;
        this.Password = password;
        var register = new RegisterPlayFabUserRequest { Email = this.Email, Password = this.Password, Username = this.Username };
        PlayFabClientAPI.RegisterPlayFabUser(register, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnError(PlayFabError obj)
    {
        Debug.Log("Login error!");
        Debug.Log(obj.ErrorMessage);
        this.UserLoggedIn = false;
    }

    private void OnRegisterFailure(PlayFabError obj)
    {
        Debug.Log("Playfab register failure");
        Debug.Log(obj.ErrorMessage);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
    {
        TcpKlijent klijent = new TcpKlijent();
        DataManager dm = new DataManager();
        dm.SetMySessionTicket(obj.SessionTicket);
        dm.SetMyPlayfabId(obj.PlayFabId);
        klijent.PosaljiServeru("{\"commandId\":\"YOIJUSTREGISTERED\", \"sessionTicket\":\"" + obj.SessionTicket + "\"}");
        string odgovor = klijent.PrimiOdServera();
        RegisterResponse parsedResponse = JsonConvert.DeserializeObject<RegisterResponse>(odgovor);
        dm.SetJwt(parsedResponse.MyData.Jwt);
        dm.SetPlayerLoggedIn(true);
        SceneManager.LoadScene("Lobby");
    }

    private void OnSuccess(LoginResult result)
    {
        Debug.Log(JsonUtility.ToJson(result));
        Debug.Log("Successfully logged in");
        this.GetLeaderboard();
        this.UserLoggedIn = true;
        GetUserData(result.PlayFabId);
        TcpKlijent klijent = new TcpKlijent();
        DataManager dm = new DataManager();
        dm.SetMySessionTicket(result.SessionTicket);
        dm.SetMyPlayfabId(result.PlayFabId);
        klijent.PosaljiServeru("{\"commandId\":\"YOIJUSTLOGGEDIN\", \"sessionTicket\":\"" + result.SessionTicket+"\"}");
        string odgovor = klijent.PrimiOdServera();
        LoginResponse parsedResponse = JsonConvert.DeserializeObject<LoginResponse>(odgovor);
        Debug.Log("Primio sam odgovor:" + odgovor);
        dm.SetJwt(parsedResponse.MyData.Jwt);
        klijent.ZatvoriSocket();
        dm.SetMyUsername(parsedResponse.MyData.Username);
        dm.SetPlayerLoggedIn(true);
    }

    void GetUserData(string myPlayFabeId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabeId,
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("Ancestor")) Debug.Log("No Ancestor");
            else Debug.Log("Ancestor: " + result.Data["Ancestor"].Value);
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "PlatformScore",
                    Value = score,
                }
            },
    };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdate, OnUpdateError);
}

    private void OnUpdateError(PlayFabError obj)
    {
        Debug.Log("Update success");
        Debug.Log(obj.ErrorMessage);
    }

    private void OnUpdate(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log("Update succcess");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10,
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardResult, OnGetLeaderBoardError);
}

    private void OnLeaderboardResult(GetLeaderboardResult obj)
    {
        Debug.Log("Leaderboard result success");
        foreach(var item in obj.Leaderboard)
        {
            Debug.Log("position " + item.Position + " for player " + item.DisplayName);
        }
    }

    private void OnGetLeaderBoardError(PlayFabError obj)
    {
        Debug.Log("Get leaderboard error");
        Debug.Log(obj.ErrorMessage);
    }
}
