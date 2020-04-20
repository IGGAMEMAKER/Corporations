using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePersonButtons : View
{
    public GameObject Hire;
    public GameObject Fire;

    public override void ViewRender()
    {
        base.ViewRender();

        var human = SelectedHuman;

        var flagshipId = Companies.GetPlayerFlagshipID(Q);

        bool worksInMyCompany = Humans.IsWorksInCompany(human, MyCompany.company.Id) || Humans.IsWorksInCompany(human, flagshipId);

        bool isPlayer = human.isPlayer;

        // Hire
        Hire.SetActive(!isPlayer && !worksInMyCompany);
        Fire.SetActive(!isPlayer && worksInMyCompany);
    }
}
