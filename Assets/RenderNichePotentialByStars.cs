using Assets.Core;

public class RenderNichePotentialByStars : View
{
    int GetAmountOfStars(long potential)
    {
        var million = 1000000;
        var billion = 1000 * million;

        if (potential < million)
            return 1;

        if (potential < 30 * million)
            return 2;

        if (potential < billion)
            return 3;

        if (potential < 1000 * billion)
            return 4;

        return 5;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var potential = Markets.GetMarketPotential(Q, SelectedNiche);

        var stars = GetAmountOfStars(potential);

        GetComponent<SetAmountOfStars>().SetStars(stars);
    }
}
