using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class IngamePlayer
{
    public string username;
    public string guid;
}
public class TotalPlayers : MonoBehaviour
{
    public static TotalPlayers instance;
    public List<IngamePlayer> players = new List<IngamePlayer>();
    public int totalPlayers = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
