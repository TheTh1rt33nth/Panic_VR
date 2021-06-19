using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    [SerializeField]
    bool isLeft;
    [HideInInspector]
    public float rotationSpeed;
    Quaternion targetQuaternion;

    public Transform LeftGun, RightGun;
    public GameObject BulletPrefab;


    private void Start()
    {
        Set(0.5f);
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetQuaternion, 1);// rotationSpeed * Time.deltaTime);
    }

    public void Set(float percent)
    {
        targetQuaternion = Quaternion.Euler(0, (isLeft ? 90 - 90 * percent : 90 * percent), 0);
    }

   /* public void ShootLeft()
    {

    }  */

    public void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, RightGun.position, RightGun.rotation,null);
        StartCoroutine( bullet.GetComponent<Bullet>().ShootMe(RightGun.up, 100, 10));

        bullet = Instantiate(BulletPrefab, LeftGun.position, LeftGun.rotation,null);
        StartCoroutine(bullet.GetComponent<Bullet>().ShootMe(RightGun.up, 100, 10));
    }
}
