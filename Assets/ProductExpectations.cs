using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductExpectations : View
    , IAnyDateListener
{
    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }

    void Start()
    {
        ListenDateChanges(this);

        Render();
    }

    void Render()
    {
        long balance = MyProductEntity.companyResource.Resources.money;
        long change = CompanyEconomyUtils.GetBalanceChange(GameContext, MyProductEntity.company.Id);

        var Text = GetComponent<Text>();


        if (change >= 0)
        {
            Text.text = VisualFormattingUtils.Positive("We are making money!");
        } else
        {
            Text.text = VisualFormattingUtils.Colorize($"We will be bankrupt in {balance * -1 / change} months!", false);
        }

        Text.text += $" {balance} {change}";
    }
}
