using Assets.Core;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class JobOfferScreen : View
{
    public Text Offer;
    public Text WorkerName;
    public Text RoleName;

    public Text ProposalStatus;

    public JobOffer JobOffer;

    // tweak salary buttons
    void OnEnable()
    {
        Render(SelectedHuman);
    }

    void Render(GameEntity human)
    {
        var role = Humans.GetRole(human);
        var rating = Humans.GetRating(Q, human);

        var baseSalary = Teams.GetSalaryPerRating(human, rating);

        JobOffer = new JobOffer(baseSalary / 4);


        Offer.text = $"${Format.Minify(JobOffer.Salary)} / week";
        WorkerName.text = $"Hire {Humans.GetFullName(human)}, ({rating}LVL)";
        RoleName.text = Humans.GetFormattedRole(role);

        RenderProposalStatus();
    }

    void RenderProposalStatus()
    {
        var text = Visuals.Neutral("Is waiting for your response");

        var role = Humans.GetRole(SelectedHuman);

        var company = Flagship;
        var managers = company.team.Managers;

        var hasSameRoleWorkers = managers.ContainsValue(role);
        if (hasSameRoleWorkers)
        {
            var workerIndex = managers.Values.ToList().FindIndex(k => k == role);
            var humanId = managers.Keys.ToList().Find(k => k == workerIndex);

            var level = Humans.GetRating(Q, humanId);
            //text = Visuals.Negative($"You already have the {Humans.GetFormattedRole(role)} ({level}LVL) in company {company.company.Name}.");
        }

        ProposalStatus.text = text;
    }

    public void IncreaseSalary()
    {
        Debug.Log("Increase Salary");
        JobOffer.Salary = JobOffer.Salary * 11 / 10;

        Render(SelectedHuman);
    }

    public void DecreaseSalary()
    {
        Debug.Log("Decrease Salary");
        JobOffer.Salary = JobOffer.Salary * 9 / 10;

        Render(SelectedHuman);
    }
}
