using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public GameObject Hand;
    public GameObject Card;
    public GameObject Table;
    private List<GameObject> Cards = new List<GameObject>();
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MyData.Instance.IsDealer)
            this.DealCards();
        if (MyData.Instance.IsNewRound && MyData.Instance.CardsDealt)
        {
            this.InstatiateCards();
        }
    }

    private void SetHorizontalLayoutGroupForCards(int numberOfCards)
    {
        var rect = Hand.GetComponent<RectTransform>().rect;
        if (numberOfCards == 10)
        {
            Hand.GetComponent<RectTransform>().rect.Set(rect.x, rect.y, 1000, rect.height);
            Hand.GetComponent<HorizontalLayoutGroup>().spacing = -200;
        }
        else if (numberOfCards == 9)
        {
            Hand.GetComponent<RectTransform>().rect.Set(rect.x, rect.y, 800, rect.height);
            Hand.GetComponent<HorizontalLayoutGroup>().spacing = -200;
        }
        else
        {
            Hand.GetComponent<RectTransform>().rect.Set(rect.x, rect.y, 600, rect.height);
            Hand.GetComponent<HorizontalLayoutGroup>().spacing = -200;
        }
    }

    private void InstatiateCards()
    {
        Debug.Log("Instanciram karte");
        SetHorizontalLayoutGroupForCards(this.Cards.Count);
        for (int i=1; i<Cards.Count; i++)
        {
            GameObject newCard = Instantiate(Card);
            newCard.transform.SetParent(Hand.transform);
        }
    }

    public void DealCards()
    {
        bool cardsDealt = false;
        if (cardsDealt)
        {
            Debug.Log("Podijelio sam karte");
        }
        else
        {
            Debug.Log("Karte nisu podijeljene iz nekog cudnog razloga. NE mogu nastaviti sa igrom.");
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
