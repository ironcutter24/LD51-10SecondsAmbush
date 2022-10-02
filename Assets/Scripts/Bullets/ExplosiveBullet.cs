using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    [SerializeField] GameObject childBulletsPrefab;
    [SerializeField] float spread = 60f;

    int bounces = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounces++;
        //Debug.Log(bounces);

        if (bounces > 0)
        {
            // Spawn more bullets

            var hitNormal = collision.GetContact(0).normal;

            SpawnChildBullet(hitNormal.Rotate(-spread * .5f));
            SpawnChildBullet(hitNormal);
            SpawnChildBullet(hitNormal.Rotate(spread * .5f));

            Destroy(gameObject);
        }
    }

    void SpawnChildBullet(Vector2 direction)
    {
        var bullet = Instantiate(childBulletsPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.SetDirection(direction);
        bullet.EnableCollider();
    }
}

public static class Vector2Extension
{
    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
