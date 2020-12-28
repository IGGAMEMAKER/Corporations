using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainGamePrefab : MonoBehaviour
{
    void Start()
    {
        Instantiate(Resources.Load<GameObject>("MainGameScreen"));
        // Instantiate(Resources.Load<GameObject>("Prefabs/MainGameScreen.prefab"));
    }
}
