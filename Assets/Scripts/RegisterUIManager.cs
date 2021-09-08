using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterUIManager : MonoBehaviour
{
    public InputField Email;
    public InputField Password;
    public InputField Username;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRegisterButtonClick()
    {
        Debug.Log("register klik!");
        this.GetComponent<PlayfabManager>().Register(this.Email.text, this.Username.text, this.Password.text);
    }
}
