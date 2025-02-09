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
    public TextMeshProUGUI scoreText;

    public Vector3 InPosition;
    public Vector3 OutPosition;
    public float animationDuration = 0.5f;

    [Header("PauseMenu")]
    public RectTransform pauseMenu;
    public Button pauseMenuButton;
    public Button resume;
    public Button mainMenu;


    [Header("EndMenu")]

    public RectTransform EndMenu;
    public Button ReturnToMainMenu;
    public Button Retry;

    public TextMeshProUGUI highScoreLabel;

    [Header("MiniGame")]
    public GameObject miniGameText;
    bool canShow;

    private Score score;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();
        Retry.onClick.AddListener(RetryButton);
        ReturnToMainMenu.onClick.AddListener(ReturnToMainMenuButton);

        mainMenu.onClick.AddListener(ReturnToMainMenuButton);
        pauseMenuButton.onClick.AddListener(PauseMenu);
        resume.onClick.AddListener(Resume);

        foreach (Transform item in EndMenu.GetChild(1).GetComponentInChildren<Transform>())
        {
            item.localScale = Vector3.zero;
        }
        foreach (Transform item in pauseMenu.GetComponentInChildren<Transform>())
        {
            item.localScale = Vector3.zero;
        }
    }

    private void PauseMenu()
    {
        pauseMenuButton.interactable = false;
        Player.Instance.ChangeState(GameStates.PauseButton);
        AnimateIn(pauseMenu, hasContentAnimation: true, false);
    }
    private void Resume()
    {
        pauseMenuButton.interactable = true;
        Player.Instance.ChangeState(GameStates.Playing);
        AnimateOut(pauseMenu);
    }

    public void EndGameUI()
    {
        bool isNewHighScore = score.CheckAndUpdateHighScore();
        UpdateHighScoreUI(isNewHighScore);
        AnimateIn(EndMenu, hasContentAnimation: true, true);
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
    public void UpdateScoreUI()
    {
        score = Player.Instance.score;

        scoreText.text = "Score: " + score.CurrentScore.ToString();
    }

    private void RetryButton()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    private void ReturnToMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Show_HideMGText()
    {
        canShow = !canShow;
        miniGameText.SetActive(canShow);
    }


    public void AnimateIn(RectTransform uiElement, bool hasContentAnimation, bool isChild1)
    {
        if (hasContentAnimation)
        {
            uiElement.DOAnchorPos(InPosition, animationDuration).OnComplete(() => StartCoroutine(ContentAnimPopUp(isChild1)));
        } else
        {
            uiElement.DOAnchorPos(InPosition, animationDuration);
        }
    }

    public void AnimateOut(RectTransform uiElement)
    {
        uiElement.DOAnchorPos(OutPosition, animationDuration);
    }

    IEnumerator ContentAnimPopUp(bool isChild1)
    {
        if (isChild1)
        {
            foreach (Transform item in EndMenu.GetChild(1).GetComponentInChildren<Transform>())
            {
                item.DOScale(Vector3.one, 0.5f);
                yield return new WaitForSeconds(0.05f);
            }
        } else
        {
            foreach (Transform item in pauseMenu.GetComponentInChildren<Transform>())
            {
                item.DOScale(Vector3.one, 0.5f);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
