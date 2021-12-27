using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerContChar : MonoBehaviour
{

    private CharacterController _charController;
    private managerJoystick _mngrJoyStick;


    private Transform meshPlayer;

    private float inputX;
    private float inputZ;

    private Vector3 v_movement;
    private float moveSpeed;
    private float gravity;

    private float tempPlayer;

    //카메라~~
    private GameObject cameraObj;


    //private Transform mainCameraTransform = null;



    // Start is called before the first frame update
    void Start()
    {

        moveSpeed = 0.1f;
        gravity = 0.5f;


        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        meshPlayer = tempPlayer.transform;
        _charController = tempPlayer.GetComponent<CharacterController>();


        // 카메라 초기세팅
        cameraObj = GameObject.Find("CameraAnchor");
        cameraObj.transform.position = meshPlayer.position; /* 카메라 초기 위치 */

        ////mainCameraTransform = Camera.main.transform;
        //mainCameraTransform = cameraObj.transform;



        _mngrJoyStick = GameObject.Find("ImgJoystickBg").GetComponent<managerJoystick>();

    }

    // Update is called once per frame
    void Update() 
    {

        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        inputX = _mngrJoyStick.inputHorizontal();
        inputZ = _mngrJoyStick.inputVertical();

    }

    private void FixedUpdate()
    {

        //Vector3 forward = mainCameraTransform.forward;
        //Vector3 right = mainCameraTransform.right;


        // 카메라 따라다니게 하는 거
        if (tempPlayer != null && cameraObj != null)
        {
            //Debug.Log(rb_Player.transform.position);
            cameraObj.transform.position = meshPlayer.position;
        }
        // 카메라 따라다니게 하는 거 여기까지



        if (_charController.isGrounded)
        {
            v_movement.y = 0f;
        }
        else
        {
            v_movement.y -= gravity * Time.deltaTime;
        }

        v_movement = new Vector3(inputX * moveSpeed, 0, inputZ * moveSpeed);
        _charController.Move(v_movement);

        if (inputX != 0 || inputZ != 0)
        {
            Vector3 lookDir = new Vector3(v_movement.x, 0, v_movement.z);
            meshPlayer.rotation = Quaternion.LookRotation(lookDir);

            //Vector3 desireMoveDirection = (forward * inputZ + right * inputX).normalized;
            //meshPlayer.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desireMoveDirection), 3f);

        }

    }
}
