using System;

namespace Assets.Core
{
    public static class EnumUtils
    {
        public static T Next<T>(this T src) where T : struct
        {
            // https://stackoverflow.com/questions/642542/how-to-get-next-or-previous-enum-value-in-c-sharp
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static string GetSingleFormattedNicheName(NicheType niche)
        {
            var name = GetFormattedNicheName(niche);

            switch (niche)
            {
                case NicheType.Com_Messenger: return "Messenger";
                case NicheType.Com_SocialNetwork: return "Social Network";
                case NicheType.Com_Blogs: return "Blog";
                case NicheType.Com_Forums: return "Forum";
                case NicheType.Com_Email: return "Email service";
                case NicheType.Com_Dating: return "Dating app";


                case NicheType.Tech_Clouds: return "Cloud service";
                case NicheType.Tech_OSDesktop: return "Operational System";
                case NicheType.Tech_SearchEngine: return "Search Engine";
                case NicheType.Tech_Browser: return "Web browser";
                case NicheType.Tech_MobileOS: return "Mobile Operational System";

                // entertainment
                case NicheType.Ent_Casino: return "Online Casino";
                case NicheType.Ent_Betting: return "Online Betting App";
                case NicheType.Ent_Lottery: return "Online Lottery App";
                case NicheType.Ent_Poker: return "Online Poker";

                case NicheType.Ent_MMOs: return "MMO game";
                case NicheType.Ent_FreeToPlay: return "F2P Game";
                case NicheType.Ent_SinglePlayer: return "Single player game";


                case NicheType.Ent_VideoHosting: return "Video hosting";
                case NicheType.Ent_StreamingService: return "Streaming Service";
                case NicheType.Ent_TVStreamingService: return "TV Streaming Service";

                // e-commerce
                case NicheType.ECom_Exchanging: return "Money Exchange";
                case NicheType.ECom_OnlineBanking: return "Online Bank";
                case NicheType.ECom_PaymentSystem: return "Payment System";

                case NicheType.ECom_BookingTours: return "Tours Booking App";
                case NicheType.ECom_BookingHotels: return "Hotel Booking App";
                case NicheType.ECom_BookingTransportTickets: return "Airplane/train Booking App";
                case NicheType.ECom_BookingAppartments: return "Rent appartments";

                case NicheType.ECom_OnlineTaxi: return "Online taxi";
                case NicheType.ECom_Marketplace: return "Marketplace";

                // quality of life
                case NicheType.Qol_3DGraphicalEditor: return "3D Editing tool";
                case NicheType.Qol_AdBlocker: return "Advert blocking tool";
                case NicheType.Qol_Antivirus: return "Antivirus";
                case NicheType.Qol_Archivers: return "Archiver";
                case NicheType.Qol_DiskFormattingUtils: return "Disk formatting util";
                case NicheType.Qol_DocumentEditors: return "Document Editor";
                case NicheType.Qol_Encyclopedia: return "Online Encyclopedia";
                case NicheType.Qol_GraphicalEditor: return "2D Editing tool";
                case NicheType.Qol_MusicPlayers: return "Music Player";
                case NicheType.Qol_OnlineEducation: return "Online school";
                case NicheType.Qol_PdfReader: return "PDF reader";
                case NicheType.Qol_RSSReader: return "RSS reader";
                case NicheType.Qol_VideoEditingTool: return "Video editing tool";
                case NicheType.Qol_VideoPlayers: return "Video player";

                case NicheType.Qol_MusicSearch: return "Music search";
                case NicheType.Qol_Maps: return "Online Map";

                default:
                    return name.EndsWith("s") ? name.Substring(0, name.Length - 1) : name;
            }
        }

        public static string GetShortNicheName(NicheType niche)
        {
            switch (niche)
            {
                case NicheType.Com_Messenger: return "MES";
                case NicheType.Com_SocialNetwork: return "SN";
                case NicheType.Com_Blogs: return "BLG";
                case NicheType.Com_Forums: return "FOR";
                case NicheType.Com_Email: return "EML";
                case NicheType.Com_Dating: return "DAT";


                case NicheType.Tech_Clouds: return "CLO";
                case NicheType.Tech_OSDesktop: return "OS";
                case NicheType.Tech_SearchEngine: return "SER";
                case NicheType.Tech_Browser: return "BRW";
                case NicheType.Tech_MobileOS: return "mOS";

                // entertainment
                case NicheType.Ent_Casino: return "CAS";
                case NicheType.Ent_Betting: return "BET";
                case NicheType.Ent_Lottery: return "LOT";
                case NicheType.Ent_Poker: return "POK";

                case NicheType.Ent_MMOs: return "MMO";
                case NicheType.Ent_FreeToPlay: return "F2P";
                case NicheType.Ent_SinglePlayer: return "GAM";


                case NicheType.Ent_VideoHosting: return "VH";
                case NicheType.Ent_StreamingService: return "STR";
                case NicheType.Ent_TVStreamingService: return "TV";

                // e-commerce
                case NicheType.ECom_Exchanging: return "EX$";
                case NicheType.ECom_OnlineBanking: return "BNK";
                case NicheType.ECom_PaymentSystem: return "PAY";

                case NicheType.ECom_BookingTours: return "TOU";
                case NicheType.ECom_BookingHotels: return "BUK";
                case NicheType.ECom_BookingTransportTickets: return "TRS";
                case NicheType.ECom_BookingAppartments: return "APA";

                case NicheType.ECom_OnlineTaxi: return "TAX";
                case NicheType.ECom_Marketplace: return "MKT";
                case NicheType.ECom_Blockchain: return "BLC";
                case NicheType.ECom_TradingBot: return "BOT";



                // quality of life
                case NicheType.Qol_3DGraphicalEditor: return "3DE";
                case NicheType.Qol_AdBlocker: return "ADB";
                case NicheType.Qol_Antivirus: return "AVR";
                case NicheType.Qol_Archivers: return "ARC";
                case NicheType.Qol_DiskFormattingUtils: return "FOR";
                case NicheType.Qol_DocumentEditors: return "DOC";
                case NicheType.Qol_Encyclopedia: return "ENC";
                case NicheType.Qol_GraphicalEditor: return "2DE";
                case NicheType.Qol_MusicPlayers: return "MP";
                case NicheType.Qol_OnlineEducation: return "SCO";
                case NicheType.Qol_PdfReader: return "PDF";
                case NicheType.Qol_RSSReader: return "RSS";
                case NicheType.Qol_VideoEditingTool: return "VET";
                case NicheType.Qol_VideoPlayers: return "VP";

                case NicheType.Qol_MusicSearch: return "MS";
                case NicheType.Qol_Maps: return "MAP";


                default: return niche.ToString();
            }
        }

        public static string GetFormattedMonetisationType(GameEntity niche) => GetFormattedMonetisationType(niche.nicheBaseProfile.Profile.MonetisationType);
        public static string GetFormattedMonetisationType(Monetisation monetisation)
        {
            switch (monetisation)
            {
                case Monetisation.IrregularPaid: return "Irregular paid";
                default: return monetisation.ToString();
            }
        }

        public static string GetFormattedNicheName(NicheType niche)
        {
            switch (niche)
            {
                case NicheType.Com_Messenger: return "Messengers";
                case NicheType.Com_SocialNetwork: return "Social Networks";
                case NicheType.Com_Blogs: return "Blogs";
                case NicheType.Com_Forums: return "Forums";
                case NicheType.Com_Email: return "Email services";
                case NicheType.Com_Dating: return "Dating";


                case NicheType.Tech_Clouds: return "Clouds";
                case NicheType.Tech_OSDesktop: return "Operational Systems";
                case NicheType.Tech_SearchEngine: return "Search Engines";
                case NicheType.Tech_Browser: return "Web browsers";
                case NicheType.Tech_MobileOS: return "Mobile Operational Systems";

                // entertainment
                case NicheType.Ent_Casino: return "Online Casinos";
                case NicheType.Ent_Betting: return "Online Bettings";
                case NicheType.Ent_Lottery: return "Online Lottery";
                case NicheType.Ent_Poker: return "Online Poker";

                case NicheType.Ent_MMOs: return "MMO games";
                case NicheType.Ent_FreeToPlay: return "F2P Games";
                case NicheType.Ent_SinglePlayer: return "Single player games";


                case NicheType.Ent_VideoHosting: return "Video hosting";
                case NicheType.Ent_StreamingService: return "Streaming Services";
                case NicheType.Ent_TVStreamingService: return "TV Streaming Services";

                // e-commerce
                case NicheType.ECom_Exchanging: return "Money Exchange";
                case NicheType.ECom_OnlineBanking: return "Online Banking";
                case NicheType.ECom_PaymentSystem: return "Payment Systems";

                case NicheType.ECom_BookingTours: return "Booking Tours";
                case NicheType.ECom_BookingHotels: return "Booking hotels";
                case NicheType.ECom_BookingTransportTickets: return "Online airplane/train booking";
                case NicheType.ECom_BookingAppartments: return "Rent appartments";

                case NicheType.ECom_OnlineTaxi: return "Online taxi";
                case NicheType.ECom_Marketplace: return "Marketplaces";
                case NicheType.ECom_Blockchain: return "Blockchain";
                case NicheType.ECom_TradingBot: return "Trading bot";



                // quality of life
                case NicheType.Qol_3DGraphicalEditor: return "3D Editing tools";
                case NicheType.Qol_AdBlocker: return "Advert blocking tools";
                case NicheType.Qol_Antivirus: return "Antiviruses";
                case NicheType.Qol_Archivers: return "Archivers";
                case NicheType.Qol_DiskFormattingUtils: return "Disk formatting util";
                case NicheType.Qol_DocumentEditors: return "Document Editors";
                case NicheType.Qol_Encyclopedia: return "Online Encyclopedia";
                case NicheType.Qol_GraphicalEditor: return "2D Editing tools";
                case NicheType.Qol_MusicPlayers: return "Music Players";
                case NicheType.Qol_OnlineEducation: return "Online schools";
                case NicheType.Qol_PdfReader: return "PDF readers";
                case NicheType.Qol_RSSReader: return "RSS readers";
                case NicheType.Qol_VideoEditingTool: return "Video editing tool";
                case NicheType.Qol_VideoPlayers: return "Video players";

                case NicheType.Qol_MusicSearch: return "Music search";
                case NicheType.Qol_Maps: return "Maps";


                default: return niche.ToString();
            }
        }

        public static string GetFormattedIndustryName(IndustryType industry)
        {
            switch (industry)
            {
                //case IndustryType.OS: return "Operation Systems";
                case IndustryType.Technology: return "Technology";
                case IndustryType.Communications: return "Social Media";
                case IndustryType.Entertainment: return "Entertainment";

                case IndustryType.Ecommerce: return "E-commerce";
                    case IndustryType.Finances: return "Finances";
                    case IndustryType.Tourism: return "Tourism";

                case IndustryType.WorkAndLife: return "Other";

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
                case CompanyType.FinancialGroup: return "Investment Company";
                case CompanyType.Group: return "Group of companies";

                default: return "WUT " + companyType.ToString();
            }
        }
    }
}
