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
        playerMat.SetFloat("_HitEffectBlend", 0f);
        RestoreHearts();
    }

    public void Hit()
    {
        Health--;

        if(health <= 0)
        {
            Debug.Log("You died");
        }
        else
        {
            playerMat.DOFloat(1f, "_HitEffectBlend", .1f)
                .OnComplete(() => playerMat.DOFloat(0f, "_HitEffectBlend", .1f));

            Camera.main.DOShakePosition(.1f, .2f);
        }
    }

    public void RestoreHearts()
    {
        Health = maxHealth;
    }
}
