using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HierarchyView : View
{
    public MyGroupCompaniesListView MyGroupCompaniesListView;
    public MyProductCompaniesListView MyProductCompaniesListView;
    public SelectedCompanyTeamListView SelectedCompanyTeamListView;
    public SelectedTeamEmployeesListView SelectedTeamEmployeesListView;

    public Text MainCompanyName;
    public Text SelectedProductsName;
    public Text SelectedCompanyTeamsName;

    GameEntity SelectedGroupEntity = null;
    GameEntity SelectedProductEntity = null;

    public override void ViewRender()
    {
        base.ViewRender();

        MainCompanyName.text = $"Hierarchy of {MyCompany.company.Name}";

        // ---------------------
        RenderGroups();

        // Managers
        //company = SelectedCompany;

        //var team = company.team.Teams[SelectedTeam];

        //SelectedTeamEmployeesListView.SetItems(team.Managers.Select(humanId => Humans.Get(Q, humanId)));
    }

    void RenderGroups()
    {
        var groups = new List<GameEntity> { MyCompany };

        groups.AddRange(Investments.GetOwnings(MyCompany, Q).Where(Companies.IsGroup));

        MyGroupCompaniesListView.SetItems(groups);

        // ---------------------
        RenderProducts();
    }

    void RenderProducts()
    {
        var products = new List<GameEntity>();

        var company = SelectedGroupEntity;

        products.AddRange(Investments.GetOwnings(company, Q).Where(Companies.IsProduct));

        SelectedProductsName.text = $"{products.Count} Products attached to {company.company.Name}";

        MyProductCompaniesListView.SetItems(products);

        if (products.Count == 0)
        {
            Draw(SelectedCompanyTeamListView, false);
        }
        else
        {
            Draw(SelectedCompanyTeamListView, true);
            RenderTeams();
        }
    }

    void RenderTeams()
    {
        var company = SelectedProductEntity;

        SelectedCompanyTeamsName.text = $"Teams in {company.company.Name}";

        var teams = company.team.Teams;
        SelectedCompanyTeamListView.SetItems(teams);
    }

    private void OnEnable()
    {
        //ScreenUtils.SetSelectedCompany(Q, Flagship.company.Id);
        //ScreenUtils.SetSelectedTeam(Q, 0);

        if (SelectedGroupEntity == null)
            SelectedGroupEntity = MyCompany;

        if (SelectedProductEntity == null)
            SelectedProductEntity = Flagship;

        ViewRender();
    }

    public void ChooseProduct(GameEntity company)
    {
        SelectedProductEntity = company;

        ViewRender();
    }

    public void ChooseGroup(GameEntity company)
    {
        SelectedGroupEntity = company;

        SelectedProductEntity = Companies.GetDaughterProducts(Q, company).FirstOrDefault();

        ViewRender();
    }
}
