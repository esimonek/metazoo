using UnityEngine;
using System.Collections;

public class MainMenuVik : MonoBehaviour
{


    private string roomName = "SG Metaverse Room";
    private Vector2 scrollPos = Vector2.zero;

    void Awake()
    {
        //PhotonNetwork.logLevel = NetworkLogLevel.Full;

        //Connect to the main photon server. This is the only IP and port we ever need to set(!)
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings("v1.0"); // version of the game/demo. used to separate older clients from newer ones (e.g. if incompatible)

        //Load name from PlayerPrefs
        PhotonNetwork.playerName = PlayerPrefs.GetString("playerName", "초천재_" + Random.Range(1, 99));

        //Set camera clipping for nicer "main menu" background
        Camera.main.farClipPlane = Camera.main.nearClipPlane + 0.1f;

    }



    void OnGUI()
    {


        //joystic_sh = GameObject.FindWithTag("JJStick");
        //joystic_sh = GameObject.Find("MobileSingleStickControl");



        if (!PhotonNetwork.connected)
        {
            ShowConnectingGUI();
            return;   //Wait for a connection
        }


        if (PhotonNetwork.room != null)
            return; //Only when we're not in a Room


        GUIStyle labelStyle1 = new GUIStyle(GUI.skin.label);
        labelStyle1.fontSize = 50;
        labelStyle1.normal.textColor = Color.black;

        GUIStyle btStyle = new GUIStyle(GUI.skin.button);
        btStyle.fontSize = 50;
        btStyle.normal.textColor = Color.white;
        btStyle.onActive.textColor = Color.yellow;
        btStyle.normal.background = Texture2D.grayTexture;

        GUIStyle tfStyle = new GUIStyle(GUI.skin.textField);
        tfStyle.fontSize = 50;
        tfStyle.normal.textColor = Color.white;
        tfStyle.onActive.textColor = Color.yellow;
        tfStyle.normal.background = Texture2D.grayTexture;



        GUILayout.BeginArea(new Rect((Screen.width - 1200) / 2, (Screen.height - 650) / 2, 1200, 650));

        //GUILayout.Label("Main Menu");

        //Player name
        GUILayout.BeginHorizontal();
        GUILayout.Label("사용자명:", labelStyle1, GUILayout.Width(330));

        PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName, tfStyle);
        if (GUI.changed)//Save name
            PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);

        GUILayout.EndHorizontal();
        GUILayout.Space(35);



        //Create a room (fails if exist!)
        GUILayout.BeginHorizontal();
        GUILayout.Label("방개설:", labelStyle1, GUILayout.Width(330));
        roomName = GUILayout.TextField(roomName, tfStyle);

        if (GUILayout.Button("방개설", btStyle))
        {
            // using null as TypedLobby parameter will also use the default lobby
            PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 10 }, TypedLobby.Default);



            //joystic_sh.SetActive(true);



        }

        GUILayout.EndHorizontal();


        /*

        GUILayout.Space(15);




        //Join room by title
        GUILayout.BeginHorizontal();
        GUILayout.Label("방입장:", labelStyle1, GUILayout.Width(330));


        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("개설된 방이 없습니다.", labelStyle1);
        }
        else
        {
            roomName = GUILayout.TextField(roomName, tfStyle);
            if (GUILayout.Button("입장", btStyle))
            {
                PhotonNetwork.JoinRoom(roomName);



                joystic_sh.SetActive(true);


            }
        }

        
        GUILayout.EndHorizontal();
        */



        GUILayout.Space(35);



        //Join random room

        if (PhotonNetwork.GetRoomList().Length > 0)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("랜덤 방 입장:", labelStyle1, GUILayout.Width(330));


            if (GUILayout.Button("입장", btStyle))
            {
                PhotonNetwork.JoinRandomRoom();

            }

            GUILayout.EndHorizontal();
            GUILayout.Space(60);
        }






        GUILayout.BeginHorizontal();
        GUILayout.Label("열린 방 목록:", labelStyle1, GUILayout.Width(330));
        GUILayout.Space(20);

        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("개설된 방이 없습니다.", labelStyle1);
        }
        GUILayout.EndHorizontal();



        if (PhotonNetwork.GetRoomList().Length != 0)
        {
            //Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(game.Name + " " + game.PlayerCount + "/" + game.MaxPlayers, labelStyle1);
                if (GUILayout.Button("입장", btStyle))
                {
                    PhotonNetwork.JoinRoom(game.Name);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }

        GUILayout.EndArea();
    }



    void ShowConnectingGUI()
    {

        GUIStyle labelStyle2 = new GUIStyle(GUI.skin.label);
        labelStyle2.fontSize = 70;
        labelStyle2.normal.textColor = Color.black;

        GUILayout.BeginArea(new Rect((Screen.width - 800) / 2, (Screen.height - 400) / 2, 900, 400));

        GUILayout.Label("     개발에 개짜도 모르는", labelStyle2);
        GUILayout.Space(10);
        GUILayout.Label("초천재가 만든 메타버스월드", labelStyle2);
        GUILayout.Space(50);
        GUILayout.Label("     서버에 접속중입니다.", labelStyle2);

        GUILayout.EndArea();
    }



    public void OnConnectedToMaster()
    {
        // this method gets called by PUN, if "Auto Join Lobby" is off.
        // this demo needs to join the lobby, to show available rooms!

        PhotonNetwork.JoinLobby();  // this joins the "default" lobby
    }
}
