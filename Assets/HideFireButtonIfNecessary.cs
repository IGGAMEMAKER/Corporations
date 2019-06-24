using Assets.Utils;

public class HideFireButtonIfNecessary : HideOnSomeCondition
{
    public override bool HideIfTrue()
    {
        var myCompany = MyProductEntity.company.Id;

        bool worksInMyCompany = HumanUtils.IsWorksInCompany(SelectedHuman, myCompany);

        bool isMe = SelectedHuman.isPlayer;

        return isMe || !worksInMyCompany;
    }
}
