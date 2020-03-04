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

        var human = SelectedHuman;


        var myCompany = MyCompany.company.Id;

        var flagship = Companies.GetFlagship(Q, MyCompany);
        var flagshipId = flagship?.company.Id ?? -1;

        bool worksInMyCompany = Humans.IsWorksInCompany(human, myCompany) || Humans.IsWorksInCompany(human, flagshipId);

        bool isMe = human.isPlayer;

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
