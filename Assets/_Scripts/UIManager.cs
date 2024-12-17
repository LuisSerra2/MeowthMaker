using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public RectTransform MainMenu;
    public RectTransform EndMenu;

    public TextMeshProUGUI[] highScoreTexts;
    public TextMeshProUGUI highScoreLabel;

    public Vector3 InPosition;
    public Vector3 OutPosition;
    public float animationDuration = 0.5f;

    private Score score;


    private void Awake()
    {
        Instance = this;    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            score.AddScore(10);
            bool isNewHighScore = score.CheckAndUpdateHighScore();
            UpdateHighScoreUI(isNewHighScore);
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

    public void AnimateIn(RectTransform uiElement)
    {
        uiElement.DOAnchorPos(InPosition, animationDuration);
    }

    public void AnimateOut(RectTransform uiElement)
    {
        uiElement.DOAnchorPos(OutPosition, animationDuration);
    }
}
