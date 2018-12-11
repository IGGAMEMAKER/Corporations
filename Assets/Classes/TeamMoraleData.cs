namespace Assets.Classes
{
    public struct TeamMoraleData
    {
        public bool isMakingMoney;
        public bool isTopCompany;
        public bool isTeam;
        public bool isInnovative;

        public int teamSizePenalty;
        public int salaries;

        public int Morale
        {
            get
            {
                // set base value
                int value = Balance.MORALE_BONUS_BASE;

                if (isMakingMoney)
                    value += Balance.MORALE_BONUS_IS_PROFITABLE;

                if (isTopCompany)
                    value += Balance.MORALE_BONUS_IS_PRESTIGEOUS_COMPANY;

                if (isInnovative)
                    value += Balance.MORALE_BONUS_IS_INNOVATIVE;

                if (isTeam)
                    value += Balance.MORALE_BONUS_IS_TEAM;

                value += salaries;
                value += teamSizePenalty;

                return value;
            }
        }
        
        public TeamMoraleData (bool isMakingMoney, bool isTopCompany, bool isTeam, bool isInnovative, int teamSizePenalty, int salaries)
        {
            this.isMakingMoney = isMakingMoney;
            this.isTopCompany = isTopCompany;
            this.isTeam = isTeam;
            this.isInnovative = isInnovative;

            this.teamSizePenalty = teamSizePenalty;
            this.salaries = salaries;
        }
    }
}
