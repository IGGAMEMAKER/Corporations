using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketSituationDescriptionView : View
{
    Text Text;

    void Start()
    {
        Text = GetComponent<Text>();
    }

    void Update()
    {
        Render();
    }

    void Render()
    {
        int diff = MarketingUtils.GetMarketDiff(GameContext, MyProductEntity.company.Id);

        //"We are out of trends We follow trends We are leading trends!"

        if (diff == 0)
        {
            Text.text = "We are in trends";
            Text.color = Color.green; // new Color(255, 255, 0);
        } else if (diff == 1)
        {
            Text.text = "We need some improvements";
            Text.color = Color.white;
        } else
        {
            Text.text = $"We are out of market (-{diff}lvl)";
            Text.color = Color.red;
        }
    }
}
