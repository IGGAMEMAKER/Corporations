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
    public override Vector2 spacing
    {
        get { return new Vector2(150f, 50f); }
        set { }
    }

    public override void RenderObject(GameObject gameObject, object advertObject, int index, Dictionary<string, object> parameters)
    {
        Advert advert = (Advert) advertObject;

        GameObject image = gameObject.transform.GetChild(0).gameObject;
        GameObject text = gameObject.transform.GetChild(1).gameObject;
        GameObject prepareAdButton = gameObject.transform.GetChild(2).gameObject;
        GameObject startAdButton = gameObject.transform.GetChild(3).gameObject;

        Button p = prepareAdButton.GetComponent<Button>();
        Button s = startAdButton.GetComponent<Button>();
        Text TextComponent = text.GetComponent<Text>();

        TextComponent.text = string.Format("Ad for channel {0}", advert.Channel);

        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary["advert"] = advert;

        p.onClick.RemoveAllListeners();
        s.onClick.RemoveAllListeners();
        p.onClick.AddListener(delegate { BaseController.SendCommand(Commands.AD_CAMPAIGN_PREPARE, dictionary); });
        s.onClick.AddListener(delegate { BaseController.SendCommand(Commands.AD_CAMPAIGN_START, dictionary); });

        if (advert.Duration == 0)
        {
            prepareAdButton.SetActive(true);
            startAdButton.SetActive(false);
        }
        else
        {
            prepareAdButton.SetActive(false);
            startAdButton.SetActive(true);
        }
    }
}
