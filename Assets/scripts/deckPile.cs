using UnityEngine;

using System.Collections.Generic;
using System.Collections;
[RequireComponent(typeof(cardStack))]
public class deckPile : MonoBehaviour{
    cardStack card_Deck;
    Dictionary<int, GameObject> fetchedCards;
    public Vector3 place;
    public GameObject fab;
    public float offset;
    public Transform cardsTransorm;

    void Start()
    {

        card_Deck = GetComponent<cardStack>();
        fetchedCards = new Dictionary<int, GameObject>();
        card_Deck.cardRemoved += RemoveCard;
        showCards();
    }
    void Update()
    {
       // showCards();
    }
    void RemoveCard(object sender, CardEventArg e)
    {
        if (fetchedCards.ContainsKey(e.cardIndex))
        {
            Destroy(fetchedCards[e.cardIndex]);
            fetchedCards.Remove(e.cardIndex);
        }
    }
    public void updateCardView() //TODO:m Can i delete this?
    {
        foreach (GameObject i in fetchedCards.Values)
        {
            Destroy(i);

        }
        fetchedCards.Clear();
        showCards();

    }
    void showCards()
    {
        int[] angle = new int[] {0,45,90,/* 135, 180, 225, 270,*/ 315 };
        int CardCount = 0;
        if (card_Deck.cardCount != 0)
        {
            foreach (int i in card_Deck.GetCards())
            {
                float length = angle[(CardCount % 4)];
                    //CardCount * offset;
                addCard(i, length, CardCount);
                CardCount++;
            }
        }
    }

    void addCard(int index, float positon,int sortIndex)
    {
        if (fetchedCards.ContainsKey(index))
        {
            return;
        }
        //GameObject card = (GameObject)Instantiate(fab);
        GameObject card =Instantiate<GameObject>(fab, cardsTransorm);
        c1 cardModel = card.GetComponent<c1>();
        // SpriteRenderer sr = card.GetComponent<SpriteRenderer>();
        // card.transform.position = place;
        cardModel.cardIndex = index;
        cardModel.toggleFace(true);
        
        // sr.sortingOrder = sortIndex;TODO:
        card.transform.Rotate(0, 0, positon);
        //Debug.Log(card.transform.localRotation);
        //Debug.Log(card.transform.rotation);

   // public Quaternion rotation = Quaternion.identity;
       // cardModel.transform.rotation = new Quaternion(0, 0, 0.2f,0);
       // print(rotation.eulerAngles.y);

        fetchedCards.Add(index, card);

    }
}

