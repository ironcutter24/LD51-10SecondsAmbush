using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [Space]
    [SerializeField] GameObject warningSign;
    [SerializeField] SpriteRenderer warningGfx;
    [SerializeField] Sprite bulletImage;

    float scaleDuration = .2f;
    float launchWait = .5f;
    float soundFXAnticipation = .3f;

    void Start()
    {
        //warningGfx.sprite = bulletImage;
        StartCoroutine(_SpawnBullets());
    }

    IEnumerator _SpawnBullets()
    {
        warningSign.transform.localScale = Vector3.zero;
        warningSign.transform.DOScale(Vector3.one, scaleDuration);
        yield return new WaitForSeconds(scaleDuration + launchWait - soundFXAnticipation);

        AudioManager.Play(AudioManager.SFX.shootBullet);
        yield return new WaitForSeconds(soundFXAnticipation);

        if (!GameManager.Instance.IsGameOver)
        {
            Vector2 dir = CharacterController.Instance.transform.position - transform.position;
            dir.Normalize();
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.SetDirection(dir);
        }

        warningSign.transform.DOScale(Vector3.zero, .2f);

        Destroy(gameObject);
    }
}
