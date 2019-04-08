using Assets.Utils;
using UnityEngine;

public abstract class AbandonCompanyController : ButtonController
{
    public abstract GameEntity GetCompany();

    public override void Execute()
    {
        var c = GetCompany();

        if (c != null)
        {
            CompanyUtils.LeaveCEOChair(GameContext, c.company.Id);

            ReNavigate();
        }
    }
}