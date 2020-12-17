using Assets.Core;
using UnityEngine;

public class RenderCorporateCultureView : View
{
    public GameObject StructurePolicy;

    public GameObject ResponsibilityPolicy;

    public GameObject PeopleOrProcessesPolicy;

    public GameObject SalariesPolicy;

    public GameObject SoftSkillsPolicy;


    public override void ViewRender()
    {
        base.ViewRender();

        bool canChangeCulture = Companies.GetPolicyValue(MyCompany, CorporatePolicy.DoOrDelegate) > 3;

        Draw(ResponsibilityPolicy, true);
        
        Draw(StructurePolicy, canChangeCulture);
        Draw(PeopleOrProcessesPolicy, canChangeCulture);
        Draw(SalariesPolicy, false);

        Draw(SoftSkillsPolicy, canChangeCulture);
    }
}
