using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float min, max;

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
        if (!resetScore)
        {
            score = new Score(PlayerPrefs.GetInt("Score", 0));
        } else
        {
            score = new Score(PlayerPrefs.GetInt("Score", 0))
            {
                score = 0
            };
            score.Save("Score");
        }

        Debug.Log(score.score);


        initialPosition = new Vector2(0, transform.position.y);
        GetRandomAnimal();
    }

    private void Update()
    {
        HandleDrop();

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = transform.position;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float animalSize = animalClone.transform.localScale.x;

            pos.x = Mathf.Clamp(mouseWorldPosition.x, min + animalSize / 2, max - animalSize / 2);

            transform.position = pos;
        }

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

        if (Input.GetMouseButtonUp(0))
        {


            animalClone.transform.SetParent(animalsGroup);

            animalClone.GetComponent<Collider2D>().isTrigger = false;
            animalClone.GetComponent<Rigidbody2D>().isKinematic = false;

            transform.position = initialPosition;

            GetRandomAnimal();
        }

    }
}
