using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FillGroupOwnings : View
    //, IMenuListener
    //, IAnyShareholdersListener
{
    void Render()
    {
        Debug.Log("Render Group ownings");
        GetComponent<OwningsListView>().SetItems(HasGroupCompany ? GetOwnings() : new GameEntity[0]);
    }

    GameEntity[] GetOwnings()
    {
        return CompanyUtils.GetDaughterCompanies(GameContext, MyGroupEntity.company.Id);
    }
}
