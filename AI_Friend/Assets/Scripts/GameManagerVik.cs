using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerVik : Photon.MonoBehaviour
{

    // this is a object name (must be in any Resources folder) of the prefab to spawn as player avatar.
    // read the documentation for info how to spawn dynamically loaded game objects at runtime (not using Resources folders)




    // 여기를 잘 적용하면 랜덤하게 캐릭터를 바꿀 수 있겠네...

    public string playerPrefabName = "Charprefab";

    //public GameObject[] Charprefab; //스폰


    // 여기를 잘 적용하면 랜덤하게 캐릭터를 바꿀 수 있겠네 여기까지.....




    void OnJoinedRoom()
    {

        StartGame();
    }

    IEnumerator OnLeftRoom()
    {
        //Easy way to reset the level: Otherwise we'd manually reset the camera
        //Wait untill Photon is properly disconnected (empty room, and connected back to main server)
        while (PhotonNetwork.room != null || PhotonNetwork.connected == false)
            yield return 0;

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }



    void StartGame()
    {



        Camera.main.farClipPlane = 1000; //Main menu set this to 0.4 for a nicer BG    

        //prepare instantiation data for the viking: Randomly diable the axe and/or shield
        bool[] enabledRenderers = new bool[2];
        //enabledRenderers[0] = Random.Range(0, 2) == 0;//Axe
        //enabledRenderers[1] = Random.Range(0, 2) == 0; ;//Shield

        object[] objs = new object[1]; // Put our bool data in an object array, to send
        objs[0] = enabledRenderers;




        // 여기를 잘 적용하면 랜덤하게 캐릭터를 바꿀 수 있겠네...



        // Spawn our local player
        //PhotonNetwork.Instantiate(playerPrefabName, transform.position, Quaternion.identity, 0, objs);

        int chrand = Random.Range(1, 6); //랜덤으로 뽑은(1, 3) int 형 - 1~2 까지 float형 - 0~3 까지의 범위를 나타낸다.

        PhotonNetwork.Instantiate(playerPrefabName + chrand, transform.position, Quaternion.identity, 0, objs);

        // 여기를 잘 적용하면 랜덤하게 캐릭터를 바꿀 수 있겠네 여기까지.....


    }




    void OnGUI()
    {
        GUIStyle btStyle = new GUIStyle(GUI.skin.button);
        btStyle.fontSize = 45;
        btStyle.normal.textColor = Color.white;
        btStyle.onActive.textColor = Color.yellow;
        btStyle.normal.background = Texture2D.grayTexture;

        if (PhotonNetwork.room == null) return; //Only display this GUI when inside a room

        GUILayout.BeginArea(new Rect(40, 40, 200, 100));
        if (GUILayout.Button("나가기", btStyle))
        {
            PhotonNetwork.LeaveRoom();
        }
        GUILayout.EndArea();
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("OnDisconnectedFromPhoton");
    }
}
