using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexityView : MonoBehaviour {
    GameObject ComplexityPrefab;

    void Start()
    {
        ComplexityPrefab = Resources.Load<GameObject>("Assets/Prefabs/ComplexityCost");
    }

    public void Render(int complexity)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        for (var i = 0; i < complexity; i++)
            Instantiate(ComplexityPrefab, transform);
    }
}
