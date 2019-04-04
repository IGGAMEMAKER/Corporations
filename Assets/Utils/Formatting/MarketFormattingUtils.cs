namespace Assets.Utils.Formatting
{
    public static class MarketFormattingUtils
    {
        public static string GetFormattedNicheName(NicheType niche)
        {
            switch (niche)
            {
                case NicheType.CloudComputing: return "Cloud\nComputing";
                case NicheType.Messenger: return "Messengers";
                case NicheType.SocialNetwork: return "Social\nNetworks";
                case NicheType.OSSciencePurpose: return "Science\nPurpose";
                case NicheType.OSCommonPurpose: return "Desktop";
                case NicheType.SearchEngine: return "Search\nEngines";

                default: return "Unknown niche: " + niche.ToString();
            }
        }

        public static string GetFormattedIndustryName(IndustryType industry)
        {
            switch (industry)
            {
                case IndustryType.CloudComputing: return "Cloud computing";
                case IndustryType.Communications: return "Communications";
                case IndustryType.OS: return "Operation Systems";
                case IndustryType.Search: return "Search Engines";

                default: return "Unknown Industry: " + industry.ToString();
            }
        }
    }
}
