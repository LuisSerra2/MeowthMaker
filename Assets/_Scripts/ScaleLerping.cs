using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLerping : MonoBehaviour
{
    public float diseredScale = 1;
    public float timer = .1f;
    private void Start()
    {
        Scale();
    }

    private void Scale()
    {
        Vector3 scale = new Vector3(diseredScale, diseredScale, diseredScale);

        transform.DOScale(scale, timer);
    }
}
