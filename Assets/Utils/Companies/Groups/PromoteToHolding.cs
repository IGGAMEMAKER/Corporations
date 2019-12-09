namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static GameEntity TurnToHolding(GameContext context, int companyId)
        {
            var c = GetCompany(context, companyId);

            c.ReplaceCompany(c.company.Id, c.company.Name, CompanyType.Holding);

            return c;
        }
    }
}
