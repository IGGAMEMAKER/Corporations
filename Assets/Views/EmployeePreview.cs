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

    public void SetEntity(int humanId)
    {
        var human = Humans.GetHuman(Q, humanId);

        bool employed = Humans.IsEmployed(human);

        var rating = Humans.GetRating(human);

        string[] Traits = human.humanSkills.Traits.Select(t => Visuals.Positive(t.ToString())).ToArray(); // new string[] { "Leader", "Teacher", "Ambitious", "Process-oriented", "Career-oriented", "Team-oriented" };

        //if (employed)
        //{
        //    Text.text = Visuals.Colorize("Works in competing company", Colors.COLOR_GOLD);
        //}
        //else
        //{
        //    Text.text = Visuals.Positive("UNEMPLOYED");
        //}

        //Text.text += "\n\n";

        var min = (int)(rating + Companies.GetRandomValueInRange(0, 15, humanId, Flagship.company.Id));
        var max = (int)Mathf.Min(rating + Companies.GetRandomValueInRange(20, 55, humanId, Flagship.company.Id + 1), 100);

        Text.text = $"Potential\n{Visuals.Colorize(min, 30, max)} - {Visuals.Colorize(max, 30, max)}\n\n";

        Text.text += string.Join("\n", Traits.Take(1)); // .Take(Random.Range(0, Traits.Length - 1))

        Hide(LoyaltyTab);

        ViewRender();
    }

    public string ConvertTraitToString(TraitType traitType)
    {
        switch (traitType)
        {
            case TraitType.AverageSpecialist: return "Maker";
            case TraitType.Useful: return "Wants to make useful things";
            case TraitType.NewChallenges: return "Likes new challenges";
            case TraitType.WantsToCreate: return "Wants to create";

            default: return traitType.ToString();
        }
    }
}
