using Assets.Core;
using UnityEngine.UI;
using System.Linq;

public class JobOfferScreen : View
{
    public Text Offer;
    public Text WorkerName;
    public Text RoleName;

    public Text ProposalStatus;

    // tweak salary buttons

    void RenderOffer()
    {
        long offer = C.SALARIES_DIRECTOR;

        Offer.text = $"${Format.Minify(offer)} per month";
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

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        RenderOffer();
        RenderProposalStatus();

        var role = Humans.GetRole(SelectedHuman);

        WorkerName.text = $"Hire {Humans.GetFullName(SelectedHuman)}, ({Humans.GetRating(Q, SelectedHuman)}LVL)";
        RoleName.text = Humans.GetFormattedRole(role);
    }
}
