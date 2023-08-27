
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


abstract class abstarctPlay : MonoBehaviour
{
    protected bool cardchoose = false;
    protected bool cardgive = false;
    public controlWorld gameScript;
    public GameObject fab;
    // public AudioClip shufflingAudio;
    public AudioSource shufflingAudio;

    public Text TurnText;
    public Text InfoText;
    public Text InfoText1;
    public GameObject Panel;

    protected enum turn { Player, Rival };
    protected enum Action
    {
        SkipI = 10, SkipII = 24, SkipIII = 38, SkipIV = 52,
        ReverseI = 11, ReverseII = 25, ReverseIII = 53, ReverseIV = 39,
        DrawtwoI = 12, DrawtwoII = 26, DrawtwoIII = 54, DrawtwoIV = 40,
        WildI = 13, WildII = 27, WildIII = 55, WildIV = 41
    };
    //protected GameObject playerGameObject;
    //protected GameObject rivalGameObject;
    public bool MoveCardAnimate = false;
    public float speed = 2f;
    GameObject selectedCard;
    protected Vector3 playerPos;
    protected Vector3 rivalPos;
    protected Vector3 deckPos;
    protected cardStack cardLeft;
    protected cardStackView cardLeftView;
    protected cardStack player;
    protected cardStack rival;
    protected turn shift = turn.Player;
    protected cardStack deck;
    protected deckPile deckStackView;
    protected cardStackView playerStackView;
    protected cardStackView rivalStackView;
    protected string notification = "Hi!";
    protected bool playerTurnFine = false;
    protected float rivalMoved = 0f;
    protected int lastAction = 0;
    protected bool playerSelectCardAfterWild = false;
    protected bool player_uno = false;
    protected bool rival_uno = false;
    protected bool paused = false;
    protected bool boolGameOver = false;
    protected Rect menu;
    protected string menuInfo="";
    protected Rect pauseMenuRect;
    protected AudioSource resource;
    protected Texture2D contImg;
    protected Texture2D resetImg;
    protected Texture2D goBackImg;
    public  AudioClip winning;
    protected string lastNotif = "";
    abstract public void play();
    public abstract void startPlay();//this is the start method
    public abstract void distributeCard();
    public abstract void startTheGame();
    public abstract void playerMoved(object sender,CardEventArg args);
    private void Awake() {
        gameScript = GetComponent<controlWorld>();
        playerPos = gameScript.playerStackView.start;
        
        deck = gameScript.deck;
      //  deckPos = gameScript.deckGameObject.GetComponent<deckPile>().place;
        deckPos = gameScript.deckTransform.position;
        cardLeftView = gameScript.cardLeftView;
        cardLeft = gameScript.cardLeft;
        player = gameScript.player;
        playerStackView = gameScript.playerStackView;

        rival = gameScript.rival;
        rivalStackView = gameScript.rivalStackView;
        deckStackView = gameScript.deckStackView;
        rivalPos = gameScript.rivalTransform.position; //rivalStackView.start;
        playerStackView.setAsPlayerCard(true);
        menu = new Rect(Screen.width*9/10 , Screen.height * 9/10, Screen.width/10, Screen.height/10);
        pauseMenuRect = new Rect(0, 0, Screen.width, Screen.height);
        resource = GetComponent<AudioSource>();

        contImg = gameScript.onePlayerImage;
        resetImg = gameScript.MultPlayerImage;
        goBackImg = gameScript.quitImage;

        controlWorld.GAMETYPE = GameState.MultiPlayer;
        

    }
    //     private void Update() {
    //     // play();
    //     InfoText.text = notification;
    //     TurnText.text = menuInfo;
    // }
    public void NewGame()
    {
        Debug.Log("stattt"+controlWorld.GAMETYPE);
        // resource.PlayOneShot(shufflingAudio);
        shufflingAudio.Play();

        startTheGame();
        distributeCard();
        startPlay();
        Panel.SetActive(false);
    }
    protected void resetPlay()
    {
        player.resetStack();
        rival.resetStack();
        cardLeft.resetStack();
        deck.resetStack();
        menuInfo = "";
        paused = false;
        playerTurnFine = false;
        rivalMoved = 0f;
        lastAction = 0;
        playerSelectCardAfterWild = false;
        player_uno = false;
        rival_uno = false;
  

}

    protected bool isCompatible(int A, int B)// B usually is the new card.
    {
        int aRange = A / 14;
        int aNumber = A % 14;


        // Debug.Log("firstCard:" + aRange + " #" + aNumber);

        int bRange = B / 14;
        int bNumber = B % 14;

        // Debug.Log("newCard:" + bRange + " #" + bNumber);
        return aRange == bRange || aNumber == bNumber || isWildCardOn(B) || isWildCardOn(A);
    }
  
    protected void checkCard(int cardOn)
    {
        // Debug.Log("card " + cardOn + " is gonna be " + lastAction);
        if (lastAction != cardOn)
        {
            if (cardOn == (int)Action.SkipI || cardOn == (int)Action.SkipII || cardOn == (int)Action.SkipIII || cardOn == (int)Action.SkipIV)
            {
                // Debug.Log("skip");

                notification = "skip, play again";

                converseShift();
                lastAction = cardOn;
            }
            if (cardOn == (int)Action.ReverseI || cardOn == (int)Action.ReverseII || cardOn == (int)Action.ReverseIII || cardOn == (int)Action.ReverseIV)
            {
                // Debug.Log("Reverse");

                notification = "Reverse, play again ";

                converseShift();
                lastAction = cardOn;
            }
            if (cardOn == (int)Action.DrawtwoI || cardOn == (int)Action.DrawtwoII || cardOn == (int)Action.DrawtwoIII || cardOn == (int)Action.DrawtwoIV)
            {
                // Debug.Log("Draw Two");

                notification = "Draw Two, play again";
                converseShift();
                Draw();
                Draw();
                lastAction = cardOn;
            }
            notify(notification);
        }
    }

  
    protected bool isWildCardOn(int cardOn)
    {
        if (cardOn == (int)Action.WildI || cardOn == (int)Action.WildII || cardOn == (int)Action.WildIII || cardOn == (int)Action.WildIV)
        {
            return true;
        }
        return false;

    }
    protected bool isDrawCardOn(int cardOn)
    {
        if (cardOn == (int)Action.DrawtwoI || cardOn == (int)Action.DrawtwoII || cardOn == (int)Action.DrawtwoIII || cardOn == (int)Action.DrawtwoIV)
        {
            return true;
        }
        return false;

    }


    /*protected void drawPauseMenu(int windowID)
    {
        float drUnit = Screen.height / 7;
        float wdrUnit = Screen.width /5;
        float width = wdrUnit;
        float dist = 0;
        float height = drUnit;
        float posx = Screen.width / 2 - width / 2;
        float posy = Screen.height / 2 - (2 * height - height / 2);
        GUI.Label(new Rect(posx, posy, Screen.width / 2, 100), menuInfo);

        if (menuInfo == "")
        {
            Rect contRect = new Rect(posx, posy + dist, width, height);
            GUI.DrawTexture(contRect, contImg);
            if (GUI.Button(contRect, "continue"))
            {
                paused = false;
                print("Got a click");
            }
        }
        else
        {
            resource.PlayOneShot(winning);
        }
        Rect resetRect = new Rect(posx, posy + height + dist, width, height);
        GUI.DrawTexture(resetRect, resetImg);
        if (GUI.Button(resetRect, "restart"))
        {

            print("Got a click");
            resetPlay();
            NewGame();
            paused = false;
        }
        Rect goBackRect = new Rect(posx, posy + 2 * height + dist, width, height);
        GUI.DrawTexture(goBackRect, goBackImg);
        if (GUI.Button(goBackRect, "main menu"))
        {
            resetPlay();
            gameScript.gameState = GameState.Opening;
            paused = false;
            print("Got a click");
        }
    }*/
    protected  void Draw()//TODO:m add onfinnished
    {
        //int cardIdx = 0;
        int cardID = cardLeft.GetaCard();
        Vector3 cardPos = cardLeftView.GetCardPosition(cardID);
        Debug.Log("Card" + cardPos);
        if (shift == turn.Player)
        {
            //player.push(cardLeft.pop());

            dealCard(cardID, cardPos, gameScript.playerTransform.position, true, player, cardLeft, new System.Action(() =>
                 {
                     playerStackView.updateCardView();
                     cardLeftView.updateCardView();
                 }));
        }
        else
        {
            //  rival.push(cardLeft.pop());
            dealCard(cardID, cardPos, gameScript.rivalTransform.position, false, rival, cardLeft, new System.Action(() =>
            {
                rivalStackView.updateCardView();
                cardLeftView.updateCardView();
            }));
        }
    }
    public  void converseShift()
    {
        if (shift == turn.Player)
        {
            shift = turn.Rival;
            // Debug.Log("T = Rival");
        }
        else
        {
            shift = turn.Player;
            // Debug.Log("T= Player");
        }

    }
    protected void dealCard(int idx,Vector3 pos,Vector3 cardTargetPos, bool cardOn,cardStack pushStack,cardStack popStack,  System.Action fin)
    {
        int temp = popStack.pop(idx);

        GameObject card = Instantiate<GameObject>(fab,GameObject.FindGameObjectWithTag("canvas").transform);
        c1 cardModel = card.GetComponent<c1>();
        card.transform.position = pos;
        cardModel.cardIndex = temp;
        cardModel.played += playerMoved;
        cardModel.toggleFace(cardOn);

        playerSelectCardAfterWild = false;//why is it true!? change it in call back
        //rivalSelectCardAfterWild = false;
        //MoveCardAnimate = true;
        

        //cardAnimator cardAnime = card.GetComponent<cardAnimator>();
       // cardTargetPos.z = -2;
            pushStack.push(temp);

        //  moveAnimate(card);//TODO: Temp
        Debug.Log(card.transform.position+" "+cardTargetPos.x+" "+cardTargetPos.y+" "+cardTargetPos.z);
        StartCoroutine(MoveCard(card, cardTargetPos, new System.Action(() => { // Debug.Log("you"); })));//, temp, new System.Action(() => {TODO:
                                                                               // cardAnime.MoveCard(cardTargetPos, temp, new System.Action(() => {TODO:
           // popStack.pop(idx);
            Destroy(card);

             fin();
                  
        })));
    }
     public void notify(string note){
        if(note != lastNotif && note != ""){
        InfoText.text = note+"\n..............\n" +  InfoText.text;
        lastNotif = note;
        }
        // Debug.Log(notification);//BUG:when she forgot uno disble when she said uno
 }
    void moveAnimate(GameObject card){
        // float step = speed* Time.deltaTime;
        // Vector3 target = new Vector3(0,0,0);
        card.transform.position = new Vector3(0,0,0);
       // try{
        // card.transform.position = Vector3.MoveTowards(card.transform.position ,target,step);
        // if(Vector3.Distance(card.transform.position ,target)<0.001f){
            // MoveCardAnimate = false;

        // }
        // }
        // catch{
            Debug.Log("yo");
        // }
    }
    IEnumerator MoveCard(GameObject card, Vector3 cardTargetPos, System.Action fin) 
        {
        // float step = speed* Time.deltaTime;
        //
       // cardTargetPos = new Vector3(-350,0,0);
        // card.transform.position = new Vector3(350, 0, 0);
        //  try{
        while (true)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position, cardTargetPos, 0.6f);
           // Debug.Log("ti");
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(cardTargetPos, card.transform.position) < 0.5f) {
                fin();
                 break;
            }
        }
    }
        // private void Update() {
        //     if(MoveCardAnimate){
        //         moveAnimate();
        //     }
        // }
    }
