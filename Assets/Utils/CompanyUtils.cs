using Entitas;
using System;

namespace Assets.Utils
{
    public static class CompanyUtils
    {
        public static int GenerateCompanyId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company).Length;
        }

        public static GameEntity GetCompanyById(GameContext context, int companyId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Id == companyId);
        }

        public static int GenerateShareholderId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Shareholder).Length;
        }


    }
}
