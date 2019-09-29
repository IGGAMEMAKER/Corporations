using Assets.Utils.Formatting;
using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        static float GetRandomColor(int companyId, int mixer)
        {
            return GetHashedRandom(companyId, mixer);
        }

        public static int GetCEOId(GameEntity company)
        {
            return company.cEO.HumanId;
        }

        public static int GetAccumulatedExpertise(GameEntity company)
        {
            var exp = company.expertise.ExpertiseLevel;

            return Mathf.Clamp((int)(10f * Mathf.Log(1.2f, exp)), 0, 60);
        }

        public static int GetCompanyExpertise (GameEntity company)
        {
            //var CEOId = 
            int companyId = company.company.Id;
            int CEOId = GetCEOId(company);

            //var accumulated = GetAccumulatedExpertise(company);

            return 35 + (int)(30 * GetHashedRandom2(companyId, CEOId));
            //return 35 + (int)(30 * GetHashedRandom2(companyId, CEOId) + accumulated);
        }

        internal static long GetMarketStageInnovationModifier (GameEntity company, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);

            return GetMarketStageInnovationModifier(niche);
        }

        // TODO move this to niche utils!!!
        public static string GetMarketStateDescription(NicheLifecyclePhase state)
        {
            switch (state)
            {
                case NicheLifecyclePhase.Idle: return "???";
                case NicheLifecyclePhase.Innovation: return "Innovation";
                case NicheLifecyclePhase.Trending: return "Trending";
                case NicheLifecyclePhase.MassUse: return "Mass use";
                case NicheLifecyclePhase.Decay: return "Decay";
                case NicheLifecyclePhase.Death: return "Death";

                default: return "???WTF " + state.ToString();
            }
        }

        internal static long GetMarketStageInnovationModifier (GameEntity niche)
        {
            var phase = NicheUtils.GetMarketState(niche);

            switch (phase)
            {
                case NicheLifecyclePhase.Death:
                case NicheLifecyclePhase.Decay:
                case NicheLifecyclePhase.Idle:
                    return 0;

                case NicheLifecyclePhase.Innovation:
                    return 25;

                case NicheLifecyclePhase.Trending:
                    return 15;

                case NicheLifecyclePhase.MassUse:
                    return 5;

                default: return 0;
            }
        }

        public static BonusContainer GetInnovationChanceDescription(GameEntity company, GameContext gameContext)
        {
            var morale = company.team.Morale;

            var moraleChance = morale / 5; // 0...20
            var expertiseChance = Mathf.Clamp(company.expertise.ExpertiseLevel, 0, 25);

            var crunch = company.isCrunching ? 10 : 0;


            var sphereOfInterestBonus = 0;

            if (!company.isIndependentCompany)
            {
                var parent = GetParentCompany(gameContext, company);

                if (parent != null)
                {
                    if (IsInSphereOfInterest(parent, company.product.Niche))
                        sphereOfInterestBonus = 10;
                }
            }

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var phase = NicheUtils.GetMarketState(niche);
            var marketStage = GetMarketStageInnovationModifier(niche);

            return new BonusContainer("Innovation chance")
                .Append("Base", 15)
                //.Append("Morale", moraleChance)
                .Append("Market stage " + CompanyUtils.GetMarketStateDescription(phase), marketStage)
                .AppendAndHideIfZero("Is fully focused on market", company.isIndependentCompany ? 25 : 0)
                .AppendAndHideIfZero("Parent company focuses on this company market", sphereOfInterestBonus)
                .AppendAndHideIfZero("Crunch", crunch);
                //.Append("Expertise", expertiseChance);
        }
        public static int GetInnovationChance (GameEntity company, GameContext gameContext)
        {
            var chance = GetInnovationChanceDescription(company, gameContext);

            return (int) chance.Sum();
        }

        public static Color GetCompanyUniqueColor(int companyId)
        {
            var r = GetRandomColor(companyId, companyId);
            var g = GetRandomColor(companyId, companyId + 1);
            var b = GetRandomColor(companyId, companyId + 2);

            return new Color(r, g, b);
        }

        // 0... 1
        static float GetHashedRandom(int companyId, int mixer)
        {
            var C = 0.61f;

            var K = companyId + mixer + GetSeedValue();

            return ((C * K) % 1);
        }

        static float GetHashedRandom2(int companyId, int mixer)
        {
            var C = 0.58f;

            var K = 2 * companyId + mixer + GetSeedValue2();

            return ((C * K) % 1);
        }

        static float GetRandomAcquisitionPriceModifier(int companyId, int shareholderId)
        {
            var min = 0.9f;
            var max = 3f;

            var M = max - min;
            var C = 0.61f;
            var K = companyId + shareholderId + GetSeedValue();

            return min + M * ((C * K) % 1);

            return 1;
            //return Mathf.Clamp(value, 0.9f, 3f);
        }

        static int GetSeedValue()
        {
            return DateTime.Now.Hour;
        }

        static int GetSeedValue2()
        {
            return DateTime.Now.Hour;
        }

        public static long GetRandomValue (long baseValue, int id1, int id2, float min = 0.4f, float max = 1.35f)
        {
            return (long)(baseValue * (min + (max - min) * GetHashedRandom2(id1, id2)));
        }
    }
}
