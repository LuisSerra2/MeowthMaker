using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    public static WarningLine Instance;

    private const float defaultTimer = 0;
    private float timer = defaultTimer;

    private bool IsInWarningLine;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Debug.Log((int)timer);
        if (IsInWarningLine)
        {
            timer += Time.deltaTime;

            if (timer >= 3)
            {
                AnimalsManager.Instance.EndGame = true;
                Player.Instance.gameStates = GameStates.EndGame;
            }

        } else
        {
            timer = defaultTimer;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WL wl))
        {
                IsInWarningLine = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WL wl))
        {
            IsInWarningLine = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WL wl))
        {
            IsInWarningLine = false;
        }
    }
}
