using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnCounterExpired = () => { };

    [SerializeField] TMPro.TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        StartCoroutine(_Counter());
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }

    int count;
    IEnumerator _Counter()
    {
        while (gameObject != null)
        {
            count = 10;
            textMeshProUGUI.text = count.ToString();

            while (count > 0)
            {
                yield return new WaitForSeconds(1f);
                count--;
                textMeshProUGUI.text = count.ToString();
            }

            OnCounterExpired();
        }
    }
}
