using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    void Start()
    {
        StartCoroutine(_SpawnBullets());
    }

    IEnumerator _SpawnBullets()
    {
        while (gameObject != null)
        {
            yield return new WaitForSeconds(2f);


            Vector2 dir = CharacterController.Instance.transform.position - transform.position;
            dir.Normalize();

            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.SetDirection(dir);
        }
    }
}
