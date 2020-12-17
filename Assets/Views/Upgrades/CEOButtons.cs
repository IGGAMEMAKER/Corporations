using Assets.Core;
using UnityEngine;

public class CEOButtons : RoleRelatedButtons
{
    public GameObject[] HiringManagers;

    internal override void Render(GameEntity company)
    {
        base.Render(company);

        bool isCEO = HasWorker(WorkerRole.CEO, company);

        foreach (var manager in HiringManagers)
        {
            var role = manager.GetComponent<HireManagerByRole>().WorkerRole;

            bool thereAreManagersWithSuchRole = Teams.GetCandidatesForRole(Flagship, Q, role).Count > 0;

            Draw(manager, thereAreManagersWithSuchRole && CanHireManager(role, company) && isCEO);
        }
    }
}
