using Assets.Core;
using UnityEngine;

public class Cheats : View
{
    void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Companies.AddResources(MyCompany, 1000000000, "CHEATS");
            Refresh();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //foreach (var e in (TutorialFunctionality[])System.Enum.GetValues(typeof(TutorialFunctionality)))
            TutorialUtils.Unlock(Q, TutorialFunctionality.UnlockAll);
        }


        if (Input.GetKeyDown(KeyCode.F6))
        {
            Companies.CreateProductAndAttachItToGroup(Q, NicheType.Com_Blogs, MyCompany);
            Refresh();
        }
        #endif

        //if (Input.GetKeyDown(KeyCode.F9))
        //{
        //    Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_BookingTours, MyCompany);
        //    Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_BookingHotels, MyCompany);
        //    Companies.CreateProductAndAttachItToGroup(Q, NicheType.ECom_PaymentSystem, MyCompany);
        //}
    }
}
