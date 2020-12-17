using System.Collections.Generic;
using UnityEngine;

public class GetObjectsWithSpecificLayerID : MonoBehaviour
{
    // Start is called before the first frame update
    void OnGUI()
    {
        if (GUI.Button(new Rect(), "Count Layers"))
        {
            for (var i = 0; i < 10; i++)
            {
                var list = FindGameObjectsWithLayer(i);

                Debug.Log("LayerID =" + list.Length);
            }
        }
    }

    GameObject[] FindGameObjectsWithLayer(int layer) {
        var goArray = FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();

        for (var i = 0; i < goArray.Length; i++) {
            if (goArray[i].layer == layer) {
                goList.Add(goArray[i]);
            }
        }

        if (goList.Count == 0) {
            return null;
        }

        return goList.ToArray();
    }
}
