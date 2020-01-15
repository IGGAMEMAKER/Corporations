using Assets.Core;
using UnityEngine.UI;

public class JobOfferScreen : View
{
    public Text Offer;
    public Text WorkerName;
    public Text RoleName;

    public Text ProposalStatus;

    // tweak salary buttons

    void RenderOffer()
    {
        long offer = Constants.SALARIES_CEO;

        Offer.text = $"${Format.Minify(offer)} per month";
    }

    void RenderProposalStatus()
    {
        var text = Visuals.Neutral("Is waiting for your response");

        var role = Humans.GetRole(SelectedHuman);

        var hasSameRoleWorkers = SelectedCompany.team.Managers.ContainsValue(role);
        if (hasSameRoleWorkers)
        {
            text = Visuals.Negative(
                $"You already have the {Humans.GetFormattedRole(role)} in company {SelectedCompany.company.Name}, while you only need one per company. Are you sure?"
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

        WorkerName.text = $"Hire {Humans.GetFullName(SelectedHuman)}, ({Humans.GetRating(GameContext, SelectedHuman)}LVL)";
        RoleName.text = Humans.GetFormattedRole(role);
    }
}
