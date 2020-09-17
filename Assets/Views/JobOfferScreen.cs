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
    public GameObject HireManager;

    // tweak salary buttons
    void OnEnable()
    {
        var human = SelectedHuman;

        var role = Humans.GetRole(human);
        var rating = Humans.GetRating(Q, human);

        var baseSalary = Teams.GetSalaryPerRating(rating);

        JobOffer = new JobOffer(baseSalary);

        Render(SelectedHuman);
    }

    void Render(GameEntity human)
    {
        var role = Humans.GetRole(human);
        var rating = Humans.GetRating(Q, human);

        Offer.text = $"${Format.Minify(JobOffer.Salary)} / month";
        WorkerName.text = $"Hire {Humans.GetFullName(human)}, ({rating}LVL)";
        RoleName.text = Humans.GetFormattedRole(role);

        RenderProposalStatus(human, rating);
    }

    void RenderProposalStatus(GameEntity human, int rating)
    {
        var offer = JobOffer.Salary;
        var wantedOffer = Teams.GetSalaryPerRating(human, rating);

        var text = Visuals.Neutral("Is waiting for your response");

        if (offer < wantedOffer)
        {
            text = Visuals.Negative("Wants bigger salary!");
            Hide(HireManager);
        }

        if (offer >= wantedOffer)
        {
            text = Visuals.Positive("Will accept job offer");
            Show(HireManager);
        }

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
        JobOffer.Salary = JobOffer.Salary * 11 / 10;

        Render(SelectedHuman);
    }

    public void DecreaseSalary()
    {
        JobOffer.Salary = JobOffer.Salary * 9 / 10;

        Render(SelectedHuman);
    }
}
