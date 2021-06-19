using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeFakeController : MonoBehaviour
{
    [Header("Управление")]
    public bool IsRotateCamera;
    public float mouseSensivity;
    public float wingsSensivity = 0.1f;
    public float minHeight, maxHeight;
    public bool IsRightTriggerDown, IsLeftTriggerDown;



    [Header("Части VR")]
    public GameObject[] toDisable;
    public GameObject[] toEnable;

    [Header("Остальное")]

    public Staship Staship;
    public Transform CameraTransform;
    float right, left;

    private void Awake()
    {
        for (int i = 0; i < toDisable.Length; i++)
        {
            toDisable[i].SetActive(false);
        }
        for (int i = 0; i < toEnable.Length; i++)
        {
            toEnable[i].SetActive(true);
        }
    }


    private void Start()
    {
        right = Staship.rightController.y;
        left = Staship.leftController.y;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            right -= Time.deltaTime * wingsSensivity;
            if (right < minHeight)
                right = minHeight;
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            right += Time.deltaTime * wingsSensivity;
            if (right > maxHeight)
                right = maxHeight;
        }

        

        if (Input.GetKey(KeyCode.S))
        {
            left -= Time.deltaTime * wingsSensivity;
            if (left < minHeight)
                left = minHeight;
        }

        if (Input.GetKey(KeyCode.W))
        {
            left += Time.deltaTime * wingsSensivity;
            if (left > maxHeight)
                left = maxHeight;
        }

        IsRightTriggerDown = Input.GetKeyDown(KeyCode.RightArrow);
        IsLeftTriggerDown = Input.GetKeyDown(KeyCode.A);

        Staship.IsLeftTriggerDown = IsLeftTriggerDown;
        Staship.IsRightTriggerDown = IsRightTriggerDown;


        Staship.rightController = Vector3.up * right;
        Staship.leftController = Vector3.up * left;

        if (Input.GetKeyDown(KeyCode.Space))
            IsRotateCamera = !IsRotateCamera;

        CameraRotation();
    }


    void CameraRotation()
    {
        if (IsRotateCamera)
        {
            float rotationX = CameraTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensivity;
            float rotationY = CameraTransform.localEulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensivity;
            CameraTransform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
        }

    }
}
