using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
public class Staship : MonoBehaviour
{

    
    [Header("Настройки")]
    [SerializeField]
    bool isInvertedUpDown;
    [SerializeField]
    float normalSpeed;
  
    [SerializeField]
    float verticalRotationSpeed;
    [SerializeField]
    float shipRotationSpeed;
    [SerializeField]
    float wingRotationSpeed;
    [SerializeField]
    float flexRotationAngle;
    [SerializeField]
    [Range(0,100)]
    float RotationLerpSpeed;

    [Header("Внешние контроллеры")]
    public Vector3 headController;
    public Vector3 rightController, leftController;
    public bool IsRightTriggerDown, IsLeftTriggerDown;
    public Quaternion headQuaternion;
    public float pulse;

    public static int AsteroidsDetroyed;
    [Header("Остальное")]
    public Text DistanceText;
    public Text AsteroidsDetroyedText;
    public Text PulseText;
    public Respawn Respawn;
    public FakeController FakeController;
    public Wing RightWing, LeftWing;
    public GameObject Trigger;


    float speed;
    [HideInInspector]
    public bool isMoving;
    Rigidbody rb;
    float rightPercent, leftPercent;
    Quaternion targetQuaternion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pulse = 120;

        MoveToRespawn();
        StartCoroutine(Respawn.UpdateRespawn());
        StartCoroutine(TriggerEnlarge());
    }

    IEnumerator TriggerEnlarge()
    {
        float max = Trigger.transform.localScale.x;
        float cur = 1;
        Trigger.transform.localScale = Vector3.one;
        while (Trigger.transform.localScale.x < max)
        {
            cur += Time.fixedDeltaTime * 10;
            Trigger.transform.localScale = Vector3.one * cur;
            yield return new WaitForFixedUpdate();
        }

    }

    // Update is called once per frame
    void Update()
    {
        speed = (pulse / 100) * normalSpeed;

        PulseText.text = "Pulse: " + pulse.ToString();
        LeftWing.rotationSpeed = wingRotationSpeed;
        RightWing.rotationSpeed = wingRotationSpeed;

        rightPercent = (rightController.y - FakeController.minHeight) / (FakeController.maxHeight - FakeController.minHeight);
        leftPercent = (leftController.y - FakeController.minHeight) / (FakeController.maxHeight - FakeController.minHeight);

        DistanceText.text = "Distance: "+ Mathf.RoundToInt( transform.position.magnitude);
        AsteroidsDetroyedText.text = "Asteroids Destroyed: " + AsteroidsDetroyed;
       
    }

    private void LateUpdate()
    {
        if (isMoving)
        {
            if (IsRightTriggerDown)
                RightWing.Shoot();
            if (IsLeftTriggerDown)
                LeftWing.Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            RightWing.Set(rightPercent);
            LeftWing.Set(leftPercent);

            rb.velocity = transform.forward * speed * Time.fixedDeltaTime;
            MyRotation();
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        MoveToRespawn();
    }

    void MoveToRespawn()
    {
        isMoving = false;
        Vector3 respawnPoint = Respawn.respawnPoint;
        Quaternion respawnQuaternion = Respawn.respawnQuaternion;
        RightWing.Set(0.5f);
        LeftWing.Set(0.5f);

        rb.isKinematic = true;
        transform.SetPositionAndRotation(respawnPoint, respawnQuaternion);

        StartCoroutine(Respawn.Respawning(StartMoving));
    }

    void StartMoving()
    {
        rb.isKinematic = false;
        isMoving = true;
    }


    void MyRotation()
    {
        float middle = leftPercent - rightPercent;
        targetQuaternion = transform.rotation * Quaternion.Euler(CalculateUpDown() * verticalRotationSpeed, middle * shipRotationSpeed, -1 * middle  * shipRotationSpeed  / 1.5f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, RotationLerpSpeed * Time.fixedDeltaTime);
    }

    float CalculateUpDown()
    {
        float verticalSpeed = 0;
        if (rightPercent >= 0.5f && leftPercent >= 0.5f)
        {
            verticalSpeed = rightPercent;
            if (rightPercent >= leftPercent)
                verticalSpeed = leftPercent;
            verticalSpeed -= 0.5f;
        }
        if (rightPercent < 0.5f && leftPercent < 0.5f)
        {
            verticalSpeed = leftPercent;
            if (rightPercent >= leftPercent)
                verticalSpeed = rightPercent;
            verticalSpeed = 0.5f - verticalSpeed;
            verticalSpeed *= -1;
        }
        if (isInvertedUpDown)
            verticalSpeed *= -1;
        verticalSpeed *= 2;


        return verticalSpeed;
    }


}
