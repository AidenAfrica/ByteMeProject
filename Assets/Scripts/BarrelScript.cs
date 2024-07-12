using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript: MonoBehaviour
{
    public int hitCounter = 0;
    public int hitThreshold = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCounter++;
        }

        if (hitCounter == hitThreshold)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
