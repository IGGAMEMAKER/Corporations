using Entitas;
using System;

namespace Assets.Core
{
    public enum Ambition
    {
        EarnMoney,
        //RuleProductCompany,

        RuleCorporation
    }
    public static partial class HumanUtils
    {
        // ambitions
        public static Ambition GetFounderAmbition(GameContext gameContext, int humanId)
        {
            var human = GetHuman(gameContext, humanId);

            return GetFounderAmbition(human.humanSkills.Traits[TraitType.Ambitions]);
        }

        public static Ambition GetFounderAmbition(int ambitions)
        {
            //if (ambitions < 75)
            //    return Ambition.RuleProductCompany;

            if (ambitions < 85)
                return Ambition.EarnMoney;

            return Ambition.RuleCorporation;
        }
    }
}
