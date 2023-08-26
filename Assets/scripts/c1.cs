using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class c1 : MonoBehaviour {

    //public Sprite first;
    public Sprite second;
    public Sprite[] AllFace;
    public int cardIndex=0;
    public bool isPlayerCard = false;
    public bool isOutOfGameCard = false;
        // public Image second;
      Image cardSprite;
    public event cardChosenEventHandler chooseThisCard;
    public event cardChosenEventHandler giveMeACard;
    public event cardChosenEventHandler played;
    public void toggleFace(bool on)
    {

		if (on) {
            // Debug.Log(cardSprite.sprite==null);// "  " AllFace.lrngyh);
			cardSprite.sprite = AllFace [cardIndex /*% AllFace.Length*/]; //BUG may because of not pausing when finish
		} else {
			cardSprite.sprite = second;
		}
	}      

	public	void OnMouseDown() {
			
			Debug.Log ("clicked on " + cardIndex);
        if (isPlayerCard)
        {
            Debug.Log("isPlayerCard");
            played?.Invoke(this, new CardEventArg(cardIndex));
            if (chooseThisCard != null)
                chooseThisCard(this, new CardEventArg(cardIndex));
        }
        else if (isOutOfGameCard)
        {
            Debug.Log("isOutOfGameCard");
            if (giveMeACard != null)
                giveMeACard(this, new CardEventArg(cardIndex));
        }
    }

    
    void Awake()
    {
        cardSprite = GetComponent<Image>();
        // Debug.Log(cardSprite.sprite==null);
       
    }
}
