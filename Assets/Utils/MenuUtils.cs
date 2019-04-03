using Entitas;
using System;

namespace Assets.Utils
{
    public static class MenuUtils
    {
        public static GameEntity GetMenu(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Menu)[0];
        }

        public static void SetSelectedCompany(int companyId, GameContext gameContext)
        {
            gameContext.GetEntities(GameMatcher.SelectedCompany)[0].isSelectedCompany = false;

            var company = Array.Find(gameContext.GetEntities(GameMatcher.Company), c => c.company.Id == companyId);

            company.isSelectedCompany = true;
        }

        public static void SetSelectedNiche(NicheType niche, GameContext gameContext)
        {
            
        }
    }
}
