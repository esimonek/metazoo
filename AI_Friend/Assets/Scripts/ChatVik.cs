using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This simple chat example showcases the use of RPC targets and targetting certain players via RPCs.
/// </summary>
public class ChatVik : Photon.MonoBehaviour
{

    public static ChatVik SP;
    public List<string> messages = new List<string>();

    private int chatHeight = (int)161;
    private Vector2 scrollPos = Vector2.zero;
    private string chatInput = "";
    private float lastUnfocusTime = 0;

    void Awake()
    {
        SP = this;
    }

    void OnGUI()
    {
        //Chat input

        GUIStyle labelStyle1 = new GUIStyle(GUI.skin.label);
        labelStyle1.fontSize = 35;
        labelStyle1.normal.textColor = Color.black;

        GUIStyle btStyle = new GUIStyle(GUI.skin.button);
        btStyle.fontSize = 45;
        btStyle.normal.textColor = Color.white;
        btStyle.onActive.textColor = Color.yellow;
        btStyle.normal.background = Texture2D.grayTexture;

        GUIStyle tfStyle = new GUIStyle(GUI.skin.textField);
        tfStyle.fontSize = 45;
        tfStyle.normal.textColor = Color.white;
        tfStyle.onActive.textColor = Color.yellow;
        tfStyle.normal.background = Texture2D.grayTexture;



        GUI.SetNextControlName("");



        GUILayout.BeginArea(new Rect((Screen.width - 750) / 2, Screen.height - chatHeight - 90, 750, chatHeight));

        //Show scroll list of chat messages
        scrollPos = GUILayout.BeginScrollView(scrollPos, GUIStyle.none, GUIStyle.none);
        //GUI.color = Color.red;
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            GUILayout.Label(messages[i], labelStyle1);
        }
        GUILayout.EndScrollView();




        GUILayout.BeginHorizontal();
        GUI.SetNextControlName("ChatField");
        chatInput = GUILayout.TextField(chatInput, tfStyle, GUILayout.MaxWidth(610));


        if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')
        {
            if (GUI.GetNameOfFocusedControl() == "ChatField")
            {
                SendChat(PhotonTargets.All);
                lastUnfocusTime = Time.time;
                GUI.FocusControl("");
                GUI.UnfocusWindow();
            }

            else
            {
                if (lastUnfocusTime < Time.time - 0.1f)
                {
                    GUI.FocusControl("ChatField");
                }
            }
        }


        // Send 버튼
        //if (GUILayout.Button("SEND", GUILayout.Height(30)))
        if (GUILayout.Button("전송", btStyle, GUILayout.Width(130)))
            SendChat(PhotonTargets.All);




        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();



        GUILayout.EndArea();
    }

    public static void AddMessage(string text)
    {
        SP.messages.Add(text);
        if (SP.messages.Count > 15)
            SP.messages.RemoveAt(0);

    }


    [PunRPC]
    void SendChatMessage(string text, PhotonMessageInfo info)
    {
        AddMessage("[" + info.sender + "] " + text);
    }

    void SendChat(PhotonTargets target)
    {
        if (chatInput != "")
        {
            photonView.RPC("SendChatMessage", target, chatInput);
            chatInput = "";
        }
    }

    void SendChat(PhotonPlayer target)
    {
        if (chatInput != "")
        {
            chatInput = "[PM] " + chatInput;
            photonView.RPC("SendChatMessage", target, chatInput);
            chatInput = "";
        }
    }

    void OnLeftRoom()
    {
        this.enabled = false;
    }

    void OnJoinedRoom()
    {
        this.enabled = true;
    }
    void OnCreatedRoom()
    {
        this.enabled = true;
    }
}
