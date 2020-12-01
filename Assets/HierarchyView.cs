using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HierarchyView : View
{
    public MyGroupCompaniesListView MyGroupCompaniesListView;
    public MyProductCompaniesListView MyProductCompaniesListView;
    public SelectedCompanyTeamListView SelectedCompanyTeamListView;
    public SelectedTeamEmployeesListView SelectedTeamEmployeesListView;

    public override void ViewRender()
    {
        base.ViewRender();

        // ---------------------
        var groups = new List<GameEntity> { MyCompany };

        groups.AddRange(Investments.GetOwnings(MyCompany, Q).Where(Companies.IsGroup));

        MyGroupCompaniesListView.SetItems(groups);
        // ---------------------

        var products = new List<GameEntity>();

        var company = MyCompany;

        products.AddRange(Investments.GetOwnings(company, Q).Where(Companies.IsProduct));
        MyProductCompaniesListView.SetItems(products);
        // ----------------------------

        company = SelectedCompany;

        var teams = company.team.Teams;
        SelectedCompanyTeamListView.SetItems(teams);
        // ------------------------

        company = SelectedCompany;

        var team = company.team.Teams[SelectedTeam];

        SelectedTeamEmployeesListView.SetItems(team.Managers.Select(humanId => Humans.Get(Q, humanId)));
    }

    private void OnEnable()
    {
        ScreenUtils.SetSelectedCompany(Q, Flagship.company.Id);
        ScreenUtils.SetSelectedTeam(Q, 0);


        ViewRender();
    }
}
