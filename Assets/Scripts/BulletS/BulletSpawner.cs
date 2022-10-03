using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Material bulletMat;
    [SerializeField] GameObject shootVFX;

    [SerializeField] float flashStart = .2f;
    float flashEnd = .2f;

    void Start()
    {
        shootVFX.transform.localScale = Vector3.zero;
        StartCoroutine(_SpawnBullets());
        shootVFX.GetComponentInChildren<SpriteRenderer>().material = bulletMat;
    }

    IEnumerator _SpawnBullets()
    {
        shootVFX.transform.localScale = Vector3.zero;
        shootVFX.transform.DOScale(Vector3.one * 3f, flashStart);
        yield return new WaitForSeconds(flashStart);

        if (!GameManager.Instance.IsGameOver)
        {
            Vector2 dir = CharacterController.Instance.transform.position - transform.position;
            dir.Normalize();
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.SetDirection(dir);

            shootVFX.transform.DOScale(Vector3.zero, flashEnd);
            yield return new WaitForSeconds(flashEnd);
        }

        Destroy(gameObject);
    }
}
