using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public GameObject Hand;
    public GameObject Card;
    public GameObject Table;
    public bool CanPlay = false;
    private List<GameObject> Cards = new List<GameObject>();
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //private void SetHorizontalLayoutGroupForCards(int numberOfCards)
    //{
    //    var rect = Hand.GetComponent<RectTransform>().rect;
    //    if (numberOfCards == 10)
    //    {
    //        Hand.GetComponent<RectTransform>().rect.Set(rect.x, rect.y, 1000, rect.height);
    //        Hand.GetComponent<HorizontalLayoutGroup>().spacing = -200;
    //    }
    //    else if (numberOfCards == 9)
    //    {
    //        Hand.GetComponent<RectTransform>().rect.Set(rect.x, rect.y, 800, rect.height);
    //        Hand.GetComponent<HorizontalLayoutGroup>().spacing = -200;
    //    }
    //    else
    //    {
    //        Hand.GetComponent<RectTransform>().rect.Set(rect.x, rect.y, 600, rect.height);
    //        Hand.GetComponent<HorizontalLayoutGroup>().spacing = -200;
    //    }
    //}

    public void InstatiateCards(CardData[] hand, string gameId)
    {
        for(int i=0; i<hand.Length; i++) {
            List<GameObject> cards = new List<GameObject>();
            GameObject card = Instantiate(Card, new Vector3(0, 0, 0), Quaternion.identity);
            card.name = hand[i].Value;
            GameObject text = card.transform.Find("Text").gameObject;
            text.GetComponent<Text>().text = hand[i].Value;
            card.transform.SetParent(Hand.transform);
            card.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (this.CanPlay)
                {
                    card.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 300);
                    card.transform.SetParent(this.Table.transform);
                    TcpKlijent klijent = new TcpKlijent();
                    klijent.PosaljiServeru("{\"commandId\":\"YOIPLAYEDCARD\", \"sessionTicket\":\"" + MyData.Instance.SessionTicket + "\", \"Jwt\":\"" + new DataManager().GetJwt() + "\", \"GameId\":\"" + gameId + "\", \"CardValue\": \""+card.name+"\"}");
                    string odgovor = klijent.PrimiOdServera();
                    GameResponse response = JsonConvert.DeserializeObject<GameResponse>(odgovor);
                    DataManager dm = new DataManager();
                    dm.SetMyGame(response.MyData);
                    this.CanPlay = false;
                    for(int i=0; i<response.MyData.Players.Length; i++) { 
                        if(response.MyData.Players[i].PlayfabId == dm.GetMyPlayfabId())
                        {
                            if (response.MyData.Players[i].Hand.Length == 0)
                                dm.SetMyHandEmpty(true);
                        }
                    }
                }
            });
            cards.Add(card);
        }
    }

    static int Next(RNGCryptoServiceProvider random)
    {
        byte[] randomInt = new byte[4];
        random.GetBytes(randomInt);
        return Convert.ToInt32(randomInt[0]);
    }

    public CardData[] DealCards(Game game)
    {
        bool cardsDealt = false;
        if (cardsDealt)
        {
            var cards = new CardData[40];
            for(int i=1; i<=40; i++)
            {
                CardData card = new CardData();
                card.Value = i.ToString();
                cards[i - 1] = card;
            }
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            cards = cards.OrderBy(x => Next(random)).ToArray();
            return cards;
        }
        else
        {
            Debug.Log("Karte nisu podijeljene iz nekog cudnog razloga. NE mogu nastaviti sa igrom.");
            return null;
        }
    }

    public void OnCardClicked()
    {
        Debug.Log("klik");
    }


    public void OnCardDragBegin()
    {
        Debug.Log("Pocinjem dragati");
        
    }

    public void OnCardDragEnd()
    {
        Debug.Log("Dragging gotov");
    }
    public void DragCard()
    {
        Debug.Log("Karta se draga");
    }
}
