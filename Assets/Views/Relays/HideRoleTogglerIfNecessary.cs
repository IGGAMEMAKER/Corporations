public class HideRoleTogglerIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return true;

        //var myCompany = MyCompany.company.Id;

        //bool worksInMyCompany = Humans.IsWorksInCompany(SelectedHuman, myCompany);

        //return !worksInMyCompany;
    }
}
