using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnCounterExpired = () => { };
    public static event Action<int> OnRoundPassed = (x) => { };

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

    public int CurrentRound { get => round; }

    int count;
    int round = 0;
    IEnumerator _Counter()
    {
        while (gameObject != null)
        {
            round++;
            OnRoundPassed(round);

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
        }
    }
}
