using Assets.Core;
using UnityEngine;

public class Cheats : View
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Economy.IncreaseCompanyBalance(Q, MyCompany.company.Id, 1000000000);
            Refresh();
        }

        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.W))
        {
            //foreach (var e in (TutorialFunctionality[])System.Enum.GetValues(typeof(TutorialFunctionality)))
            TutorialUtils.Unlock(Q, TutorialFunctionality.UnlockAll);
        }


        #endif
        if (Input.GetKeyDown(KeyCode.F6))
        {
            Companies.CreateProductAndAttachItToGroup(Q, NicheType.Com_Blogs, MyCompany);
            Refresh();
        }

        //if (Input.GetKeyDown(KeyCode.F9))
        //{
        //    Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_BookingTours, MyCompany);
        //    Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_BookingHotels, MyCompany);
        //    Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_PaymentSystem, MyCompany);
        //}
    }
}
