using Entitas;

namespace Assets.Utils
{
    public static class CompanyUtils
    {
        public static int GenerateCompanyId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company).Length;
        }

        public static int GenerateShareholderId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Shareholder).Length;
        }
    }
}
