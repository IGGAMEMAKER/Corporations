using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niche : MonoBehaviour
{
    RectTransform RectTransform;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform = GetComponent<RectTransform>();

        float size = Random.Range(0.58f, 1.35f);

        RectTransform.localScale = new Vector3(size, size, size);
    }
}
