using Assets.Utils;
using Assets.Utils.Formatting;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);

        var startCapital = NicheUtils.GetStartCapital(nicheType, GameContext);

        //if (!CompanyUtils.IsEnoughResources(MyCompany, new Assets.Classes.TeamResource(startCapital)))
        //    return;

        CompanyUtils.SpendResources(MyCompany, startCapital);


        string name = MyCompany.company.Name + " " + EnumUtils.GetFormattedNicheName(nicheType);
        var c = CompanyUtils.GenerateProductCompany(GameContext, name, nicheType);


        CompanyUtils.AttachToGroup(GameContext, MyCompany.company.Id, c.company.Id);
    }
}
