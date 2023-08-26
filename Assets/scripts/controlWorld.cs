using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(cardStack))]

public class controlWorld : MonoBehaviour
{

    public GameState gameState = GameState.Opening;
    public static GameState GAMETYPE = GameState.Opening;
    public Texture2D titleImage;
    public Texture2D onePlayerImage;
    public Texture2D MultPlayerImage;
    public Texture2D HTPImage;
    public Texture2D quitImage;
    public GameObject playerGameObject;
    public GameObject rivalGameObject;
    public GameObject deckGameObject;
    
    public SpriteRenderer BackgroundImage;
    playAI  AI;
    playMulti Human;
    public cardStack cardLeft;
    public cardStackView cardLeftView;
    public cardStack player;
    public cardStack rival;
    public cardStack deck;

    public cardStackView playerStackView;
    public cardStackView rivalStackView;

    public GameObject background;
    public Sprite gameBG;
    public Sprite mainMenuBG;
    public Sprite rule1;
    public Sprite rule2;
    int rulepage = 0;
    float helpClicked;
    float widthScreen;
    float heightScreen;

    public Transform deckTransform;
    public Transform playerTransform;
    public Transform rivalTransform;

    SpriteRenderer backImg;

    public GameObject menu;

    void Awake()
    {
        //    card = card.GetComponent<c1>();
        AI= GetComponent<playAI>();
        Human = GetComponent<playMulti>();
       cardLeft = GetComponent<cardStack>();
        cardLeftView = GetComponent<cardStackView>();
        
        player = playerGameObject.GetComponent<cardStack>();
        playerStackView = playerGameObject.GetComponent<cardStackView>();

        rival= rivalGameObject.GetComponent<cardStack>();
        rivalStackView = rivalGameObject.GetComponent<cardStackView>();

        backImg = background.GetComponent<SpriteRenderer>();
        deck = deckGameObject.GetComponent<cardStack>();
        widthScreen = Screen.width;
        heightScreen = Screen.height;

    }
    private void Start() {
                      //  controlMenu.GAMETYPE = GameState.MultiPlayer;

    
        Debug.Log("sttttt"+controlMenu.GAMETYPE);
        switch (controlMenu.GAMETYPE)
        {
            case GameState.OnePlayer:   
            
                GAMETYPE = GameState.OnePlayer;
                AI.NewGame();
                AI.play();
                backImg.sprite = gameBG;
                menu.SetActive(false);

                break;
            case GameState.MultiPlayer:    
            
                GAMETYPE = GameState.MultiPlayer;
                Human.NewGame();      
                Human.play();
                backImg.sprite = gameBG;
                menu.SetActive(false);
                break;
        }
        

    }
 public void GotoMenu(int gameState){
        switch (gameState)
        {
            case (int)GameState.Opening:     //the opening menu screen
                // DrawOpening();
                backImg.sprite = mainMenuBG;
                break;
            case  (int)GameState.tut:          //the how to play screen
                showRules();
                break;
            case  (int)GameState.OnePlayer:   
            
                GAMETYPE = GameState.OnePlayer;
                AI.NewGame();
                AI.play();
                backImg.sprite = gameBG;
                menu.SetActive(false);

                break;
            case  (int)GameState.MultiPlayer:    
            
                GAMETYPE = GameState.MultiPlayer;
                Human.NewGame();      
                Human.play();
                backImg.sprite = gameBG;
                menu.SetActive(false);
                break;
            
                //case GameState.GameOver:        //one of the players has won
                //    DrawGameOver();
                //    break;
        }
    }

 
           // helpClicked = Time.realtimeSinceStartup;
            // NewGame(Human);      //call to initialize the new game
           // gameState = GameState.tut;      //change the game state to start the game
                                                    // BackgroundImage.color = new Color(100, 0, 0,0.5f);


    //BUG:  uno sound but no info ui
    void DrawOpening()
    {
        float drUnit = heightScreen / 5;
        float wdrUnit = widthScreen / 5;

        GUI.skin.button.fontSize = (int)drUnit / 5;
        GUI.skin.label.fontSize = (int)drUnit / 5;
        Rect groupRect = new Rect(widthScreen/2-wdrUnit/2, heightScreen / 2-drUnit*2 , wdrUnit*2, heightScreen/2+2*drUnit);
        GUI.BeginGroup(groupRect);

        //draw the title image
        Rect titleRect = new Rect(0, 0, wdrUnit, drUnit);
        GUI.DrawTexture(titleRect, titleImage);

        //draw a button for starting a new game
        Rect oneplayerRect = new Rect(0, drUnit*4/3, wdrUnit, drUnit * 2 / 3);
        GUI.DrawTexture(oneplayerRect, onePlayerImage);
        if (GUI.Button(oneplayerRect, "One Player"))
        {
           
            AI.NewGame();      //call to initialize the new game
            gameState = GameState.OnePlayer;      //change the game state to start the game
        }
        Rect multiplayerRect = new Rect(0, drUnit*6/3, wdrUnit, drUnit*2/3);
        GUI.DrawTexture(multiplayerRect, MultPlayerImage);
        //GUI.Button()
        if (GUI.Button(multiplayerRect, "Multi Player"))
        {
            Human.NewGame();      //call to initialize the new game
            gameState = GameState.MultiPlayer;      //change the game state to start the game
        }
        Rect tutorial = new Rect(0, drUnit*8/3, wdrUnit, drUnit * 2 / 3);
        GUI.DrawTexture(tutorial, HTPImage);
        if (GUI.Button(tutorial, "How to play"))
        {
            helpClicked = Time.realtimeSinceStartup;
            // NewGame(Human);      //call to initialize the new game
            gameState = GameState.tut;      //change the game state to start the game
                                                    // BackgroundImage.color = new Color(100, 0, 0,0.5f);
        }
        Rect quit = new Rect(0, drUnit * 10 / 3, wdrUnit, drUnit * 2 / 3);
        GUI.DrawTexture(quit, quitImage);
        if (GUI.Button(quit, "exit"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
        GUI.EndGroup();		//close the group

       
    }
    void showRules()
    {
        if(rulepage==0)
        backImg.sprite = rule1;
        else
            backImg.sprite = rule2;
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.realtimeSinceStartup - helpClicked > 0.5f)
            {
                helpClicked = Time.realtimeSinceStartup;
                rulepage++;
            }
            
        }
        if (rulepage == 2)
        {
            gameState = GameState.Opening;
            rulepage = 0;
        }
    }

   
  
}