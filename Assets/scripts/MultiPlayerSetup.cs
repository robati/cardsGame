using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class MultiPlayerSetup :MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    bool isConnecting;
    public Text log;
    void Start()
    {
    //     	LogFeedback("Connecting...");
							isConnecting = true;

	// 			// #Critical, we must first and foremost connect to Photon Online Server.
				PhotonNetwork.ConnectUsingSettings();
    //             PhotonNetwork.GameVersion = this.gameVersion;
    }
public void a(string text){//TODO:Add diconnect to game scene
if(log!= null)
    log.text = text;
}
 public override void OnConnectedToMaster()
		{
            // we don't want to do anything if we are not attempting to join a room. 
			// this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
			// we don't want to do anything.
			if (isConnecting)
			{
				Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");
                a("OnConnectedToMaster");
				// #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
				PhotonNetwork.JoinRandomRoom();
			}
		}

        		public override void OnDisconnected(DisconnectCause cause)
		{
			// LogFeedback("<Color=Red>OnDisconnected</Color> "+cause);
			Debug.Log("PUN Basics Tutorial/Launcher:Disconnected");
            a("OnDisconnected");
			// #Critical: we failed to connect or got disconnected. There is not much we can do. Typically, a UI system should be in place to let the user attemp to connect again.
			// loaderAnime.StopLoaderAnimation();

			isConnecting = false;
			// controlPanel.SetActive(true);

		}

        public override void OnJoinRandomFailed(short returnCode, string message)
		{
			Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            a("OnJoinRandomFailed");
			// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
			PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2});
		}
        
        public override void OnJoinedRoom()
		{
			Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");

			// #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
			if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
			{
				Debug.Log("We load the 'Room for 1' ");
                a("OnJoinedRoom, wait");				

			}
            else{
                controlMenu.GAMETYPE = GameState.MultiPlayer;
                PhotonNetwork.LoadLevel("cardTestonDeck");
            }
		}
        public override void OnPlayerEnteredRoom( Player other  )
        {
            controlMenu.GAMETYPE = GameState.MultiPlayer;
            a("wait is over");
            PhotonNetwork.LoadLevel("cardTestonDeck");


          }
    // Update is called once per frame
    void Update()
    {
        
    }
}
