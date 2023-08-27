using UnityEngine;

using System.Collections.Generic;
using System.Collections;

public class cardStack : MonoBehaviour {

    List<int> cards= new List<int>();
//bug: card image quality sucks
    AudioSource resource;
    public AudioClip cardPopAudio;
    //public bool isPlayer=false;
    public int playingCard = -1;//deckPile
    public int cardCount {
		get{
			if(cards==null)
				return 0;
			return cards.Count;		
		
		}
	}
    public int GetaCard()
    {
        if(cardCount > 0)
            return cards[cardCount-1];
        Debug.LogWarning("Card finished");
        return -1;
    }

	public event cardRemovedEventHandler cardRemoved;
	public int pop(){
		if (cardCount != 0 ) {
				int temp = cards [0];
            if (temp < Constants.cardCount&&temp>=0) {
                cards.RemoveAt(0);
                cardRemoved(this, new CardEventArg(temp));
                return temp;
                     }
				}
		return -1;
		}
    public int pop(int idx)
    {
        if (idx == -2)
        {
           return pop();
        }
        if (cardCount != 0)
        {
            
            int cardPosInList = cards.IndexOf(idx);
            Debug.Log("in pop methodddd: index=" + idx + "and in list :" + cardPosInList);
            int temp = cards[cardPosInList];
            if (temp < Constants.cardCount && temp >= 0) {
                resource.PlayOneShot(cardPopAudio);
                cards.RemoveAt(cardPosInList);
                cardRemoved(this, new CardEventArg(temp));          
                return temp;
                }
        }

        return -1;
    }
    public void push(int i){
        if (i != -1)
        {
            if (i < Constants.cardCount && i >= 0) {
               cards.Add(i);
               playingCard = i;
                  }

             }
             Debug.Log("ry");
		}
   public void createDeck()
    {
        if (cards == null)
        {
            cards = new List<int>();
        }
        else
        {
            cards.Clear();
        }
        for(int i = 0; i < Constants.cardCount; i++)
        {
            cards.Add(i);
        }
        int n = cards.Count - 1;
        while (n >= 0)
        {
            int k = Random.Range(0, n);
            int temp = cards[n];
            cards[n] = cards[k];
            cards[k] = temp;
            n--;
        }

    }
    public bool has(int idx)
    {
        return cards.Contains(idx);
           
    }
    public IEnumerable<int> GetCards()
    {
        
        foreach(int i in cards)
        {
            yield return i;
        }
    }
    void Start () {
        resource = GetComponent<AudioSource>();
        
        //cards = new List<int>();
        //if (!isPlayer) {//TODO: omit this
        //				createDeck ();
        //		}
    }
    public void resetStack()
    {
        foreach( int tempCard in cards)
        {
            cardRemoved(this, new CardEventArg(tempCard));
        }

        cards= new List<int>();
    }

}
