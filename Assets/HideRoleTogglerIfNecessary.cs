using Assets.Utils;

public class HideRoleTogglerIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var myCompany = MyProductEntity.company.Id;

        bool worksInMyCompany = HumanUtils.IsWorksInCompany(SelectedHuman, myCompany);

        return !worksInMyCompany;
    }
}
