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
        long offer = Balance.SALARIES_CEO;

        Offer.text = $"${Format.Minify(offer)} per month";
    }

    void RenderProposalStatus()
    {
        var text = Visuals.Neutral("Is waiting for your response");

        var role = Humans.GetRole(SelectedHuman);

        var hasSameRoleWorkers = SelectedCompany.team.Managers.ContainsValue(role);
        if (hasSameRoleWorkers)
        {
            var workerIndex = SelectedCompany.team.Managers.Values.ToList().FindIndex(k => k == role);
            var humanId = SelectedCompany.team.Managers.Keys.ToList().Find(k => k == workerIndex);

            var level = Humans.GetRating(Q, humanId);
            text = Visuals.Negative(
                $"You already have the {Humans.GetFormattedRole(role)} ({level}LVL) in company {SelectedCompany.company.Name}" +
                $", who will be fired if we continue"
                );
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
