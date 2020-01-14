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
        //bool isFounder = HumanUtils

        var text = Visuals.Neutral("They are waiting for our response");

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

        WorkerName.text = $"Hire {Humans.GetFullName(SelectedHuman)}, ({Humans.GetOverallRating(GameContext, SelectedHuman)}LVL)";
        RoleName.text = Humans.GetFormattedRole(SelectedHuman.worker.WorkerRole);
    }
}
