using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyProductCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyPreviewView>().SetEntity(entity as GameEntity);

        //var link = t.GetComponent<LinkToProjectView>();

        //if (link != null)
        //    Destroy(link);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        var company = Item.GetComponent<CompanyPreviewView>().Company;

        FindObjectOfType<HierarchyView>().ChooseProduct(company);
    }

    //public override void ViewRender()
    //{
    //    base.ViewRender();

    //    var myCompanies = new List<GameEntity>();

    //    var company = SelectedCompany;

    //    myCompanies.AddRange(Investments.GetOwnings(company, Q).Where(Companies.IsProduct));

    //    SetItems(myCompanies);
    //}
}
