using Assets.Utils;
using UnityEngine.UI;
using UnityEngine;

public class JobOfferScreen : View
{
    public Text Offer;
    public Text WorkerName;

    public Text ProposalStatus;

    public HireWorker HireWorker;

    // tweak salary buttons

    void RenderOffer()
    {
        long offer = Constants.SALARIES_CEO;

        Offer.text = $"${ValueFormatter.Shorten(offer)} per month";
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

    void SetButtons()
    {
        HireWorker.gameObject.SetActive(true);
    }

    void Render()
    {
        RenderOffer();
        RenderProposalStatus();

        SetButtons();

        WorkerName.text = $"Hire {SelectedHuman.human.Name}, {HumanUtils.GetFormattedRole(SelectedHuman.worker.WorkerRole)} ({HumanUtils.GetOverallRating(SelectedHuman, GameContext)}LVL)";
    }
}
