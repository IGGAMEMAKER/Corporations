namespace Assets.Utils.Formatting
{
    public static class EnumFormattingUtils
    {
        public static string GetFormattedNicheName(NicheType niche)
        {
            switch (niche)
            {
                case NicheType.CloudComputing: return "Cloud\nComputing";
                case NicheType.Messenger: return "Messengers";
                case NicheType.SocialNetwork: return "Social\nNetworks";
                case NicheType.OSSciencePurpose: return "Science\nPurpose OS";
                case NicheType.OSCommonPurpose: return "Desktop OS";
                case NicheType.SearchEngine: return "Search\nEngines";

                default: return "Unknown niche: " + niche.ToString();
            }
        }

        public static string GetFormattedIndustryName(IndustryType industry)
        {
            switch (industry)
            {
                case IndustryType.OS: return "Operation Systems";
                case IndustryType.Search: return "Search Engines";
                case IndustryType.CloudComputing: return "Cloud computing";
                case IndustryType.Communications: return "Communications";

                default: return "Unknown Industry: " + industry.ToString();
            }
        }

        public static string GetFormattedCompanyType(CompanyType companyType)
        {
            switch (companyType)
            {
                case CompanyType.ProductCompany: return "Product Company";
                case CompanyType.Holding: return "Holding Company";
                case CompanyType.Corporation: return "Corporation";
                case CompanyType.FinancialGroup: return "Financial Group";
                case CompanyType.Group: return "Group of companies";

                default: return "WUT " + companyType.ToString();
            }
        }
    }
}
