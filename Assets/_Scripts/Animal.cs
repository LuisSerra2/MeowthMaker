using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Tier tier;

    public int score = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Animal animal))
        {
            if (animal.tier == tier)
            {
                AnimalsManager.Instance.Animals.Add(gameObject);
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
            }
        }
    }
}
