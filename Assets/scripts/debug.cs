using UnityEngine;
using System.Collections;
using System;

public class debug : MonoBehaviour {
    //public cardStackView dealer;
    //public cardStack player;
    //public cardStack game;
    cardAnimator cardAnimate;
    public GameObject card;


    private void Awake()
    {
        cardAnimate = card.GetComponent<cardAnimator>();
    }
    void OnGUI(){
        if (GUI.Button(new Rect(10, 20, 100, 20), "hia"))
        {
            cardAnimate.FlipCard();
        }
        if (GUI.Button(new Rect(10, 40, 100, 20), "1"))
        {
          //  cardAnimate.MoveCard(new Vector3(0,0,0));
        }
        //           int temp = game.pop();
        //		player.push(temp);
        //           Debug.Log("pushed" + temp);
        //		//player.push(dealer.mpop());
        //}
    }

    internal static void Log(bool v)
    {
        throw new NotImplementedException();
    }
}