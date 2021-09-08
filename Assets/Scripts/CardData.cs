using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    [SerializeField]
    public string Name { get; set; }
    [SerializeField]
    public string Type { get; set; }
    [SerializeField]
    public string Value { get; set; }
    [SerializeField]
    public string BackgroundImage { get; set; }
    [SerializeField]
    public int Points { get; set; }
}
