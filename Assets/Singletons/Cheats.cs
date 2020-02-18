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

        if (Input.GetKeyDown(KeyCode.F9))
        {
            Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_BookingTours, MyCompany);
            Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_BookingHotels, MyCompany);
            Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_PaymentSystem, MyCompany);
        }
    }
}
