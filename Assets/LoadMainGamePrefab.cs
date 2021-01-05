using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoadMainGamePrefab : View
{
    void Start()
    {
        OpenUrl("/Holding/Main");
        // Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/MainGameScreen.prefab"));

        // Instantiate(Resources.Load<GameObject>("Prefabs/MainGameScreen"));
        // Instantiate(Resources.Load<GameObject>("Prefabs/MainGameScreen.prefab"));
    }
}
