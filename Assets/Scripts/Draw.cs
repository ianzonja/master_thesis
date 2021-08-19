using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject Card1;
    public GameObject HandArea;
    List<GameObject> cards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        cards.Add(Card1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        for(int i=0; i<5; i++)
        {
            GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            playerCard.transform.SetParent(HandArea.transform);
            cards.Add(playerCard);
        }
    }
}
