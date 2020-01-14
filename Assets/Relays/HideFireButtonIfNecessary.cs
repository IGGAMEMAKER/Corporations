using Assets.Core;

public class HideFireButtonIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var myCompany = MyCompany.company.Id;

        bool worksInMyCompany = Humans.IsWorksInCompany(SelectedHuman, myCompany);

        bool isMe = SelectedHuman.isPlayer;

        return isMe || !worksInMyCompany;
    }
}
