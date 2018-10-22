using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvertRenderer : ListRenderer {
    public override int itemsPerLine
    {
        get { return 5; }
        set { }
    }

    public override void UpdateObject<Advert>(GameObject gameObject, Advert advert, int index)
    {
        Debug.Log("UpdateObject Advert");

        GameObject image = gameObject.transform.GetChild(0).gameObject;
        GameObject text = gameObject.transform.GetChild(1).gameObject;
        text.GetComponent<Text>().text = "Instantiated text";

        GameObject button = gameObject.transform.GetChild(2).gameObject;
        Button b = button.GetComponent<Button>();

        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary["advert"] = advert;
        dictionary["id"] = index;

        b.onClick.AddListener(delegate { BaseController.SendCommand(Commands.AD_CAMPAIGN_START, dictionary); });
    }
}
