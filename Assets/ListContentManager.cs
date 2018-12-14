using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContentManager : MonoBehaviour {
    public GameObject Content;

    void ClearWorkerContent()
    {
        foreach (Transform child in Content.transform)
            Destroy(child.gameObject);
    }

    public void SetContent(List<GameObject> items)
    {
        ClearWorkerContent();

        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.SetParent(Content.transform);
        }
    }
}
