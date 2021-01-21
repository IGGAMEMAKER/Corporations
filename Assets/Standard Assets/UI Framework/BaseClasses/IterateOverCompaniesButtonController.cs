using System;
using System.Linq;

public abstract class IterateOverCompaniesButtonController : ButtonController
{
    public abstract ScreenMode GetScreenMode();
    public abstract GameEntity[] GetEntities();

    public override void Execute()
    {
        var companies = GetEntities();

        var targetMenu = GetScreenMode();


        // copy
        var companyId = SelectedCompany.company.Id;

        if (companies.Length == 0)
            return;

        var firstId = companies.First().company.Id;


        if (CurrentScreen != targetMenu)
            companyId = firstId;
        else
        {
            var ind = Array.FindIndex(companies, m => m.company.Id == companyId);

            if (ind == -1 || ind == companies.Length - 1)
                companyId = firstId;
            else
                companyId = companies[ind + 1].company.Id;
        }

        Navigate(targetMenu, C.MENU_SELECTED_COMPANY, companyId);
    }

}
