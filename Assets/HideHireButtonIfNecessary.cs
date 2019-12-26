using Assets.Core;

public class HideHireButtonIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var myCompany = MyCompany.company.Id;

        bool worksInMyCompany = HumanUtils.IsWorksInCompany(SelectedHuman, myCompany);

        bool isMe = SelectedHuman.isPlayer;

        return isMe || worksInMyCompany;
    }
}
