namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static GameEntity TurnToHolding(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            c.ReplaceCompany(c.company.Id, c.company.Name, CompanyType.Holding);

            return c;
        }
    }
}
