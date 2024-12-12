using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

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
    }

    private void Update()
    {
        Debug.Log("Score: " + score.score);
        Debug.Log("HighScore: " + score.highScore);


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

    }

    private void GetRandomAnimal()
    {
        int rndindex = Random.Range(0, animals.Length);

        animalClone = Instantiate(animals[rndindex], transform.position, Quaternion.identity);
        animalClone.transform.SetParent(transform);

        animalClone.GetComponent<Collider2D>().isTrigger = true;
        animalClone.GetComponent<Rigidbody2D>().isKinematic = true;
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

            transform.position = initialPosition;
        }

    }
}
