using Assets.Core;
using UnityEngine;

public class ManagePersonButtons : View
{
    public GameObject Hire;
    public GameObject Fire;

    public GameObject KeyWorker;
    public GameObject Vacation;
    public GameObject Educate;
    public GameObject DelaySalaries;

    public override void ViewRender()
    {
        base.ViewRender();

        var human = SelectedHuman;

        var flagshipId = Companies.GetPlayerFlagshipID(Q);

        bool worksInMyCompany = Humans.IsWorksInCompany(human, MyCompany.company.Id) || Humans.IsWorksInCompany(human, flagshipId);

        bool isPlayer = human.isPlayer;
        bool isEmployed = Humans.IsEmployed(human);

        // Hire
        Hire.SetActive(!isPlayer && !worksInMyCompany);
        Fire.SetActive(!isPlayer && worksInMyCompany);

        Draw(KeyWorker,     isEmployed && worksInMyCompany && !isPlayer);
        Draw(Vacation,      isEmployed && worksInMyCompany && !isPlayer && false);
        Draw(Educate,       isEmployed && worksInMyCompany && !isPlayer && false);
        Draw(DelaySalaries, isEmployed && worksInMyCompany && !isPlayer && false);
    }
}
