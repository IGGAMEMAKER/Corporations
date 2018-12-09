using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureView : MonoBehaviour {
    public Text Name;
    public Text Relevancy;
    public Hint RelevancyHint;
    public ProgressBar ProgressBar;

    public Button Button;
    public Text ActionText;

    string GetRelevancyHintText(Feature feature)
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

    void SetActionText(string text)
    {
        ActionText.text = text;
    }

    void RenderButtons(bool isWorkInProgress, Feature feature, int featureId, int projectId)
    {
        Button.gameObject.SetActive(!isWorkInProgress);

        Button.interactable = true;

        Button.onClick.RemoveAllListeners();

        SetActionText("");

        if (feature.NeedsExploration)
        {
            if (feature.IsInnovative)
                Button.gameObject.SetActive(false);
            else
            {
                SetActionText("Explore feature");
                Button.onClick.AddListener(delegate { BaseController.ExploreFeature(featureId, projectId); });
            }
        }
        else
        {
            if (feature.NeedsToUpgrade)
                SetActionText("Upgrade feature");
            else if (feature.CanMakeBreakthrough)
                SetActionText("Make breakthrough");
            else
                Button.interactable = false;

            Button.onClick.AddListener(delegate { BaseController.UpgradeFeature(featureId, projectId); });
        }
    }

    public void Render(Feature feature, int index, Dictionary<string, object> parameters)
    {
        bool isWorkInProgress = false;
        int featureId = index;
        int projectId = 0;

        Name.text = feature.name;

        Relevancy.text = feature.GetLiteralRelevancy();

        //RelevancyHint.SetHintObject(GetRelevancyHintText(feature));

        // if we are already upgrading or exploring feature - show ProgressBar
        ProgressBar.gameObject.SetActive(isWorkInProgress);

        RenderButtons(isWorkInProgress, feature, featureId, projectId);
    }
}
