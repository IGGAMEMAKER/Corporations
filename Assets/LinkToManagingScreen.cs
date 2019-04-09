using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToManagingScreen : ButtonController
{
    public override void Execute()
    {
        //  MyGroupEntity.company.Id

        if (SelectedCompany == MyGroupEntity)
            Navigate(ScreenMode.GroupManagementScreen, null);
        else if (SelectedCompany == MyProductEntity)
            Navigate(ScreenMode.DevelopmentScreen, null);
    }
}
