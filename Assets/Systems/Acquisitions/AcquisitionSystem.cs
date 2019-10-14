using Assets.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AcquisitionSystem : OnDateChange
{
    public AcquisitionSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //foreach (var e in CompanyUtils.GetAIManagingCompanies(gameContext))

        //foreach (var e in CompanyUtils.GetAIProducts(gameContext))
    }
}