using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyGroupCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyPreviewView>().SetEntity(entity as GameEntity);
        Destroy(t.GetComponent<LinkToProjectView>());
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        var company = Item.GetComponent<CompanyPreviewView>().Company;

        FindObjectOfType<HierarchyView>().ChooseGroup(company);
    }

    //public override void ViewRender()
    //{
    //    base.ViewRender();

    //    var myGroups = new List<GameEntity>();

    //    myGroups.Add(MyCompany);

    //    myGroups.AddRange(Investments.GetOwnings(MyCompany, Q).Where(Companies.IsGroup));

    //    SetItems(myGroups);
    //}
}
