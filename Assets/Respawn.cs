using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Respawn : MonoBehaviour
{
    public Transform ship;

    public Vector3 respawnPoint;
    public Quaternion respawnQuaternion;

    public float RespawnBackInSeconds = 3;
    public float RespawnTime = 3;
    public IEnumerator UpdateRespawn()
    {
        for (; ; )
        {
            if (Physics.CheckSphere(respawnPoint, 15))
            {
                respawnPoint = ship.position;
                respawnQuaternion = ship.rotation;
                transform.position = respawnPoint;
                yield return new WaitForSeconds(RespawnBackInSeconds);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator Respawning(UnityAction callback)
    {
        yield return new WaitForSeconds(RespawnTime);
        callback();
    }
}
