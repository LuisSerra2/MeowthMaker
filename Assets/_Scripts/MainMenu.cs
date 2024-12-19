using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public RectTransform mainMenu;
    public RectTransform theme;
    public RectTransform options;

    public Button StartButton;
    public Button ThemeButton;
    public Button OptionsButton;
    public Button ExitButton;

    public Button ExitThemeButton;
    public Button ExitOptionsButton;

    public Vector3 InPosition;
    public Vector3 OutPosition;
    public float animationDuration = 0.5f;

    private void Awake()
    {
        foreach (Transform item in mainMenu.GetChild(0).GetComponentInChildren<Transform>())
        {
            item.localScale = Vector3.zero;
        }
        foreach (Transform item in theme.GetChild(2).GetComponentInChildren<Transform>())
        {
            item.localScale = Vector3.zero;
        }
        foreach (Transform item in options.GetChild(2).GetComponentInChildren<Transform>())
        {
            item.localScale = Vector3.zero;
        }
    }

    private void Start()
    {
        StartCoroutine(ContentAnimPopUp(mainMenu.GetChild(0)));

        StartButton.onClick.AddListener(() => StartB());
        ThemeButton.onClick.AddListener(() => Theme());
        OptionsButton.onClick.AddListener(() => Options());
        ExitButton.onClick.AddListener(() => Exit());
        ExitThemeButton.onClick.AddListener(() => ExitTheme());
        ExitOptionsButton.onClick.AddListener(() => ExitOptions());
    }

    private void StartB()
    {
        SceneManager.LoadScene(1);
    }
    private void Theme()
    {
        AnimateOut(mainMenu, () =>
        {
            foreach (Transform item in mainMenu.GetChild(0).GetComponentInChildren<Transform>())
            {
                item.localScale = Vector3.zero;
            }
            AnimateIn(theme, theme.GetChild(2), true);
        });
    }
    private void Options()
    {
        AnimateOut(mainMenu, () =>
        {
            foreach (Transform item in mainMenu.GetChild(0).GetComponentInChildren<Transform>())
            {
                item.localScale = Vector3.zero;
            }
            AnimateIn(options, options.GetChild(2), true);
        });
    }
    private void Exit()
    {
        Application.Quit();
    }


    private void ExitTheme()
    {
        AnimateOut(theme, () =>
        {
            foreach (Transform item in theme.GetChild(2).GetComponentInChildren<Transform>())
            {
                item.localScale = Vector3.zero;
            }
            AnimateIn(mainMenu, mainMenu.GetChild(0), true);
        });
    }
    private void ExitOptions()
    {
        AnimateOut(options, () =>
        {
            foreach (Transform item in options.GetChild(2).GetComponentInChildren<Transform>())
            {
                item.localScale = Vector3.zero;
            }
            AnimateIn(mainMenu, mainMenu.GetChild(0), true);
        });
    }

    public void AnimateIn(RectTransform uiElement, Transform menu, bool hasContentAnimation)
    {
        if (hasContentAnimation)
        {
            uiElement.DOAnchorPos(InPosition, animationDuration).OnComplete(() => StartCoroutine(ContentAnimPopUp(menu)));
        } else
        {
            uiElement.DOAnchorPos(InPosition, animationDuration);
        }
    }

    public void AnimateOut(RectTransform uiElement, Action action)
    {
        uiElement.DOAnchorPos(OutPosition, animationDuration).OnComplete(()=> action?.Invoke());
    }

    IEnumerator ContentAnimPopUp(Transform menu)
    {
        foreach (Transform item in menu.GetComponentInChildren<Transform>())
        {
            item.DOScale(Vector3.one, 0.5f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
