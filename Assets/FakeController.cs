using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FakeController : MonoBehaviour
{ 



    [Header("Управление")]
    public bool IsRotateCamera;
    public float mouseSensivity;
    public float wingsSensivity = 0.1f;
    public float minHeight, maxHeight;
    public bool IsRightTriggerDown, IsLeftTriggerDown;



    [Header("Остальное")]

    public Staship Staship;
    public Transform CameraTransform;
    float right, left;


    public SteamVR_Action_Boolean shootActionLeft = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ShootLeftWing");
    public SteamVR_Action_Boolean shootActionRight = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ShootRightWing");
    public SteamVR_Action_Pose leftWing = SteamVR_Input.GetAction<SteamVR_Action_Pose>("LeftWing");
    public SteamVR_Action_Pose rightWing = SteamVR_Input.GetAction<SteamVR_Action_Pose>("RightWing");

    private bool IsLeftShootButtonDown(Hand hand)
    {
        
        return shootActionLeft.GetStateDown(hand.handType);

    }
    private bool IsRightShootButtonDown(Hand hand)
    {
        return shootActionRight.GetStateDown(hand.handType);
    }
    private Player player = null;



    private void Start()
    {
        player = Valve.VR.InteractionSystem.Player.instance;
        right = Staship.rightController.y;
        left = Staship.leftController.y;
    }


    private void Update()
    {
        Hand[] hands = player.hands;
        IsLeftTriggerDown = IsLeftShootButtonDown(hands[0]);
        IsRightTriggerDown = IsRightShootButtonDown(hands[1]);
       

        right = rightWing.localPosition.y;
        left = leftWing.localPosition.y;



        Staship.rightController = rightWing.localPosition;
        Staship.leftController = leftWing.localPosition;

        Staship.IsRightTriggerDown = IsRightTriggerDown;
        Staship.IsLeftTriggerDown = IsLeftTriggerDown;


        /*
        Debug.Log("Right: " + Staship.rightController.y);
        Debug.Log("Left: " + Staship.leftController.y);
        */

        right = Mathf.Clamp(right, minHeight, maxHeight);
        left = Mathf.Clamp(left, minHeight, maxHeight);


        if (Input.GetKeyDown(KeyCode.Space))
            IsRotateCamera = !IsRotateCamera;

       // CameraRotation();
    }


   /* void CameraRotation()
    {
        if (IsRotateCamera)
        {
            float rotationX = CameraTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensivity;
            float rotationY = CameraTransform.localEulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensivity;
            CameraTransform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
        }

    }*/
}
