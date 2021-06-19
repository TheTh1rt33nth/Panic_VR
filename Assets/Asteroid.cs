using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public bool IsBig;
    int hp = 3;
    public Renderer Renderer;

    private void OnCollisionEnter(Collision coll)
    {
        if (!IsBig)
            if (coll.transform.tag == "Bullet")
            {
                hp--;
                Destroy(coll.gameObject);
                if (hp <= 0)
                {
                    Destroy(gameObject);
                    Staship.AsteroidsDetroyed++;
                }
                   
            }
    }

}
