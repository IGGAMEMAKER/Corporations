using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoadMainGamePrefab : MonoBehaviour
{
    void Start()
    {
        Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/MainGameScreen.prefab"));
        // Instantiate(Resources.Load<GameObject>("Prefabs/MainGameScreen"));
        // Instantiate(Resources.Load<GameObject>("Prefabs/MainGameScreen.prefab"));
    }
}
