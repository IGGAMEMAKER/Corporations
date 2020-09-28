using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMyModalWindow : ButtonController
{
    public string ModalTag;

    public override void Execute()
    {
        var m = FindObjectsOfTypeAll<MyModalWindow>().FirstOrDefault(w => w.ModalTag == ModalTag);

        if (m == null)
        {
            Debug.LogError("Modal " + ModalTag + " not found!");
        }
        else
        {
            Show(m);
        }
    }

    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> results = new List<T>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                var allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++)
                {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }
        return results;
    }
}
