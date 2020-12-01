using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnlargeOnChange : View
{
    public Text ObservableText;
    public GameObject DisplayObject;

    string previousValue;

    // Start is called before the first frame update
    void Start()
    {
        if (ObservableText == null)
        {
            ObservableText = GetComponent<Text>();
        }

        if (DisplayObject == null)
        {
            DisplayObject = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ObservableText.text != previousValue)
        {
            var e = AddIfAbsent<EnlargeOnDemand>(DisplayObject);

            e.StartAnimation();

            previousValue = ObservableText.text;
        }
    }
}
