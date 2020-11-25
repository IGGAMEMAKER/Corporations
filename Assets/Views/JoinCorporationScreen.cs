using Assets.Core;
using UnityEngine.UI;

public class JoinCorporationScreen : View
{
    public Text Title;
    public Text ProposalStatus;

    public Text OurValuation;
    public Text TargetValuation;

    public Toggle KeepAsIndependent;

    public Text Progress;

    public Text OfferNote;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!HasCompany)
            return;

        var name = SelectedCompany.company.Name;
        Title.text = $"Integrate \"{name}\" to our corporation";

        bool willAcceptOffer = false; // Companies.IsCompanyWillAcceptCorporationOffer(Q, SelectedCompany, MyCompany.shareholder.Id);

        var progress = Companies.GetCorporationOfferProgress(Q, SelectedCompany, MyCompany.shareholder.Id);

        Progress.text = Visuals.Colorize(progress + "%", willAcceptOffer);


        // TODO DIVIDE BY ZERO
        var ourCost = Economy.CostOf(MyCompany, Q);
        if (ourCost == 0) ourCost = 1;
        var targetCost = Economy.CostOf(SelectedCompany, Q);

        var sizeComparison = targetCost * 100 / ourCost;

        var futureShares = targetCost * 100 / (targetCost + ourCost);

        OurValuation.text = Format.Money(ourCost);
        TargetValuation.text = $"{Format.Money(targetCost)} ({sizeComparison}% compared to us)";

        bool willStayIndependent = KeepAsIndependent.isOn;

        var isTargetTooBig = targetCost * 100 > ourCost * 15;
        var tooSmallToAcquire = isTargetTooBig ? Visuals.Negative("This company costs more than 15% of our corporation, so they won't join us.") : "";

        OfferNote.text = $"{tooSmallToAcquire}\n\nCompany {name} will be <b>Fully</b> integrated to our company.\n\n" +
            $"Their shareholders will own {futureShares} % of our corporation";
        //if (willStayIndependent)
        //{
        //}
        //else
        //{
        //    OfferNote.text = $"Company {name} will be <b>Partially</b> integrated to our company." +
        //        $"\n\nThey will be able to leave whenever they want." +
        //        $"\nTheir shareholders won't receive our shares." +
        //        $"\n\nOur company will get +1 Brand power for all products in Communications industry." +
        //        $"\n\n{name} will get ";
        //}
    }
}
