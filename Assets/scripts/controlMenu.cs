using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(cardStack))]

public class controlMenu : MonoBehaviour
{

   
    public static GameState GAMETYPE = GameState.Opening;
   
    
    
   
    public Sprite mainMenuBG;

    SpriteRenderer backImg;



 public void GotoMenu(int gameState){
        switch (gameState)
        {
            case (int)GameState.Opening:  
                // backImg.sprite = mainMenuBG;
                break;
            case  (int)GameState.tut: 
                // showRules();
                break;
            case  (int)GameState.OnePlayer:   
            
                GAMETYPE = GameState.OnePlayer;
                 SceneManager.LoadScene(1);
                // AI.NewGame();
                // AI.play();
                // backImg.sprite = gameBG;
                // menu.SetActive(false);

                break;
            case  (int)GameState.MultiPlayer:    
            
                GAMETYPE = GameState.MultiPlayer;
                                 SceneManager.LoadScene(1);

                // Human.NewGame();      
                // Human.play();
                // backImg.sprite = gameBG;
                // menu.SetActive(false);
                // break;
            
                //case GameState.GameOver:        //one of the players has won
                //    DrawGameOver();
                   break;
        }
    }

 
           // helpClicked = Time.realtimeSinceStartup;
            // NewGame(Human);      //call to initialize the new game
           // gameState = GameState.tut;      //change the game state to start the game
                                                    // BackgroundImage.color = new Color(100, 0, 0,0.5f);


    //BUG:  uno sound but no info ui
//     void DrawOpening()
//     {
//         float drUnit = heightScreen / 5;
//         float wdrUnit = widthScreen / 5;

//         GUI.skin.button.fontSize = (int)drUnit / 5;
//         GUI.skin.label.fontSize = (int)drUnit / 5;
//         Rect groupRect = new Rect(widthScreen/2-wdrUnit/2, heightScreen / 2-drUnit*2 , wdrUnit*2, heightScreen/2+2*drUnit);
//         GUI.BeginGroup(groupRect);

//         //draw the title image
//         Rect titleRect = new Rect(0, 0, wdrUnit, drUnit);
//         GUI.DrawTexture(titleRect, titleImage);

//         //draw a button for starting a new game
//         Rect oneplayerRect = new Rect(0, drUnit*4/3, wdrUnit, drUnit * 2 / 3);
//         GUI.DrawTexture(oneplayerRect, onePlayerImage);
//         if (GUI.Button(oneplayerRect, "One Player"))
//         {
           
//             AI.NewGame();      //call to initialize the new game
//             gameState = GameState.OnePlayer;      //change the game state to start the game
//         }
//         Rect multiplayerRect = new Rect(0, drUnit*6/3, wdrUnit, drUnit*2/3);
//         GUI.DrawTexture(multiplayerRect, MultPlayerImage);
//         //GUI.Button()
//         if (GUI.Button(multiplayerRect, "Multi Player"))
//         {
//             Human.NewGame();      //call to initialize the new game
//             gameState = GameState.MultiPlayer;      //change the game state to start the game
//         }
//         Rect tutorial = new Rect(0, drUnit*8/3, wdrUnit, drUnit * 2 / 3);
//         GUI.DrawTexture(tutorial, HTPImage);
//         if (GUI.Button(tutorial, "How to play"))
//         {
//             helpClicked = Time.realtimeSinceStartup;
//             // NewGame(Human);      //call to initialize the new game
//             gameState = GameState.tut;      //change the game state to start the game
//                                                     // BackgroundImage.color = new Color(100, 0, 0,0.5f);
//         }
//         Rect quit = new Rect(0, drUnit * 10 / 3, wdrUnit, drUnit * 2 / 3);
//         GUI.DrawTexture(quit, quitImage);
//         if (GUI.Button(quit, "exit"))
//         {
// #if UNITY_EDITOR
//             UnityEditor.EditorApplication.isPlaying = false;
// #endif
//             Application.Quit();
//         }
//         GUI.EndGroup();		//close the group

       
//     }
    // void showRules()
    // {
    //     if(rulepage==0)
    //     backImg.sprite = rule1;
    //     else
    //         backImg.sprite = rule2;
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         if (Time.realtimeSinceStartup - helpClicked > 0.5f)
    //         {
    //             helpClicked = Time.realtimeSinceStartup;
    //             rulepage++;
    //         }
            
    //     }
    //     if (rulepage == 2)
    //     {
    //         gameState = GameState.Opening;
    //         rulepage = 0;
    //     }
    // }

   
  
}