using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tier
{
    one,
    two,
    three,
    four,
    five,
}

public class AnimalsManager : MonoBehaviour
{
    public static AnimalsManager Instance;

    private Tier tier;

    public List<GameObject> Animals = new List<GameObject>();
    public List<GameObject> AnimalsAlive = new List<GameObject>();

    public GameObject[] tierOb;

    public bool EndGame = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Animals.RemoveAll(item => item == null);

        if (Animals.Count >= 2)
        {
            if (Animals[0] != null && Animals[1] != null)
            {
                tier = Animals[0].transform.GetComponent<Animal>().tier;

                switch (tier)
                {
                    case Tier.one:
                        InstantiateNextTier(tierOb[1]);
                        break;
                    case Tier.two:
                        InstantiateNextTier(tierOb[2]);
                        break;
                    case Tier.three:
                        InstantiateNextTier(tierOb[3]);
                        break;
                    case Tier.four:
                        InstantiateNextTier(tierOb[4]);
                        break;
                    case Tier.five:
                        DestroyAnimals();
                        break;
                }
            }
        }
    }

    private void InstantiateNextTier(GameObject nextTierPrefab)
    {
        Vector3 midPosition = (Animals[0].transform.position + Animals[1].transform.position) / 2;

        Player.Instance.score.AddScore(Animals[0].GetComponent<Animal>().score);

        DestroyAnimals();

        GameObject animalClone = Instantiate(nextTierPrefab, midPosition, Quaternion.identity);
        animalClone.transform.localScale = Vector3.zero;
        animalClone.GetComponent<ScaleLerping>().enabled = true;

        AnimalsAlive.Add(animalClone);
    }

    private void DestroyAnimals()
    {
        SoundManager.Instance.PopSound();
        if (Animals.Count >= 2)
        {
            AnimalsAlive.Remove(Animals[0]);
            AnimalsAlive.Remove(Animals[1]);

            Destroy(Animals[0]);
            Destroy(Animals[1]);

            Animals.RemoveAt(0);
            Animals.RemoveAt(0);
        }
    }

    public void PopAnimals()
    {
        if (EndGame)
        {
            EndGame = false;

            foreach (GameObject animal in AnimalsAlive)
            {
                if (animal != null)
                {
                    animal.GetComponent<Rigidbody2D>().isKinematic = true;
                }
            }

            StartCoroutine(PopAnimalsAnim());
        }
    }

    private IEnumerator PopAnimalsAnim()
    {
        for (int i = AnimalsAlive.Count - 1; i >= 0; i--)
        {
            if (AnimalsAlive[i] != null)
            {
                AnimalsAlive[i].GetComponent<ScaleLerping>().ScaleZero();
                SoundManager.Instance.PopSound();
                yield return new WaitForSeconds(0.1f);
            }
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = AnimalsAlive.Count - 1; i >= 0; i--)
        {
            if (AnimalsAlive[i] != null)
            {
                Destroy(AnimalsAlive[i]);
            }
        }

        AnimalsAlive.Clear();
    }
}
