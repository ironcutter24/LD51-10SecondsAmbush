using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepFaceDirection : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] GameObject containerGfx;

    void FixedUpdate()
    {
        float angle = Vector2.SignedAngle(Vector2.right, bullet.Velocity);
        containerGfx.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
