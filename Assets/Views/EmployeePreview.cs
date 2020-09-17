using Assets.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EmployeePreview : View
{
    public Text Text;
    public GameObject LoyaltyTab;

    public Text WorkerRoleName;

    public void SetEntity(int humanId)
    {
        var human = Humans.GetHuman(Q, humanId);

        var rating = Humans.GetRating(human);
        var role = Humans.GetRole(human);

        string[] Traits = human.humanSkills.Traits.Select(t => Visuals.Positive(ConvertTraitToString(t))).ToArray(); // new string[] { "Leader", "Teacher", "Ambitious", "Process-oriented", "Career-oriented", "Team-oriented" };

        bool employed = Humans.IsEmployed(human);
        //if (employed)
        //{
        //    Text.text = Visuals.Colorize("Works in competing company", Colors.COLOR_GOLD);
        //}
        //else
        //{
        //    Text.text = Visuals.Positive("UNEMPLOYED");
        //}

        //Text.text += "\n\n";

        var team = Flagship.team.Teams[SelectedTeam];
        bool willCompeteWithWorker = team.Managers.Select(h => Humans.GetRole(Humans.GetHuman(Q, h))).Count(h => h == role) > 0;

        if (WorkerRoleName != null)
            WorkerRoleName.color = Visuals.GetColorPositiveOrNegative(!willCompeteWithWorker);

        var min = (int)(rating + Companies.GetRandomValueInRange(0, 15, humanId, Flagship.company.Id));
        var max = (int)Mathf.Min(rating + Companies.GetRandomValueInRange(20, 55, humanId, Flagship.company.Id + 1), 100);

        Text.text = $"Potential\n{Visuals.Colorize(min, 30, max)} - {Visuals.Colorize(max, 30, max)}\n\n";

        Text.text += string.Join("\n", Traits.Take(1)); // .Take(Random.Range(0, Traits.Length - 1))

        Hide(LoyaltyTab);

        ViewRender();
    }

    public string ConvertTraitToString(Trait traitType)
    {
        switch (traitType)
        {
            case Trait.Executor: return "Executor";
            case Trait.Useful: return "Wants to make useful things";
            case Trait.NewChallenges: return "Likes new challenges";
            case Trait.WantsToCreate: return "Wants to create";

            default: return traitType.ToString();
        }
    }
}
