using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Tier tier;

    public int score = 10;

    public GameObject target;
    public bool isTargetOn = false;

    public GameObject targetClone;

    public void SpawnTarget()
    {
        if (isTargetOn) return;
        isTargetOn = true;
        targetClone = Instantiate(target, transform);
    }

    public void DestroyTarget()
    {
        Destroy(targetClone);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Animal animal))
        {
            if (animal.tier == tier)
            {
                AnimalsManager.Instance.Animals.Add(gameObject);
                AnimalsManager.Instance.RemoveFromAnimalAlive(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Animal animal))
        {
            if (animal.tier == tier)
            {
                AnimalsManager.Instance.Animals.Add(gameObject);
                AnimalsManager.Instance.RemoveFromAnimalAlive(gameObject);
            }
        }
    }
}
