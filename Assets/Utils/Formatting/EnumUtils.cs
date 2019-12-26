﻿using System;

namespace Assets.Utils.Formatting
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

            if (name.EndsWith("s"))
                return name.Substring(0, name.Length - 1);
            else
                return name;
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
                case NicheType.Tech_MobileOS: return "MOS";

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

                case NicheType.ECom_EventTickets: return "TKT";
                case NicheType.ECom_Tourism: return "TOU";
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
                case NicheType.Qol_OnlineGenealogy: return "GEN";


                default: return niche.ToString();
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

                case NicheType.ECom_EventTickets: return "Event tickets booking";
                case NicheType.ECom_Tourism: return "Booking Tours";
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
                case NicheType.Qol_OnlineGenealogy: return "Online genealogy";


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
                case IndustryType.WorkAndLife: return "Other";
                case IndustryType.Ecommerce: return "E-commerce";

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
