using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public IEnumerator ShootMe(Vector3 direction, float speed, float timeToDie)
    {
        rb.detectCollisions = false;
        rb.AddForce(direction.normalized * speed, ForceMode.VelocityChange);
        Destroy(gameObject, timeToDie);
        yield return new WaitForSeconds(0.1f);
        rb.detectCollisions = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
