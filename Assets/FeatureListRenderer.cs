using Assets.Classes;
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

    public override void RenderObject(GameObject gameObject, object item, int index)
    {
        Debug.LogFormat("Render Feature!");
        Feature feature = (Feature)item;

        bool isWorkInProgress = false;

        gameObject.transform.Find("Title").gameObject.GetComponent<Text>().text = feature.name;
        gameObject.transform.Find("RelevancyStatus").gameObject.GetComponent<Text>().text = feature.GetLiteralRelevancy();

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

        string text;
        if (feature.Status == FeatureStatus.NeedsExploration)
        {
            text = "Explore feature";
            button.onClick.AddListener(delegate { BaseController.SendCommand(Commands.FEATURE_EXPLORE, dictionary); });
        }
        else
        {
            if (feature.IsNeedToUpgrade())
            {
                text = "Upgrade feature";
                button.onClick.AddListener(delegate { BaseController.SendCommand(Commands.FEATURE_UPGRADE, dictionary); });
            } else if (feature.IsCanMakeBreakthrough())
            {
                text = "Make breakthrough";
            } else
            {
                text = "";
                button.interactable = false;
            }
        }

        actionText.text = text;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
