using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    int bounces = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounces++;
        //Debug.Log(bounces);

        if (bounces > 3)
            Destroy(gameObject);
    }
}
