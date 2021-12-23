using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNickName : MonoBehaviour
{

    string nickName;
    public GameObject textObj;
    private Text textField;

    public GameObject f_Player;
    private GameObject target;



    // Start is called before the first frame update
    void Start()
    {

        textField = textObj.GetComponent<Text>();

        // 닉네임 호출
        nickName = GetComponent<PhotonView>().owner.name;
        print(nickName);
        // 닉네임 호출 여기까지

        textField.text = nickName;

        target = GameObject.FindWithTag("MainCamera");

    }



    // Update is called once per frame
    void Update()
    {
        textObj.transform.forward = target.transform.forward;
    }

}