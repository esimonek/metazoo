using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;



public class PlayerController : MonoBehaviour
{

    // For Player Variables

    public float currentMoveSpeed;
    public float moveSpeed;
    public Vector3 velocity;
    public float currentWalkAnimationSpeed;
    public float walkAnimationSpeed;
    public float rotationSpeed;
    public float jumpForce;
    public float runSpeed;
    public float runAnimationSpeed;

    // For Player Component

    private Rigidbody rb_Player;
    //public Animator an_Player;
    private PhotonView pView;

    // 캐릭터 애니호출 - 애니메이션 정의
    public Animator sg_Anim;


    //// 조이스틱 호출
    //private managerJoystick _mngJoystick;


    // For Other GameObjects

    private GameObject main_Player;

    private GameObject cameraObj;
    public GameObject playerMesh;


    //private GameObject controllerSet; //콘트롤러 정의




    // Awake is called before the first frame update
    void Awake()
    {


        //controllerSet = GameObject.Find("ControllerSet"); //콘트롤러 정의




        //애니메이션 정의
        sg_Anim = GetComponent<Animator>();
        //애니메이션 여기까지





        rb_Player = GetComponent<Rigidbody>();

        main_Player = GameObject.FindWithTag("MPLAYER"); // 메인캐릭터 정ㅇ
        //sg_Anim = main_Player.GetComponent<Animator>(); // 캐릭터 애니호출 - 애니메이션 정의- 이건 아님!


        //an_Player = GetComponent<Animator>();
        pView = GetComponent<PhotonView>();


        //cameraObj = Camera.main.gameObject;
        cameraObj = GameObject.Find("CameraAnchor");
        cameraObj.transform.position = main_Player.transform.position; /* 카메라 초기 위치 */







        //// 조이스틱 호출
        //_mngJoystick = GameObject.Find("imgJoystickBg").GetComponent<managerJoystick>();
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {

        //controllerSet.SetActive(true); //콘트롤러 정의



        // 카메라 따라다니게 하는 거
        if (main_Player != null && cameraObj != null)
        {
            //Debug.Log(rb_Player.transform.position);
            cameraObj.transform.position = main_Player.transform.position;
        }
        // 카메라 따라다니게 하는 거 여기까지

    }





    // Move Function For Move Player

    public void MovePlayer(float forward, float right)
    {

        if (pView.isMine)
        {

            // forward parameter mean virtical axis from keyboard , right parameter mean horizontal axis from keyboard
            // Get Vertical axis and horizontal axis and multiple to camera vertical axis and horizontal axis

            Vector3 translation;

            translation = forward * cameraObj.transform.forward;
            translation += right * cameraObj.transform.right;
            translation.y = 0;

            //Debug.Log(translation.magnitude);

            sg_Anim.SetBool("isRun", false);



            // check if vertical and horizontal pressed
            if (translation.magnitude > 0.1f)
            {

                // set velocity to equal to translation
                velocity = translation;

                moveSpeed = 3.5f;


                //애니메이션 정의
                sg_Anim.SetBool("isRun", false);

                sg_Anim.SetBool("isWalking", true);
                //애니메이션 여기까지

                if (translation.magnitude > 0.95f)
                {

                    sg_Anim.SetBool("isRun", true);

                    moveSpeed = 6f;
                }

            }
            else
            {
                // set velocity to zero
                velocity = Vector3.zero;


                //애니메이션 정의
                sg_Anim.SetBool("isRun", false);

                sg_Anim.SetBool("isWalking", false);
                //애니메이션 여기까지
            }

            //Debug.Log(translation.magnitude);



            // Move Player By Rigidbody Velocity
            rb_Player.velocity = new Vector3(velocity.normalized.x * moveSpeed, rb_Player.velocity.y, velocity.normalized.z * moveSpeed);

            // Rotate Player
            if (velocity.magnitude > 0.2f)
            {
                transform.rotation = Quaternion.Lerp(playerMesh.transform.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * rotationSpeed);
            }

            // Move Animation
            //sg_Anim.SetFloat("isWalking", velocity.magnitude * walkAnimationSpeed);



        }

    }


    //Start Jump Function

    public void Jump()
    {

        if (pView.isMine)
        {

            // jump by rigidbody addforce
            //rb_Player.AddForce(Vector3.up * jumpForce);

            rb_Player.AddForce(new Vector3 (0,jumpForce,0));

            // Play Animation
            sg_Anim.SetTrigger("isJump");

        }

    }

    //End Jump Funcion

    // Start Attack Function

    /* 일단 히든
    public void Attack()
    {

        if (pView.isMine)
        {

            float randomNumber = Random.Range(0, 15);

            if (randomNumber >= 0 && randomNumber <= 5)
            {

                an_Player.SetTrigger("Attack1");

            }
            else if (randomNumber > 5 && randomNumber <= 10)
            {

                an_Player.SetTrigger("Attack2");

            }
            else if (randomNumber > 10 && randomNumber <= 15)
            {

                an_Player.SetTrigger("Attack3");

            }

        }


    }
    히든 여기까지 */



    // 여기부터 코드 추가

    public void AttackDown()
    {
        if (pView.isMine)
        {
            //sg_Anim.SetBool("isDance", true);
            sg_Anim.SetTrigger("isDance");

        }
    }

    //public void AttackUp()
    //{
    //    if (pView.isMine)
    //    {
    //        sg_Anim.SetTrigger("isDance");
    //    }
    //}




    //public void JumpDown()
    //{


    //    if (pView.isMine)
    //    {

    //        sg_Anim.SetTrigger("isJump");

    //        rb_Player.AddForce(Vector3.up * jumpForce);
    //    }
    //}

    //public void JumpUp()
    //{
    //    if (pView.isMine)
    //    {
    //        //rb_Player.AddForce(Vector3.up * jumpForce);
    //        //sg_Anim.SetBool("isJump");
    //    }
    //}


    // 추가코드 여기까지









    // Start Run Function

    public void RunDown()
    {

        if (pView.isMine)
        {
            moveSpeed = runSpeed;
            walkAnimationSpeed = runAnimationSpeed;
        }

    }

    public void RunUp()
    {

        if (pView.isMine)
        {
            moveSpeed = currentMoveSpeed;
            walkAnimationSpeed = currentWalkAnimationSpeed;
        }

    }

    // End Run Function












    /*
    // Start Death Function

    public void Death()
    {

        if (pView.isMine)
        {
            an_Player.SetTrigger("Death");
        }

    }

    // End Death Function

    */

}