using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : View
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            EconomyUtils.IncreaseCompanyBalance(GameContext, MyCompany.company.Id, 1000000000);
            RefreshPage();
        }
    }
}
