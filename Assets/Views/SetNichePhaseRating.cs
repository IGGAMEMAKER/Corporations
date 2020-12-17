using Assets.Core;

public class SetNichePhaseRating : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var rating = Markets.GetMarketRating(Q, SelectedNiche);
        GetComponent<SetAmountOfStars>().SetStars(rating);
    }
}
