using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToManagingScreen : ButtonController
{
    public override void Execute()
    {
        if (SelectedCompany == MyGroupEntity)
            Navigate(ScreenMode.GroupManagementScreen);
        else if (SelectedCompany == MyProductEntity)
            Navigate(ScreenMode.DevelopmentScreen);
    }
}
