using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEOHintView : View
{
    Hint Hint;

    // Start is called before the first frame update
    void Start()
    {
        Hint = GetComponent<Hint>();
    }

    // Update is called once per frame
    void Update()
    {
        //string hint = String.Format("", myProductEntity.marketing.BrandPower);
        //Hint.SetHint(hint);
    }
}
