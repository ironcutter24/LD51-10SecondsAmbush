using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Material playerMat;
    [SerializeField] List<GameObject> hearts = new List<GameObject>();

    const int maxHealth = 4;
    private int health;
    public int Health
    {
        get => health;
        set
        {
            health = value;

            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].SetActive(i < Health);
            }
        }
    }

    private void Start()
    {
        RestoreMatValues();
        RestoreHearts();
    }

    private void OnDestroy()
    {
        RestoreMatValues();
    }

    public void Hit()
    {
        Health--;

        AudioManager.Play(AudioManager.SFX.playerHit);
        Camera.main.DOShakePosition(.1f, .2f);

        if (health <= 0)
        {
            Debug.Log("You died");
            playerMat.SetColor("_Color", Color.red);
            playerMat.DOFloat(.5f, "_HitEffectBlend", .1f)
                .OnComplete(() => playerMat.DOFloat(0f, "_HitEffectBlend", .1f));

            CharacterController.Instance.SetDeath(true);
            GameManager.Instance.SetGameOver();
        }
        else
        {
            playerMat.DOFloat(1f, "_HitEffectBlend", .1f)
                .OnComplete(() => playerMat.DOFloat(0f, "_HitEffectBlend", .1f));
        }
    }

    public void RestoreMatValues()
    {
        playerMat.SetColor("_Color", Color.white);
        playerMat.SetFloat("_HitEffectBlend", 0f);
    }

    public void RestoreHearts()
    {
        Health = maxHealth;
    }
}
