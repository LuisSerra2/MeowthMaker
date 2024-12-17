using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    MainMenu,
    Playing,
    EndGame,
    ChangeTheme,
}

public class Player : MonoBehaviour
{
    public static Player Instance;
    public GameStates gameStates;

    public float min, max;

    public float defaultTimer = 0.2f;
    private float timer;
    private bool canDrop;

    [Space]

    public GameObject[] animals;
    public Transform animalsGroup;

    private GameObject animalClone;
    private Vector2 initialPosition;

    public Score score;
    public bool resetScore = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //gameStates = GameStates.MainMenu;

        canDrop = true;
        timer = defaultTimer;

        int savedScore = PlayerPrefs.GetInt("Score", 0);
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (!resetScore)
        {
            score = new Score(savedScore, savedHighScore);
        } else
        {
            score = new Score(0, 0);
            score.Save("Score", "HighScore");
        }

        initialPosition = new Vector2(0, transform.position.y);
        GetRandomAnimal();
        UIManager.Instance.UpdateHighScoreUI();
    }

    private void Update()
    {
        switch (gameStates)
        {
            case GameStates.MainMenu:



                break;

            case GameStates.Playing:

                if (!canDrop)
                {
                    timer -= Time.deltaTime;

                    if (timer <= 0)
                    {
                        canDrop = true;
                        GetRandomAnimal();
                    }
                }

                if (Input.GetMouseButton(0) && canDrop)
                {
                    Vector3 pos = transform.position;
                    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    float animalSize = animalClone.transform.localScale.x;

                    pos.x = Mathf.Clamp(mouseWorldPosition.x, min + animalSize / 2, max - animalSize / 2);

                    transform.position = pos;
                }

                HandleDrop();

                break;

            case GameStates.EndGame:
                AnimalsManager.Instance.PopAnimals();
                break;

            case GameStates.ChangeTheme:
                break;

        }


    }

    private void GetRandomAnimal()
    {
        int rndindex = Random.Range(0, animals.Length);

        animalClone = Instantiate(animals[rndindex], transform.position, Quaternion.identity);
        animalClone.transform.SetParent(transform);

        animalClone.GetComponent<Collider2D>().isTrigger = true;
        animalClone.GetComponent<Rigidbody2D>().isKinematic = true;
        animalClone.transform.GetChild(1).GetComponent<Collider2D>().gameObject.SetActive(false);
    }

    private void HandleDrop()
    {
        if (animalClone == null) return;

        if (Input.GetMouseButtonUp(0) && canDrop)
        {
            timer = defaultTimer;
            canDrop = false;

            animalClone.transform.SetParent(animalsGroup);

            animalClone.GetComponent<Collider2D>().isTrigger = false;
            animalClone.GetComponent<Rigidbody2D>().isKinematic = false;
            animalClone.transform.GetChild(1).GetComponent<Collider2D>().gameObject.SetActive(true);

            transform.position = initialPosition;

            AnimalsManager.Instance.AnimalsAlive.Add(animalClone);
        }

    }
}
