using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnCounterExpired = () => { };
    public static event Action<int> OnRoundPassed = (x) => { };
    public int CurrentRound { get => round; }

    [SerializeField] TMPro.TextMeshProUGUI countUI;
    [SerializeField] TMPro.TextMeshProUGUI roundUI;
    [Space]
    [SerializeField] GameObject HUDPanelUI;
    [SerializeField] GameObject dialoguePanelUI;
    [SerializeField] TMPro.TextMeshProUGUI dialogue;
    [SerializeField] GameObject pressKeyText;

    void Start()
    {
        StartCoroutine(_IntroScene());
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }

    IEnumerator _IntroScene()
    {
        CharacterController.Instance.SetLockControls(true);
        HUDPanelUI.SetActive(false);
        dialoguePanelUI.SetActive(false);
        pressKeyText.SetActive(false);
        yield return new WaitForSeconds(1f);

        CharacterController.Instance.GoTo(Vector3.zero, SetFlag);
        yield return new WaitUntil(() => GetFlag());

        InvisibleWalls.Instance.SetWalls(true);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(_DisplayText("Damn, the path is blocked by stop signs...<br>This must be an ambush!"));

        CharacterController.Instance.SetLockControls(false);

        HUDPanelUI.SetActive(true);
        StartCoroutine(_Counter());
    }

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

    IEnumerator _DisplayText(string text)
    {
        dialoguePanelUI.SetActive(true);
        dialogue.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '<' && text.Substring(i, 4) == "<br>")
            {
                i += 3;
            }
            dialogue.text = text.Substring(0, i + 1);
            yield return new WaitForSeconds(.05f);
        }

        yield return new WaitForSeconds(.5f);

        pressKeyText.SetActive(true);
        pressKeyText.transform.localScale = Vector3.one;

        Tween myTween = pressKeyText.transform.DOScale(Vector3.one * 1.1f, .8f)
            .SetLoops(-1, LoopType.Yoyo).SetAutoKill(false);

        yield return new WaitUntil(() => Input.anyKeyDown);

        myTween.Kill();
        dialoguePanelUI.SetActive(false);
    }

    #region Flags

    bool eventFlag = false;

    void SetFlag()
    {
        eventFlag = true;
    }

    bool GetFlag()
    {
        if (eventFlag)
        {
            eventFlag = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
}
