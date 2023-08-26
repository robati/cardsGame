using UnityEngine;
using System.Collections;

class playMulti : abstarctPlay
{


    bool rivalTurnFine = false;
    bool rivalSelectCardAfterWild = false;
    public override void startPlay()//start method
    {
        

        int temp = cardLeft.pop();
        Debug.Log("line165 leftcard pushed:" + temp);
        while (isWildCardOn(temp) || isDrawCardOn(temp))
        {
            cardLeft.push(temp);
            temp = cardLeft.pop();
            Debug.Log("line170 leftcard pushed:" + temp);
        }
        deck.push(temp);
        //notification = "Your turn";
        checkCard(deck.playingCard);

        rivalStackView.offset = 5;

        cardLeftView.myEventHandler += giveMeACard;
        rivalStackView.setAsPlayerCard(true);
        rivalStackView.myEventHandler += cardChosen;
        playerStackView.myEventHandler += cardChosen;

		boolGameOver = false;					 

    }
    private void Update() {
        play();
                TurnText.text = menuInfo;}

    public override void playerMoved(object sender,CardEventArg args){
        Debug.Log("yo");
    }
    public override void play()//i=this is the update method
    {

       // if (paused)
        //{
         //   pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu//, "Game Stopped");
        //}

      //  else
     //   {
   //        if (GUI.Button(menu, "menu"))
     //       {
       //         paused = true;
         //       pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
      //      }
			if ((player.cardCount == 0 || rival.cardCount == 0 || cardLeft.cardCount == 0 )&& !boolGameOver){
                Panel.SetActive(true);
                boolGameOver = true;
				}																						  
            if (player.cardCount == 0)
            {
                menuInfo = "playerA won!!";
                paused = true;
            //    pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
            }
            else if (rival.cardCount == 0)
            {
                menuInfo = "playerB won!!";
                paused = true;
                // pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
            }
            else if (cardLeft.cardCount == 0)
            {
                menuInfo = "no body wins";
                paused = true;
             //   pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
            }
        //BUG: move deck to middle in every move
        //            float wunit = Screen.width / 3;
        //          float hunit = Screen.height / 3;

        //            Rect groupRect = new Rect((Screen.width * 2 / 3), (Screen.height / 2), wunit, hunit);
        //          GUI.BeginGroup(groupRect);

        //        GUI.Label(new Rect(0, hunit / 6, wunit, hunit), notification);
        //      GUI.Label(new Rect(0, 0, wunit, hunit), shift == turn.Player ? "playerA Turn" : "playerB Turn");
        //    GUI.EndGroup();    
        InfoText1.text = (shift == turn.Player ? "your Turn" : "rival Turn!");
       // Debug.Log(shift == turn.Player);
        if (!boolGameOver)				 
            play(shift);
   //     }
    }


    void cardChosen(object sender, StackEventArg eventArg)
    {
        cardStack cardStack= player;
        cardStackView cardStackView= playerStackView;
        turn stackOwner = turn.Player; ;
        notification = "";
        if ((cardStackView)sender == playerStackView)
        {
            
            Debug.Log("player is choosing card");
        }
        else if ((object)sender == rivalStackView)

        {

            Debug.Log("rival chosen card is found2" + eventArg.cardIndex);
            cardStackView = rivalStackView;
            cardStack = rival;
            stackOwner = turn.Rival;
        }
        Debug.Log("the card on is:" + deck.playingCard);
        if (cardStack.has(eventArg.cardIndex)&&!cardchoose)
        {
            Debug.Log(playerSelectCardAfterWild + " " + rivalSelectCardAfterWild);

            if ((playerSelectCardAfterWild && stackOwner == turn.Player) || (rivalSelectCardAfterWild && stackOwner == turn.Rival))
            {
                Debug.Log("A");
               
                Debug.Log(playerSelectCardAfterWild + " " + rivalSelectCardAfterWild);
                Vector3 cardTargetPos = gameScript.deckTransform.position;

                dealCard(eventArg.cardIndex, eventArg.pos, cardTargetPos, true, deck, cardStack, new System.Action(() =>  { 
                
                 if (stackOwner == turn.Player)
                    playerSelectCardAfterWild = false;
                else
                    rivalSelectCardAfterWild = false;
                
                }));


                //    deck.push(cardStack.pop(eventArg.cardIndex)); //TODO:m is it right?
                cardStackView.updateCardView();

                Debug.Log("line125 in select card player pushed:" + eventArg.cardIndex);
                return;
            }

            if (((stackOwner == turn.Player) && (playerTurnFine == false) && (shift == turn.Player)) || ((stackOwner == turn.Rival) && (shift == turn.Rival) && (rivalTurnFine == false)))
            {
                cardchoose = true;
//Debug.Log(isCompatible(deck.playingCard, eventArg.cardIndex));
                if (isCompatible(deck.playingCard, eventArg.cardIndex))
                {
                    Debug.Log("B");

                    Vector3 cardTargetPos = gameScript.deckTransform.position;

                    dealCard(eventArg.cardIndex, eventArg.pos, cardTargetPos, true, deck, cardStack, new System.Action(() =>
                    {
                        if (stackOwner == turn.Player)
                            playerTurnFine = true;
                        else
                            rivalTurnFine = true;
                        cardStackView.updateCardView();
                        cardchoose = false;
                    }
                    ));

                
                }

                else
                {
                    notification = "this move is not possible";
                    notify(notification);

                    cardchoose = false;
                }

            }
        }
        //InfoText1.text = (shift == turn.Player ? "your Turn" : "rival Turn!"); //TODO:is it cool if i comment this?
        //Debug.Log(shift == turn.Player);

    }
    void giveMeACard(object sender, StackEventArg eventArg)
    {
        notification = "";
        if (cardLeft.has(eventArg.cardIndex)&&!cardgive)
        {

            if ((shift == turn.Player) && (playerTurnFine == false) && !playerSelectCardAfterWild)
            {
                cardgive = true;
                Vector3 cardTargetPos = gameScript.playerTransform.position;//playerPos + new Vector3(-playerStackView.offset * player.cardCount, 0, 0);
                dealCard(eventArg.cardIndex, eventArg.pos, cardTargetPos, true, player, cardLeft, new System.Action(() =>
                {
                    cardLeftView.updateCardView();
                    playerStackView.updateCardView();
                    playerTurnFine = true;
                    cardgive = false;
                }));
               

            }
            else if ((shift == turn.Rival) && (rivalTurnFine == false) && !rivalSelectCardAfterWild)
            {
                cardgive = true;
                Vector3 cardTargetPos = gameScript.rivalTransform.position;//rivalPos + new Vector3(-rivalStackView.offset * rival.cardCount, 0, 0);
                dealCard(eventArg.cardIndex, eventArg.pos, cardTargetPos, true, rival, cardLeft, new System.Action(() =>
                {
                    cardLeftView.updateCardView();
                    rivalStackView.updateCardView();
                    rivalTurnFine = true;
                    cardgive = false;
                }));
            }
            
            }
        
    }

    public override void distributeCard()
    {
        for (int i = 0; i < 3; i++)
        {
            int temp = cardLeft.pop();
            player.push(temp);
            //Debug.Log("pushed" + temp);
            temp = cardLeft.pop();
            rival.push(temp);
        }
    }
    public override void startTheGame()
    {
        cardLeft.createDeck();
        rivalStackView.faceUp = true;
        

    }
    void play(turn turn)
    {

        if (turn == turn.Rival )
        {
         
            if (rivalTurnFine)
            {
                rival_uno = false;

                if (isWildCardOn(deck.playingCard))
                {
                    rivalSelectCardAfterWild = true;
                }
                else
                {
                    rivalTurnFine = false;
                    checkCard(deck.playingCard);
                    converseShift();
                    Debug.Log("r did");
                    rivalMoved = Time.realtimeSinceStartup;
                }
            }
        }
        else if (turn == turn.Player)
        {
            if (playerTurnFine)
            {
                player_uno = false;

                if (isWildCardOn(deck.playingCard))
                {
                    playerSelectCardAfterWild = true;
                }
                else
                {
                    playerTurnFine = false;
                    checkCard(deck.playingCard); // if wild play one card
                    converseShift();
                    Debug.Log("p did");
                    rivalMoved = Time.realtimeSinceStartup;
                }
            }
        }
    }



    

}