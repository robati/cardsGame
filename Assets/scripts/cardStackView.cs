using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(cardStack))]
public class cardStackView : MonoBehaviour {

	cardStack card_Deck;
	int lastCount;
    public Dictionary<int, GameObject> fetchedCards;
    public bool reverseLayerOrder=false;
	public bool faceUp=false;
    public Vector3 start;
    public float offset;
    public GameObject fab;
    bool isplayerCard;
    public bool isGameCard;
    public event myEventHandler myEventHandler;
    public Transform cardsTransorm;
    void Start()
    {
        //start = cardsTransorm.position;
		card_Deck = GetComponent<cardStack>();
		fetchedCards=new Dictionary<int,GameObject>();
        
        card_Deck.cardRemoved += RemoveCard;
		//lastCount = card_Deck.cardCount;
        showCards();
    }

	void Update(){
		//if (lastCount != card_Deck.cardCount) {
			showCards();
			//lastCount = card_Deck.cardCount;
			
		//}
		}
    void showCards()
    {
        int CardCount = 0;

        if (card_Deck.cardCount != 0) {
						foreach (int i in card_Deck.GetCards()) {
				            float length = CardCount * offset;
				            Vector3 temp = start + new Vector3 (-length, 0);
                            addCard(i,temp,CardCount);
				            CardCount++;
						            }
				}
    }

	void addCard(int index,Vector3 positon,int positionalIndex){
		if(fetchedCards.ContainsKey(index)){
			return;}
		// GameObject card = (GameObject)Instantiate (fab);
        GameObject card =Instantiate<GameObject>(fab, cardsTransorm);
		c1 cardModel = card.GetComponent<c1> ();
		// SpriteRenderer sr = card.GetComponent<SpriteRenderer> ();
		// card.transform.position =new Vector3(0,0,0);// positon;
		cardModel.cardIndex = index;
		cardModel.toggleFace (faceUp);
        cardModel.isPlayerCard = isplayerCard;
        cardModel.isOutOfGameCard = isGameCard;
        if (isplayerCard)
        {
            cardModel.chooseThisCard += delegateFunction;
        }
        if (isGameCard)
        {
            cardModel.giveMeACard += delegateFunction;
        }
        // if (!reverseLayerOrder) { TODO:
		// 				sr.sortingOrder = positionalIndex;
		// 		}
		// 		else {
		// 	sr.sortingOrder =4- positionalIndex;
		// 		}

		fetchedCards.Add(index,card);

        //if (myEventHandler != null)
        //{
        //    myEventHandler(this, new CardEventArg(index));
        //}
		
	}
    void delegateFunction(object sender,CardEventArg args)
    {
        Vector3 pos=new Vector3(0,0,0);
        if (fetchedCards.ContainsKey(args.cardIndex))
            pos = fetchedCards[args.cardIndex].transform.position;//localPosition;
        myEventHandler(this, new StackEventArg(args.cardIndex, pos));
    }
	void RemoveCard(object sender,CardEventArg e){
		if (fetchedCards.ContainsKey (e.cardIndex)) {
				Destroy(fetchedCards[e.cardIndex]);
				fetchedCards.Remove (e.cardIndex);
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
    public void setAsPlayerCard(bool isPlay)
    {
        isplayerCard = isPlay;
    }
//	public int mpop(){
//
//		int temp=card_Deck.pop ();
//		Debug.Log (temp);
//		if(fetchedCards.ContainsKey(temp)){
//			Debug.Log ("temp");
//		Destroy(fetchedCards[temp]);
//		fetchedCards.Remove (temp);
//		}
//		return temp;
//		}
}