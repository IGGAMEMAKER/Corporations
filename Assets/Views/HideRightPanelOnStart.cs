using Assets.Core;
using System.Linq;

public class HideRightPanelOnStart : HideOnSomeCondition
{
    public override bool HideIf()
    {
        bool hasReleasedProducts = Companies.GetDaughters(MyCompany, Q)
            .Where(c => c.isRelease)
            .Count() > 0;

        return !hasReleasedProducts;
    }
}
