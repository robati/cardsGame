using UnityEngine;
using System.Collections;


 class playAI : abstarctPlay
{
    
    public AudioClip uno;
    bool AIWork = false;
    public Texture2D unoButtonImage;



    public override void startPlay()//start method
    {
        
        int temp = cardLeft.pop();
        // Debug.Log("line165 leftcard pushed:" + temp);
        while (isWildCardOn(temp)||isDrawCardOn(temp))
        {
            cardLeft.push(temp);
            temp = cardLeft.pop();
            // Debug.Log("line170 leftcard pushed:" + temp);
        }
        deck.push(temp);
        //notification = "Your turn";
        checkCard(deck.playingCard);

        rivalStackView.setAsPlayerCard(false);
        

        cardLeftView.myEventHandler += giveMeACard;
        playerStackView.myEventHandler += cardChosen;
        rivalMoved = Time.realtimeSinceStartup;

        boolGameOver = false;
        cardLeftView.updateCardView();
        deckStackView.updateCardView() ;   
    }
   
 public void onClick(int i){
           switch (i)
        {
            case 0: //Menu 
                            paused = true;
                            //                pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
                break;
            case 1: //toggle 
                if (!rivalStackView.faceUp)
                    rivalStackView.faceUp = true;
                else
                    rivalStackView.faceUp = false;
                rivalStackView.updateCardView();
                break;
                
            case 2://uno
                                        notification = "you said Uno!";

            notify(notification);

                playerStackView.updateCardView();
                resource.PlayOneShot(uno);
                player_uno = true;
                // Debug.Log(lastNotif);
                break;
            case 3://uno rival
                if (rival.cardCount == 1 && !rival_uno)
                {
                    if (shift == turn.Player)
                    {
                        resource.PlayOneShot(uno);
                        converseShift();
                        Draw();
                        Draw();
                        converseShift();

                    }
                    else 
                    {
                        Draw();
                        Draw();
                    }
                    notification += "she forgot Uno";
                                notify(notification);

                    // Debug.Log(notification);

                }
                break;
        }


 }

    private void Update() {
        play();
                TurnText.text = menuInfo;

        // if(notification != lastNotif){
        // InfoText.text = notification+ "\n" +  InfoText.text;
        // lastNotif = notification;
        // }
        // // Debug.Log(notification);
        // TurnText.text = menuInfo;
    }
    public override void playerMoved(object sender,CardEventArg args){
        Debug.Log("yo");
    }
    public override void play()//i=this is the update method
    {


        // if (paused)
        // { 
        //     //TODO
        //     //pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped"); 
        // }
       
        // else {
                        // Debug.Log("unpause game");

            //m if (GUI.Button(menu, "menu"))
            // {
            //     paused = true;
            //     pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
            //m }

           // Rect groupRect0 = new Rect(3*(Screen.width / 4), 0, 120, 360);
           // GUI.BeginGroup(groupRect0);
            //m if (GUI.Button(new Rect(5 * (Screen.width / 6) , Screen.height /5, 10, 10), "")) 
            // {
            //     if (!rivalStackView.faceUp)
            //         rivalStackView.faceUp = true;
            //     else
            //         rivalStackView.faceUp = false;
            //     rivalStackView.updateCardView();
            //m }
            // float wdrUnit = Screen.width / 12;
            // Rect rivalUno = new Rect( (Screen.width * 3/ 4), Screen.height /4, wdrUnit, wdrUnit);
            //m GUI.DrawTexture(rivalUno, unoButtonImage);
            // if (GUI.Button(rivalUno, "Uno"))
            // {
            //     if (rival.cardCount == 1 && !rival_uno)
            //     {
            //         if (shift == turn.Player)
            //         {
            //             resource.PlayOneShot(uno);
            //             converseShift();
            //             Draw();
            //             Draw();
            //             converseShift();

            //         }
            //         else 
            //         {
            //             Draw();
            //             Draw();
            //         }
            //         notification = "she forgot Uno";
            //     }
            //     //  card.toggleFace(false);
            //m }
           //m Rect playerUno = new Rect((Screen.width * 3 / 4), Screen.height *3/ 4, wdrUnit, wdrUnit);
            // GUI.DrawTexture(playerUno, unoButtonImage);
            // if (GUI.Button(playerUno, "Uno"))
            // {
            //     playerStackView.updateCardView();
            //     resource.PlayOneShot(uno);
            //     player_uno = true;
            //     notification = "you said Uno!";
            //     Debug.Log("player Uno");
            //     //  card.toggleFace(false);
            //m }
          //  GUI.EndGroup();
            if ((player.cardCount == 0 || rival.cardCount == 0 || cardLeft.cardCount == 0 )&& !boolGameOver){
                Panel.SetActive(true);
                boolGameOver = true;
                // Debug.Log(player.cardCount+" "+rival.cardCount+" "+cardLeft.cardCount);
            
            }//BUG: when many cart m iut of window bord
            // if(boolGameOver)
            //                 Debug.Log(player.cardCount+" "+rival.cardCount+" "+cardLeft.cardCount);

            if (player.cardCount == 0)
            {
                menuInfo = "you have won!!";
                // Debug.Log(menuInfo);
                paused = true;
                //TODO: show pauseMenu
//                pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
            }
            else if (rival.cardCount == 0)
            {
                menuInfo = "you have lost ";
                                // Debug.Log(menuInfo);

                paused = true;
                                //TODO: show pauseMenu

                // pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
            }
            else if (cardLeft.cardCount == 0)
            {
                menuInfo = "no body wins";
                                Debug.Log(menuInfo);

                paused = true;
                                //TODO: show pauseMenu

                // pauseMenuRect = GUI.Window(0, pauseMenuRect, drawPauseMenu, "Game Stopped");
            }
            // float wunit = Screen.width/3;
            // float hunit = Screen.height/3;


            // Rect groupRect = new Rect((Screen.width *2/3) , (Screen.height /2), wunit, hunit);
            // GUI.BeginGroup(groupRect);

            // GUI.Label(new Rect(0, hunit/6, wunit, hunit), notification);
            // GUI.EndGroup();     //close the group
            if(!boolGameOver)
            play(shift);
        // }
    }


    void cardChosen(object sender, StackEventArg eventArg)
    {
        // Debug.Log("the chosen card is found" + eventArg.cardIndex);
        // Debug.Log("the card on is:" + deck.playingCard);
        if (player.has(eventArg.cardIndex)&&!cardchoose){
            
            if (playerSelectCardAfterWild)
            {
                playerSelectCardAfterWild = false;
                deck.push(player.pop(eventArg.cardIndex));//TODO:m add deal
                playerStackView.updateCardView();
                deckStackView.updateCardView();

                // Debug.Log("line125 in select card player pushed:" + eventArg.cardIndex);
                // cardchoose = false;
                return;
            }
            else if ((shift == turn.Player) && (playerTurnFine == false))//?
            {
                cardchoose = true;

                if (isCompatible(deck.playingCard, eventArg.cardIndex))
                {
                    Vector3 cardTargetPos = deckPos;

                    dealCard(eventArg.cardIndex, eventArg.pos, cardTargetPos, true, deck, player, new System.Action(() =>
                    {
                        //Debug.Log("here");
                        playerStackView.updateCardView();
                        deckStackView.updateCardView();
                        playerTurnFine = true;
                        cardchoose = false;
                    }
                    ));




                }

                else
                {
                    notification = "this move is not possible";
                    notify(notification);
                                    // Debug.Log(notification);

                    cardchoose = false;

                }

            }
        }
    }
    void giveMeACard(object sender, StackEventArg eventArg)
    {
        if (cardLeft.has(eventArg.cardIndex)&&!cardgive) {
            if (!playerSelectCardAfterWild)
            {
                if ((shift == turn.Player) && (playerTurnFine == false))
                {
                    cardgive = true;
                    // Debug.Log("bede kart");
                    //  int temp = cardLeft.pop(eventArg.cardIndex);
                    Vector3 cardTargetPos = gameScript.playerTransform.position;// playerPos + new Vector3(-playerStackView.offset * player.cardCount, 0, 0);
                    dealCard(eventArg.cardIndex, eventArg.pos, cardTargetPos, true, player, cardLeft, new System.Action(() =>
                    {
                       // Debug.Log("now");
                        cardLeftView.updateCardView();
                        playerStackView.updateCardView();
                        playerTurnFine = true;
                        cardgive = false;
                    }));

                }
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
        // Debug.Log("here");
        //rival.push(cardLeft.pop(22));
        //rival.push(cardLefst.pop(21));
        //rival.push(cardLeft.pop(20));
        ////  rival.push(cardLeft.pop(13));
        //player.push(cardLeft.pop(12));
        //player.push(cardLeft.pop(13));
    }
    public override void startTheGame()
    {
        cardLeft.createDeck();

    }
    void play(turn turn)
    {
        string notification = "";
        // Debug.Log("yo"+ (turn == turn.Rival));
        if (turn == turn.Rival && Time.realtimeSinceStartup - rivalMoved > 1.5f&&  AIWork!=true)
        {
            if (player.cardCount == 1 && !player_uno)
            {
                resource.PlayOneShot(uno);
                converseShift();
                Draw();
                Draw();
                converseShift();
                notification = "you forgot Uno";
                                    notify(notification);

                // Debug.Log(notification);
                
            }
            else if (isWildCardOn(deck.playingCard))
            {
                AIWork = true;
                dealCard(-2, rivalPos, deckPos, true, deck, rival, new System.Action(() =>
                {
                    rival_uno = false;
                    notification = "rival played two cards";
                    notify(notification);
                                    // Debug.Log(notification);

                    if (!isWildCardOn(deck.playingCard))
                    {
                        checkCard(deck.playingCard);
                        converseShift();
                        rivalStackView.updateCardView();
                        deckStackView.updateCardView();

                        // Debug.Log(1 + "r did");
                        rivalMoved = Time.realtimeSinceStartup;
                    }
                    AIWork = false;
                }
                ));
                
            }
            else
            
            AIchooseCard();
               
            if (rival.cardCount == 1)
            {
                if (Random.Range(0, 2) == 0)
                {
                    rivalMoved = Time.realtimeSinceStartup;
                    resource.PlayOneShot(uno);
                    rival_uno = true;
                    notification = "she said Uno!";
                    notify(notification);
                // Debug.Log(notification);
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
                    notification = "";
                                    // Debug.Log(notification);

                    playerTurnFine = false;
                    checkCard(deck.playingCard); // if wild play one card
                    converseShift();
                    // Debug.Log("p did");
                    rivalMoved = Time.realtimeSinceStartup;
                }
            }
        }
                   
                            InfoText1.text = ( shift == turn.Player ? "your Turn" : "wait!");


        // if(turn!=shift)
        //  play();
        
    }

    void AIchooseCard()
    {
        if (!AIWork)
        {
            foreach (int i in rival.GetCards())
            {
                if (isCompatible(deck.playingCard, i))
                {
                    AIWork = true;
                    dealCard(i, rivalPos, deckPos, true, deck, rival, new System.Action(() =>
                    {
                        rivalStackView.updateCardView();
                        deckStackView.updateCardView();

                        rival_uno = false;
                        notification = "";
                                        // Debug.Log(notification);

                        if (!isWildCardOn(deck.playingCard))
                        {
                            checkCard(deck.playingCard);
                            converseShift();
                            //rivalStackView.updateCardView();
                            // Debug.Log(2 + "r did");

                            AIWork = false;
                            rivalMoved = Time.realtimeSinceStartup;
                            return;
                        }
                        else
                        {
                            // Debug.Log(3 + "r did");
                            AIWork = false;
                            return;
                        }
                    }));
                    // rival.pop(i);
                    // Debug.Log("line315 rival pushed:" + i);
                    // rivalStackView.updateCardView();
                    return;
                }


            }

            Draw();
            notification = "she draw a card";
                            // Debug.Log(notification);

            converseShift();

            // Debug.Log("she took one card");



            rivalMoved = Time.realtimeSinceStartup;
            notify(notification);
            //   return -1;
        }


    }


}