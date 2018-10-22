using Assets.Classes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdvertRenderer : ListRenderer {
    public override int itemsPerLine
    {
        get { return 5; }
        set { }
    }

    public override void UpdateObject<Advert>(GameObject gameObject, Advert ModelName)
    {
        Debug.Log("UpdateObject Advert");

        GameObject image = gameObject.transform.GetChild(0).gameObject;
        GameObject text = gameObject.transform.GetChild(1).gameObject;
        text.GetComponent<Text>().text = "Instantiated text";

        GameObject Button = gameObject.transform.GetChild(2).gameObject;
    }
}
