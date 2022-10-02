using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Material bulletMat;
    [SerializeField] GameObject shootVFX;

    void Start()
    {
        shootVFX.transform.localScale = Vector3.zero;
        StartCoroutine(_SpawnBullets());
        shootVFX.GetComponentInChildren<SpriteRenderer>().material = bulletMat;
    }

    IEnumerator _SpawnBullets()
    {
        const float flashDuration = .2f;
        shootVFX.transform.localScale = Vector3.zero;
        shootVFX.transform.DOScale(Vector3.one * 3f, flashDuration);
        yield return new WaitForSeconds(flashDuration);

        Vector2 dir = CharacterController.Instance.transform.position - transform.position;
        dir.Normalize();
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.SetDirection(dir);

        shootVFX.transform.DOScale(Vector3.zero, flashDuration);
        yield return new WaitForSeconds(flashDuration);

        Destroy(gameObject);
    }
}
