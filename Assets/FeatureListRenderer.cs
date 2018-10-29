using Assets.Classes;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureListRenderer : ListRenderer
{
    public override int itemsPerLine
    {
        get { return 2; }
        set { }
    }

    public override Vector2 spacing
    {
        get { return new Vector2(300f, 175f); }
        set { }
    }

    string GetRelevancyHintText (Feature feature)
    {
        string relevancy = feature.GetLiteralRelevancy();
        switch (feature.Relevancy)
        {
            case RelevancyStatus.Dinosaur:
                return String.Format("Feature is <b>TERRIBLE</b>\n\nWe lose 5 <b>loyalty</b> each month\nUpgrade it as soon as possible!");
            case RelevancyStatus.Outdated:
                return String.Format("Feature is <b>{0}</b>\n\nWe lose 2 <b>loyalty</b> each month", relevancy);
            case RelevancyStatus.Relevant:
                return String.Format("Feature is <b>{0}</b>\n\nWe get 3 <b>loyalty</b> each month\n\nWe can make <b>technological breakthrough</b>", relevancy);
            case RelevancyStatus.Innovative:
                return String.Format("Feature is <b>{0}</b>\n\nWe get 6 <b>loyalty</b> each month\n\nOur competitors are behind us, but this will not last long", relevancy);
            default:
                return String.Format("Feature is <b>{0}</b>\n\nSome error occured! Take a screenshot and send it to gamedeveloper", relevancy);
        }
    }

    public override void RenderObject(GameObject gameObject, object item, int index, Dictionary<string, object> parameters)
    {
        Feature feature = (Feature)item;

        bool isWorkInProgress = false;

        gameObject.transform.Find("Title").gameObject.GetComponent<Text>().text = feature.name;
        GameObject RelevancyStatusObject = gameObject.transform.Find("RelevancyStatus").gameObject;
        RelevancyStatusObject.GetComponent<Text>().text = feature.GetLiteralRelevancy();

        GameObject HintObject = RelevancyStatusObject.transform.Find("Hint").gameObject;
        HintObject.GetComponent<UIHint>().SetHintObject(GetRelevancyHintText(feature));

        // if we are already upgrading or exploring feature - show ProgressBar
        gameObject.transform.Find("ProgressBar").gameObject.SetActive(isWorkInProgress);

        GameObject ButtonObject = gameObject.transform.Find("Action").gameObject;
        Text actionText = ButtonObject.transform.GetChild(0).GetComponent<Text>();
        Button button = ButtonObject.GetComponent<Button>();

        ButtonObject.SetActive(!isWorkInProgress);

        button.interactable = true;

        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary["featureId"] = index;
        dictionary["projectId"] = 0;

        button.onClick.RemoveAllListeners();

        string text = "";
        if (feature.NeedsExploration)
        {

            if (feature.IsInnovative)
                ButtonObject.SetActive(false);
            else
            {
                text = "Explore feature";
                button.onClick.AddListener(delegate { BaseController.SendCommand(Commands.FEATURE_EXPLORE, dictionary); });
            }
        }
        else
        {
            if (feature.NeedsToUpgrade)
                text = "Upgrade feature";
            else if (feature.CanMakeBreakthrough)
                text = "Make breakthrough";
            else
                button.interactable = false;

            button.onClick.AddListener(delegate { BaseController.SendCommand(Commands.FEATURE_UPGRADE, dictionary); });
        }

        actionText.text = text;
    }
}
