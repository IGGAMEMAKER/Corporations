using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContentManager : MonoBehaviour {
    public GameObject Content;

    public void SetContent(List<GameObject> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.SetParent(transform);
        }
    }
}
