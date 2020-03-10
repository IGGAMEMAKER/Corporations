using Assets.Core;
using UnityEngine;

public class HireWorker : ButtonController
{
    public int companyId;

    public override void Execute()
    {
        var flaghship = Companies.GetFlagship(Q, MyCompany);
        var company = flaghship;


        Teams.HireRegularWorker(company, WorkerRole.Programmer);

        // autohire
        if (Input.GetKey(KeyCode.LeftControl))
        {
            var need = Products.GetNecessaryAmountOfWorkers(company, Q);
            var have = Teams.GetAmountOfWorkers(company, Q);

            var missingWorkers = need - have;

            for (var i = 0; i < missingWorkers; i++)
            {
                Teams.HireRegularWorker(company, WorkerRole.Programmer);
            }
        }
    }
}
