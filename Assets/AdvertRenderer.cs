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

    void RenderButtons(Advert advert, GameObject prepareAdButton, GameObject startAdButton)
    {
        Button p = prepareAdButton.GetComponent<Button>();
        p.onClick.RemoveAllListeners();
        p.onClick.AddListener(delegate { BaseController.PrepareAdCampaign(advert); });

        Button s = startAdButton.GetComponent<Button>();
        s.onClick.RemoveAllListeners();
        s.onClick.AddListener(delegate { BaseController.StartAdCampaign(advert); });

        if (advert.NeedsPreparation)
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

    void RenderAdvertName(Advert advert, GameObject text)
    {
        Text TextComponent = text.GetComponent<Text>();

        TextComponent.text = string.Format("Ad for channel {0}", advert.Channel);
    }

    public override void RenderObject(GameObject gameObject, object advertObject, int index, Dictionary<string, object> parameters)
    {
        Advert advert = (Advert) advertObject;

        GameObject image = gameObject.transform.GetChild(0).gameObject;
        GameObject text = gameObject.transform.GetChild(1).gameObject;
        GameObject prepareAdButton = gameObject.transform.GetChild(2).gameObject;
        GameObject startAdButton = gameObject.transform.GetChild(3).gameObject;

        RenderAdvertName(advert, text);
        RenderButtons(advert, prepareAdButton, startAdButton);
    }
}
