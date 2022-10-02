using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    [SerializeField] Transform bulletGfx;
    [SerializeField] Transform shadowGfx;
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
        col.enabled = false;
        rb.velocity = Vector3.zero;
        StartCoroutine(_DestroyBullet());
    }

    const float destroyDuration = .4f;
    IEnumerator _DestroyBullet()
    {
        bulletGfx.DOScale(Vector3.zero, destroyDuration);
        shadowGfx.DOScale(Vector3.zero, destroyDuration);
        yield return new WaitForSeconds(destroyDuration);
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 moveDirection)
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    public void EnableCollider()
    {
        col.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {

        }
    }
}
