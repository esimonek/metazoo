using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 어어어엉 오오오오 여기 조절 잘 하면 멀티터치 해결되겠다~~


public class MyRotationTouchpad : MonoBehaviour
{

    public Transform RotatableH;
    public Transform RotatableV;
    public float RotationSpeed = .1f;
    public bool InvertedV = true;
    public bool ClampedV = true;

    Vector2 currentMousePosition;
    Vector2 mouseDeltaPosition;
    Vector2 lastMousePosition;
	
	public static MyRotationTouchpad mrt;

	[HideInInspector]
	public bool istouchpadactive;


    // 더블터치 테스 추가
    public bool Started = false;
    // 더블터치 테스트 추가 여기까지



    private void Start()
    {
		mrt = this;
        ResetMousePosition();
    }

    public void ResetMousePosition()
    {
        if (Input.touchCount == 1)
        {
            if (!Started)
            {
                currentMousePosition = Input.GetTouch(0).position;
            }
        }
        else if (Input.touchCount == 2)
        {
            if (Started)
            { //원래 1
                currentMousePosition = Input.GetTouch(1).position;
            }
            else
            { //원래 0
                currentMousePosition = Input.GetTouch(0).position;
            }
        }
        else
        {
            currentMousePosition = Input.mousePosition; // 터치아닌 PC마우스일때
        }

        lastMousePosition = currentMousePosition;
        mouseDeltaPosition = currentMousePosition - lastMousePosition;
    }

    void LateUpdate()
    {

        //Debug.Log("Started : " + Started + "  istouchpadactive : " + istouchpadactive);


        if (istouchpadactive)
        {
            if (Input.touchCount == 1)
            {
                if (!Started)
                {
                    currentMousePosition = Input.GetTouch(0).position;
                }
            }
            else if (Input.touchCount == 2)
            {
                if (Started)
                { //원래 1
                    currentMousePosition = Input.GetTouch(1).position;
                }
                else
                { //원래 0
                    currentMousePosition = Input.GetTouch(0).position;
                }
            }
            else
            {
                currentMousePosition = Input.mousePosition; // 터치아닌 PC마우스일때
            }


            mouseDeltaPosition = currentMousePosition - lastMousePosition;

            if (RotatableH != null)
                RotatableH.transform.Rotate(0f, mouseDeltaPosition.x * RotationSpeed, 0f);
            if (RotatableV != null)
            {
                if (InvertedV)
                {
                    RotatableV.transform.Rotate(Mathf.Clamp(mouseDeltaPosition.y * (RotationSpeed * -1), -3, 3), 0f, 0f);
                }

                else
                {
                    RotatableV.transform.Rotate(Mathf.Clamp(mouseDeltaPosition.y * RotationSpeed, -3, 3), 0f, 0f);
                }


                if (ClampedV)
                {
                    float limitedXRot = RotatableV.transform.localEulerAngles.x;
                    if (limitedXRot > 45f && limitedXRot < 340f)
                    {
                        if (limitedXRot < 180f)
                            limitedXRot = 45f;
                        else
                            limitedXRot = 340f;

                    }
                    RotatableV.transform.localEulerAngles = new Vector3(limitedXRot, RotatableV.transform.localEulerAngles.y, /*RotatableV.transform.localEulerAngles.z*/0f);
                }
            }
            lastMousePosition = currentMousePosition;
        }

    }

    public void ActivateTouchpad()
    {
        ResetMousePosition();
        istouchpadactive = true;
    }

    public void DeactivateTouchpad()
    {
        istouchpadactive = false;
    }
}