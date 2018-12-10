using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridNavigation : MonoBehaviour {
    GridLayoutGroup group;

    void Start()
    {
        group = GetComponent<GridLayoutGroup>();
    }

    public void UpdateLinks()
    {
        Debug.Log("Update Links");
        int columns = group.constraintCount;
        Debug.Log("Columns: " + columns);

        int elements = transform.childCount;
        Debug.Log("Elements: " + elements);

        Selectable FirstChild = transform.GetChild(0).GetComponent<Selectable>();
        Selectable LastChild = transform.GetChild(elements - 1).GetComponent<Selectable>();

        //Transform child in transform
        for (var i = 0; i < elements; i++)
        {
            bool isFirstElement = i == 0;
            bool isLastElement = i == elements - 1;

            bool isFirstInLine = i % columns == 0;
            bool isLastInLine = i % columns == columns - 1;


            Transform child = transform.GetChild(i);
            Selectable selectable = child.GetComponent<Selectable>();
            Navigation navigation = selectable.navigation;

            //if (selectable == LastChild)
            //    navigation.selectOnRight = FirstChild;
            //else
            //    navigation.selectOnRight = transform.GetChild(i + 1).GetComponent<Selectable>();
        }
            
    }
}
