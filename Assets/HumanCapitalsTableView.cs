using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanCapitalsTableView : View
{
    public Image Panel;

    public Text Name;
    public Text Capitals;

    public Text Age;

    public Text Rank;

    GameEntity entity;

    public void SetEntity(GameEntity e)
    {
        entity = e;

        Render();
    }

    void Render()
    {
        Name.text = HumanUtils.GetFullName(entity);
        Capitals.text = Format.Money(InvestmentUtils.GetInvestorCapitalCost(GameContext, entity));
        Rank.text = transform.GetSiblingIndex().ToString();
        Age.text = Random.Range(35, 80).ToString();

        if (entity == Me)
            Panel.color = Visuals.Color(VisualConstants.COLOR_COMPANY_WHERE_I_AM_CEO);

        GetComponent<LinkToHuman>().SetHumanId(entity.human.Id);
    }
}
