using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePersonButtons : View
{
    public GameObject Hire;
    public GameObject Fire;

    public GameObject PersonalCapitalLabel;
    public GameObject PersonalCapital;
    public GameObject ForbesLink;


    public override void ViewRender()
    {
        base.ViewRender();

        var myCompany = MyCompany.company.Id;
        var human = SelectedHuman;

        bool worksInMyCompany = Humans.IsWorksInCompany(SelectedHuman, myCompany);

        bool isMe = SelectedHuman.isPlayer;

        // Hire
        Hire.SetActive(!isMe && !worksInMyCompany);
        Fire.SetActive(!isMe && worksInMyCompany);

        // Personal Capital
        var ownsOrManagesCompany = false;
        PersonalCapitalLabel.SetActive(ownsOrManagesCompany);
        PersonalCapital.SetActive(ownsOrManagesCompany);
        ForbesLink.SetActive(ownsOrManagesCompany);
    }
}
