using Assets.Core;

public class ReleaseApp : ButtonController
{
    int id = -1;
    public override void Execute()
    {
        Marketing.ReleaseApp(Q, Flagship);
        //NotificationUtils.AddPopup(Q, new PopupMessageDoYouWantToRelease(Flagship.company.Id));
        //NotificationUtils.AddPopup(Q, new PopupMessageRelease(Flagship.company.Id));

        var gain = Marketing.GetClientFlow(Q, Flagship.product.Niche);

        NotificationUtils.AddSimplePopup(Q, "You've released the product!", "Your flagship product " + Flagship.company.Name + " got " + Visuals.Positive("+" + Format.Minify(gain) + " users")); //  + "\n\nYou can get way more if you change your positioning"
    }

    // not used
    public void SetCompanyId(int companyId)
    {
        id = companyId;
    }
}