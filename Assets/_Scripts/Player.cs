using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates
{
    MainMenu,
    Playing,
    MiniGame,
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


    [Space]

    [Header("Minigame")]

    public Image fillMG;
    public float scoreMG = 0;

    private bool isTargetOn;

    public float MGdefaultTimer = 3f;
    private float MGTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canDrop = true;
        timer = defaultTimer;
        MGTimer = MGdefaultTimer;

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (!resetScore)
        {
            score = new Score(0, savedHighScore);
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

                CanUseMinigame();

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

                    pos.x = Mathf.Clamp(mouseWorldPosition.x, (min + animalSize / 2) + 0.05f, (max - animalSize / 2) - 0.05f);

                    transform.position = pos;
                }

                HandleDrop();

                break;
            case GameStates.MiniGame:
                Minigame();
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
        float randomValue = Random.value;
        int selectedIndex;
        if (randomValue < 0.5f)
        {
            selectedIndex = 0;
        } else if (randomValue < 0.85f)
        {
            selectedIndex = 1;
        } else
        {
            selectedIndex = 2;
        }

        animalClone = Instantiate(animals[selectedIndex], transform.position, Quaternion.identity);
        animalClone.transform.SetParent(transform);

        animalClone.GetComponent<Collider2D>().isTrigger = true;
        animalClone.GetComponent<Rigidbody2D>().isKinematic = true;
        animalClone.transform.GetChild(1).GetComponent<Collider2D>().gameObject.SetActive(false);

        var themeManager = ThemeManager.Instance;
        if (themeManager != null)
        {
            UpdateThemeSprites[] themeComponents = animalClone.GetComponentsInChildren<UpdateThemeSprites>();
            foreach (var component in themeComponents)
            {
                Sprite newSprite = themeManager.GetSpriteForTag(component.spriteTag);
                if (newSprite != null)
                {
                    component.ApplyTheme(newSprite);
                }
            }
        }
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

    private void Minigame()
    {

        if (!isTargetOn)
        {
            isTargetOn = true;
            UIManager.Instance.Show_HideMGText();
            foreach (GameObject target in AnimalsManager.Instance.AnimalsAlive)
            {
                target.GetComponent<Animal>().isTargetOn = false;
                target.GetComponent<Animal>().SpawnTarget();
            }
        }

        MGTimer -= Time.deltaTime;

        if (MGTimer <= 0)
        {
            foreach (GameObject target in AnimalsManager.Instance.AnimalsAlive)
            {
                target.GetComponent<Animal>().DestroyTarget();
            }

            scoreMG = 0;
            MGTimer = MGdefaultTimer;
            gameStates = GameStates.Playing;
            SoundManager.Instance.MusicAmbiente();
            UIManager.Instance.Show_HideMGText();
            isTargetOn = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Animal animal))
                {
                    score.AddScore(animal.score);
                    UIManager.Instance.UpdateScoreUI();
                    AnimalsManager.Instance.RemoveAnimal(animal.gameObject);
                    SoundManager.Instance.PopSound();
                }
            }
        }
    }

    public void MinigameButton()
    {
        gameStates = GameStates.MiniGame;
        SoundManager.Instance.MusicAmbiente();
    }

    private void CanUseMinigame()
    {
        fillMG.fillAmount = Mathf.Lerp(fillMG.fillAmount, scoreMG / 1000f, 0.2f);

        if (fillMG.fillAmount >= 1)
        {
            fillMG.gameObject.GetComponentInChildren<Button>().interactable = true;
            fillMG.GetComponentInParent<Animator>().SetBool("Charge", true);
        } else
        {
            fillMG.gameObject.GetComponentInChildren<Button>().interactable = false;
            fillMG.GetComponentInParent<Animator>().SetBool("Charge", false);
        }
    }
}
