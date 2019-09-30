using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCompanyController : ButtonController
{
    public override void Execute()
    {
        SelectedCompany.AddResearch(1);
    }
}
