using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnCounterExpired = () => { };

    [SerializeField] TMPro.TextMeshProUGUI countUI;
    [SerializeField] TMPro.TextMeshProUGUI roundUI;

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
    int round = 1;
    IEnumerator _Counter()
    {
        while (gameObject != null)
        {
            roundUI.text = "Round " + round.ToString();

            count = 10;
            countUI.text = count.ToString();

            while (count > 0)
            {
                yield return new WaitForSeconds(1f);
                count--;
                countUI.text = count.ToString();
            }

            OnCounterExpired();
            round++;
        }
    }
}
