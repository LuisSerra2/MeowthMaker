using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI[] highScoreTexts;

    public Vector3 InPosition;
    public Vector3 OutPosition;
    public float animationDuration = 0.5f;

    [Header("MainMenu")]
    public RectTransform ThemeMenu;


    [Header("EndMenu")]

    public RectTransform EndMenu;
    public Button ReturnToMainMenu;
    public Button Retry;

    public TextMeshProUGUI highScoreLabel;



    private Score score;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Retry.onClick.AddListener(() => RetryButton());
        ReturnToMainMenu.onClick.AddListener(() => ReturnToMainMenuButton());

        foreach (Transform item in EndMenu.GetChild(1).GetComponentInChildren<Transform>())
        {
            item.localScale = Vector3.zero;
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            score.AddScore(10);
            bool isNewHighScore = score.CheckAndUpdateHighScore();
            UpdateHighScoreUI(isNewHighScore);
            AnimateIn(EndMenu, hasContentAnimation: true);
        }
    }

    public void UpdateHighScoreUI(bool isNewHighScore = false)
    {
        score = Player.Instance.score;

        highScoreLabel.text = isNewHighScore ? "New Highscore!" : "Highscore";

        foreach (TextMeshProUGUI text in highScoreTexts)
        {
            text.text = score.HighScore.ToString();
        }
    }

    private void RetryButton()
    {
        string sceneName = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene(sceneName);
    }

    private void ReturnToMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void AnimateIn(RectTransform uiElement, bool hasContentAnimation)
    {
        if (hasContentAnimation)
        {
            uiElement.DOAnchorPos(InPosition, animationDuration).OnComplete(() => StartCoroutine(ContentAnimPopUp()));
        } else
        {
            uiElement.DOAnchorPos(InPosition, animationDuration);
        }
    }

    public void AnimateOut(RectTransform uiElement)
    {
        uiElement.DOAnchorPos(OutPosition, animationDuration);
    }

    IEnumerator ContentAnimPopUp()
    {
        foreach (Transform item in EndMenu.GetChild(1).GetComponentInChildren<Transform>())
        {
            item.DOScale(Vector3.one, 0.5f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
