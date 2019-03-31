using System;
using Entitas;

namespace Assets.Utils
{
    public static class CompanyUtils
    {
        public static int GenerateCompanyId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company).Length;
        }

        internal static int GenerateShareholderId()
        {
            throw new NotImplementedException();
        }
    }
}
