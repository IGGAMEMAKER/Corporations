using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EmployeePreview : View
{
    public Text Text;
    public GameObject LoyaltyTab;

    public override void ViewRender()
    {
        base.ViewRender();


    }

    public void SetEntity(int humanId)
    {
        var human = Humans.GetHuman(Q, humanId);

        bool employed = Humans.IsEmployed(human);

        string[] Ambitions = new string[] { "Leader", "Teacher", "Ambitious", "Process-oriented", "Career-oriented", "Team-oriented" };

        if (employed)
        {
            Text.text = Visuals.Colorize("Works in competing company", Colors.COLOR_GOLD);
        }
        else
        {
            Text.text = Visuals.Positive("UNEMPLOYED");
        }

        Text.text += "\n\n";

        Text.text += string.Join("\n", Ambitions.Take(Random.Range(0, Ambitions.Length - 1)));

        Hide(LoyaltyTab);

        ViewRender();
    }
}
