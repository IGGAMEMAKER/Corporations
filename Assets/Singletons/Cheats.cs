using Assets.Core;
using UnityEngine;

public class Cheats : View
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Economy.IncreaseCompanyBalance(Q, MyCompany.company.Id, 1000000000);
            RefreshPage();
        }
    }
}
