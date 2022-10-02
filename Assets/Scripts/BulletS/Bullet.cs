using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        GameManager.OnCounterExpired += DestroyBullet;
    }

    private void OnDestroy()
    {
        GameManager.OnCounterExpired -= DestroyBullet;
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 moveDirection)
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {

        }
    }
}
