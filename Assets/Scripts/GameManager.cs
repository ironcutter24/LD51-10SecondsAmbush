using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utility.Patterns;
using System.Runtime.CompilerServices;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnCounterExpired = () => { };
    public static event Action<int> OnRoundPassed = (x) => { };
    public int CurrentRound { get => round; }

    private bool isGameOver = false;
    public bool IsGameOver { get => isGameOver; }

    Vector3 playerStart = Vector3.zero;

    [Header("HUD")]
    [SerializeField] GameObject HUDPanelUI;
    [SerializeField] TextMeshProUGUI countUI;
    [SerializeField] TextMeshProUGUI roundUI;
    [Space]
    [Header("Game Over")]
    [SerializeField] Image gameOverUI;
    [SerializeField] TextMeshProUGUI resume;
    [SerializeField] GameObject restartText;
    [Space]
    [Header("Dialogue")]
    [SerializeField] GameObject dialoguePanelUI;
    [SerializeField] TextMeshProUGUI dialogue;
    [SerializeField] GameObject pressKeyText;

    Coroutine counterHandle;

    void Start()
    {
        StartCoroutine(_IntroScene());
        //StartCounter();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }

    #region Counter

    void StartCounter()
    {
        if (counterHandle != null)
            StopCoroutine(counterHandle);
        counterHandle = StartCoroutine(_Counter());
    }

    int count;
    int round = 0;
    IEnumerator _Counter()
    {
        CharacterController.Instance.transform.position = playerStart;

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

            if (isGameOver)
                yield break;

            OnCounterExpired();
        }
    }

    #endregion

    #region GameOver

    public void SetGameOver(bool isWinner = false)
    {
        StartCoroutine(_GameOver(isWinner));
    }

    IEnumerator _GameOver(bool isWinner)
    {
        isGameOver = true;
        OnCounterExpired();

        HUDPanelUI.SetActive(false);
        CharacterController.Instance.ShowHUD(false);

        AudioManager.Instance.StopMusic();

        round--;
        resume.text = "You survived<br>" + (round) + " round" + (round == 1 ? "" : "s") + "!";

        if (isWinner)
        {
            yield return StartCoroutine(_EndingScene());

            resume.text = "You survived!";
            gameOverUI.color = new Color(0.2941177f, 0.6392157f, 0.8745099f, .4f);
        }
        else
        {
            gameOverUI.color = new Color(1f, 0f, 0f, .2f);
        }

        restartText.SetActive(false);
        gameOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        restartText.SetActive(true);
        restartText.transform.localScale = Vector3.one;

        Tween myTween = restartText.transform.DOScale(Vector3.one * 1.1f, .8f)
            .SetLoops(-1, LoopType.Yoyo).SetAutoKill(false);

        yield return new WaitUntil(() => Input.anyKeyDown);

        myTween.Kill();

        restartText.SetActive(false);
        gameOverUI.gameObject.SetActive(false);

        StartCoroutine(_ResetGame());
    }

    IEnumerator _ResetGame()
    {
        isGameOver = false;
        HUDPanelUI.SetActive(true);

        AudioManager.Instance.PlayMusic();

        CharacterController.Instance.transform.position = playerStart;
        CharacterController.Instance.SetDeath(false);
        CharacterController.Instance.ShowHUD(true);

        round = 0;
        count = 0;
        StartCounter();

        yield break;
    }

    #endregion

    #region Cutscenes

    IEnumerator _IntroScene()
    {
        CharacterController.Instance.SetLockControls(true);
        CharacterController.Instance.ShowHUD(false);
        HUDPanelUI.SetActive(false);
        dialoguePanelUI.SetActive(false);
        pressKeyText.SetActive(false);
        yield return new WaitForSeconds(1f);

        CharacterController.Instance.GoTo(playerStart, SetFlag);
        yield return new WaitUntil(() => GetFlag());

        InvisibleWalls.Instance.SetWalls(true);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(_DisplayText("Damn, you are trapped by stop signs...<br>This must be an ambush!"));
        yield return StartCoroutine(_DisplayText("I'd like to help, but I really have to...<br>ehm...<br>do laundry..."));
        yield return StartCoroutine(_DisplayText("Good luck!"));

        AudioManager.Instance.PlayMusic();
        CharacterController.Instance.SetLockControls(false);

        StartCoroutine(_ResetGame());
    }

    IEnumerator _EndingScene()
    {
        CharacterController.Instance.SetLockControls(true);
        CharacterController.Instance.ShowHUD(false);
        HUDPanelUI.SetActive(false);
        dialoguePanelUI.SetActive(false);
        pressKeyText.SetActive(false);
        yield return new WaitForSeconds(1f);

        CharacterController.Instance.GoTo(playerStart, SetFlag);
        yield return new WaitUntil(() => GetFlag());

        InvisibleWalls.Instance.SetWalls(false);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(_DisplayText("Hey, I'm done with laundry!"));
        yield return StartCoroutine(_DisplayText("Also I took down the fruit bandits,<br>ninja style!"));
        yield return StartCoroutine(_DisplayText("Let's go buddy!"));

        CharacterController.Instance.GoTo(new Vector3(0f, 20f, 0f), SetFlag);
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
            AudioManager.Play(AudioManager.SFX.textRollout);
            yield return new WaitForSeconds(.05f);
        }

        yield return new WaitForSeconds(.5f);

        pressKeyText.SetActive(true);
        pressKeyText.transform.localScale = Vector3.one;

        Tween myTween = pressKeyText.transform.DOScale(Vector3.one * 1.1f, .8f)
            .SetLoops(-1, LoopType.Yoyo).SetAutoKill(false);

        yield return new WaitUntil(() => Input.anyKeyDown);

        myTween.Kill();
        pressKeyText.SetActive(false);
        dialoguePanelUI.SetActive(false);
    }

    #endregion

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
