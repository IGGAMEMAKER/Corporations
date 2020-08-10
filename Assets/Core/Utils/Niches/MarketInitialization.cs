using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static GameEntity GetMarketingChannel(GameContext gameContext, int channelId)
        {
            var channels = GetMarketingChannels(gameContext);

            return channels.First(c => c.marketingChannel.ChannelInfo.ID == channelId);
        }

        public static GameEntity[] GetMarketingChannels(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.MarketingChannel);
        }

        public static int GetAmountOfAvailableChannels(GameContext gameContext, GameEntity product)
        {
            var productLevel = Products.GetProductLevel(product);

            var amountOfChannels = 0;

            if (productLevel <= 1)
            {
                amountOfChannels = 1;
            }
            else if (productLevel <= 5)
            {
                amountOfChannels = 3;
            }
            else
            {
                amountOfChannels = 3 + (int)Mathf.Pow(productLevel, 0.5f);
            }

            // if player flagship
            // explore channels manually
            if (product.hasChannelExploration)
            {
                amountOfChannels = product.channelExploration.AmountOfExploredChannels;
            }

            return amountOfChannels;
        }

        public static Func<GameEntity, bool> IsReachableChannel(long companyCost, GameEntity company, GameContext gameContext, bool ShowActiveChannelsToo) => (GameEntity c) =>
        {
            var activeInChannel = Marketing.IsCompanyActiveInChannel(company, c);

            if (activeInChannel)
                return ShowActiveChannelsToo;

            var bigLoan = companyCost * 4 / 100;
            var activityCost = Marketing.GetMarketingActivityCost(company, gameContext, c);

            // can afford
            return activityCost < bigLoan;
        };

        public static GameEntity[] GetAvailableMarketingChannels(GameContext gameContext, GameEntity product, bool includeActive)
        {
            var channels = GetMarketingChannels(gameContext);
            var availableChannels = channels;

            var clients = Marketing.GetClients(product);

            var companyCost = Economy.GetCompanyCost(gameContext, product);

            var newChannels = availableChannels
                .Where(IsReachableChannel(companyCost, product, gameContext, includeActive));

            if (newChannels.Count() == 0)
            {
                // ensure, that we have at least one channel
                newChannels = availableChannels
                    .OrderBy(c => c.marketingChannel.ChannelInfo.Audience)
                    .Take(1);
            }

            return newChannels.ToArray();
        }

        public static void SpawnMarketingChannels(GameContext gameContext)
        {
            // markets:

            // small <1K Batch (little budget for marketing)
            // avg < 100K Batch
            // big < 1M Batch
            // enormous 1M+ (~5M) (big budget for marketing)

            // SPEED

            // active channel (can take it fast big reach: 7% / w)
            // avg (avg reach 2%)
            // non-active (<1% / month)

            // Additional bonus (channel theme matches with our product)

            // cheap
            // avg
            // expensive

            var i = 0;
            SpawnChannelSet(500, 7, gameContext, 1, true, ref i);
            SpawnChannelSet(1500, 12, gameContext, 2, true, ref i);

            SpawnChannelSet(5000, 8, gameContext, 2, false, ref i);
            SpawnChannelSet(25000, 5, gameContext, 3, false, ref i);
            SpawnChannelSet(100000, 3, gameContext, 4, false, ref i);
            SpawnChannelSet(500000, 2, gameContext, 4, false, ref i);
            SpawnChannelSet(1000000, 1, gameContext, 5, false, ref i);
        }

        static void SpawnChannelSet(long audience1, int amountOfChannels, GameContext gameContext, int costInWorkers, bool isFree, ref int i)
        {
            for (var j = 0; j < amountOfChannels; j++)
                SpawnChannel(audience1, gameContext, costInWorkers, isFree, ref i);
        }

        static void SpawnChannel(long batch, GameContext gameContext, int costInWorkers, bool isFree, ref int i)
        {
            var channelType = RandomEnum<ClientContainerType>.GenerateValue(ClientContainerType.ProductCompany);
            long audience = batch * 100; // * Random.Range(0.85f, 1.1f);

            var relativeCost = Random.Range(0.5f, 3f);

            var e = gameContext.CreateEntity();
            e.AddMarketingChannel(audience, channelType, new ChannelInfo
            {
                ID = i++,

                Audience = audience,
                Batch = batch,
                costPerAd = isFree ? 0 : Mathf.Pow(batch, 1.12f) * relativeCost,
                relativeCost = relativeCost,
                costInWorkers = costInWorkers,

                Companies = new Dictionary<int, long>()
            });

            e.AddChannelMarketingActivities(new Dictionary<int, long>());
        }



        public static void SpawnMarkets(GameContext gameContext)
        {
            InitializeIndustries(gameContext);

            InitializeFundamentalIndustry(gameContext);
            InitializeCommunicationsIndustry(gameContext);
            InitializeEntertainmentIndustry(gameContext);

            InitializeEcommerceIndustry(gameContext);
            InitializeFinancesIndustry(gameContext);
            InitializeTourismIndustry(gameContext);

            InitializeUsefulAppsIndustry(gameContext);

            //CheckIndustriesWithZeroNiches();
        }




        //void CheckIndustriesWithZeroNiches()
        //{
        //    foreach (IndustryType industry in (IndustryType[])Enum.GetValues(typeof(IndustryType)))
        //    {
        //        if (NicheUtils.GetNichesInIndustry(industry, GameContext).Length == 0)
        //            Debug.LogWarning("Industry " + industry.ToString() + " has zero niches! Fill it!");
        //    }
        //}

        public static void InitializeIndustries(GameContext gameContext)
        {
            foreach (IndustryType industry in (IndustryType[])System.Enum.GetValues(typeof(IndustryType)))
                gameContext.CreateEntity().AddIndustry(industry);
        }

        public static GameEntity SetNicheCosts(NicheType niche, float newBasePrice, long newClientBatch, int newTechCost, float newAdCost, GameContext gameContext) => SetNicheCosts(Get(gameContext, niche), newBasePrice, newClientBatch, newTechCost, newAdCost);
        public static GameEntity SetNicheCosts(GameEntity e, float newBasePrice, long newClientBatch, int newTechCost, float newAdCost)
        {
            e.ReplaceNicheCosts(newBasePrice, newClientBatch, newTechCost, newAdCost);

            return e;
        }


        //public static GameEntity GetNiche(NicheType nicheType, GameContext gameContext) => Markets.GetNiche(gameContext, nicheType);

        public static GameEntity AttachNicheToIndustry(NicheType niche, IndustryType industry, GameContext gameContext)
        {
            var e = Get(gameContext, niche);

            e.ReplaceNiche(
                e.niche.NicheType,
                industry,
                e.niche.MarketCompatibilities,
                e.niche.CompetingNiches,
                e.niche.Parent
                );



            return e;
        }

        public static void AttachNichesToIndustry(IndustryType industry, NicheType[] nicheTypes, GameContext gameContext)
        {
            foreach (var n in nicheTypes)
                AttachNicheToIndustry(n, industry, gameContext);
        }


        //public static GameEntity ForkNiche(NicheType parent, NicheType child)
        //{
        //    var e = GetNiche(child);

        //    e.ReplaceNiche(
        //        e.niche.NicheType,
        //        e.niche.IndustryType,
        //        e.niche.MarketCompatibilities,
        //        e.niche.CompetingNiches,
        //        parent
        //        );

        //    return e;
        //}

        //public static void AddSynergicNiche(GameEntity entity, NicheType niche, int compatibility)
        //{
        //    var list = entity.niche.MarketCompatibilities;
        //    var n = entity.niche;

        //    list.Add(new MarketCompatibility { Compatibility = compatibility, NicheType = niche });

        //    entity.ReplaceNiche(n.NicheType, n.IndustryType, list, n.CompetingNiches, n.Parent);
        //}

        //public static void SetNichesAsSynergic(NicheType niche1, NicheType niche2, int compatibility)
        //{
        //    var n1 = GetNiche(niche1);
        //    var n2 = GetNiche(niche2);

        //    AddSynergicNiche(n1, niche2, compatibility);
        //    AddSynergicNiche(n2, niche1, compatibility);
        //}


        // --------- industries --------------------------
        public static void InitializeFundamentalIndustry(GameContext gameContext)
        {
            var niches = new NicheType[] {
                NicheType.Tech_SearchEngine,
                NicheType.Tech_OSDesktop,
                NicheType.Tech_Clouds,
                NicheType.Tech_Browser,
                NicheType.Tech_MobileOS,
            };
            AttachNichesToIndustry(IndustryType.Technology, niches, gameContext);

            var cloud =
                new MarketProfile(AudienceSize.SmallEnterprise, Monetisation.Enterprise, Margin.High, AppComplexity.Hard, NicheSpeed.Year);
            var searchEngine =
                new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.High, AppComplexity.Humongous, NicheSpeed.ThreeYears);
            var desktop =
                new MarketProfile(AudienceSize.Global, Monetisation.Paid, Margin.High, AppComplexity.Humongous, NicheSpeed.ThreeYears);
            var browser =
                new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.High, AppComplexity.Hard, NicheSpeed.Year);

            SetMarkets(NicheType.Tech_OSDesktop, 1980, 2040, gameContext, desktop);
            SetMarkets(NicheType.Tech_Browser, 1990, 2050, gameContext, browser);
            SetMarkets(NicheType.Tech_SearchEngine, 1995, 2040, gameContext, searchEngine);

            SetMarkets(NicheType.Tech_MobileOS, 2005, 2070, gameContext, desktop);
            SetMarkets(NicheType.Tech_Clouds, 2006, 2070, gameContext, cloud);
        }

        public static void InitializeUsefulAppsIndustry(GameContext gameContext)
        {
            var niches = new NicheType[] {
                NicheType.Qol_PdfReader, // pdf readers
                NicheType.Qol_DocumentEditors, // micr office
                NicheType.Qol_GraphicalEditor, // Photoshop
                NicheType.Qol_3DGraphicalEditor, // 3d max
                NicheType.Qol_VideoEditingTool, // average


                NicheType.Qol_AdBlocker, // small
                NicheType.Qol_DiskFormattingUtils, // small
                NicheType.Qol_RSSReader, // small
                NicheType.Qol_Archivers, // small-average

                //NicheType.Qol_MusicPlayers, // small-average
                //NicheType.Qol_VideoPlayers, // average
                NicheType.Qol_MusicSearch,
                NicheType.Qol_Maps,

                NicheType.Qol_Encyclopedia,
                //NicheType.Qol_Antivirus,

                NicheType.Qol_OnlineEducation,
                //NicheType.Qol_OnlineGenealogy,


                NicheType.ECom_OnlineTaxi,
            };
            AttachNichesToIndustry(IndustryType.WorkAndLife, niches, gameContext);

            var smallUtil =
                new MarketProfile(AudienceSize.Million, Monetisation.Adverts, Margin.Low, AppComplexity.Solo, NicheSpeed.ThreeYears);

            var encyclopedia =
                new MarketProfile(AudienceSize.Million100, Monetisation.Adverts, Margin.Mid, AppComplexity.Easy, NicheSpeed.ThreeYears);

            // paid?
            var officeDocumentEditor =
                new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Mid, AppComplexity.Average, NicheSpeed.ThreeYears);

            var education =
                new MarketProfile(AudienceSize.Million, Monetisation.Service, Margin.High, AppComplexity.Easy, NicheSpeed.Year);

            var musicSearch =
                new MarketProfile(AudienceSize.Million100, Monetisation.Adverts, Margin.Low, AppComplexity.Easy, NicheSpeed.Year);

            var maps =
                new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.Low, AppComplexity.Hard, NicheSpeed.ThreeYears);

            //var genealogySite =
            //    new MarketProfile(AudienceSize.Million, Monetisation.Adverts, Margin.Low, AppComplexity.Easy, NicheSpeed.ThreeYears);

            var taxi =
                new MarketProfile(AudienceSize.Global, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);



            SetMarkets(NicheType.Qol_DocumentEditors, 1980, 2040, gameContext, officeDocumentEditor);
            SetMarkets(NicheType.Qol_GraphicalEditor, 1990, 2040, gameContext, officeDocumentEditor);

            SetMarkets(NicheType.Qol_3DGraphicalEditor, 1990, 2050, gameContext, officeDocumentEditor);


            SetMarkets(NicheType.Qol_DiskFormattingUtils, 1999, 2030, gameContext, smallUtil);
            SetMarkets(NicheType.Qol_RSSReader, 1999, 2030, gameContext, smallUtil);
            SetMarkets(NicheType.Qol_Archivers, 1999, 2030, gameContext, smallUtil);

            SetMarkets(NicheType.Qol_PdfReader, 2000, 2030, gameContext, officeDocumentEditor);
            SetMarkets(NicheType.Qol_VideoEditingTool, 2000, 2050, gameContext, officeDocumentEditor);


            SetMarkets(NicheType.Qol_Encyclopedia, 2002, 2050, gameContext, encyclopedia);
            SetMarkets(NicheType.Qol_AdBlocker, 2003, 2040, gameContext, smallUtil);


            SetMarkets(NicheType.Qol_Maps, 2005, 2040, gameContext, maps);
            SetMarkets(NicheType.Qol_MusicSearch, 2009, 2040, gameContext, musicSearch);

            SetMarkets(NicheType.ECom_OnlineTaxi, 1998, 2030, gameContext, taxi);

            //SetMarkets(NicheType.Qol_MusicPlayers,          2003, 2040, smallUtil);
            //SetMarkets(NicheType.Qol_VideoPlayers,          2003, 2040, smallUtil);
            //SetMarkets(NicheType.Qol_Antivirus,             2003, 2040, smallUtil);
            SetMarkets(NicheType.Qol_OnlineEducation, 2008, 2040, gameContext, education);
        }

        public static void InitializeCommunicationsIndustry(GameContext gameContext)
        {
            var niches = new NicheType[] {
                NicheType.Com_Messenger,        // chatting***, info
                NicheType.Com_SocialNetwork,    // users, chatting, info
                NicheType.Com_Blogs,            // info, users, chatting
                NicheType.Com_Forums,           // info, chatting
                NicheType.Com_Email,            // chatting

                NicheType.Com_Dating,
            };
            AttachNichesToIndustry(IndustryType.Communications, niches, gameContext);


            var socialNetworks =
                new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.Mid, AppComplexity.Hard, NicheSpeed.Year);

            var messenger = socialNetworks.Copy().IncomeLow().WebService().Dynamic();

            SetMarkets(NicheType.Com_Email, 1990, 2020, gameContext, GetPopularUsefulAppProfile);
            SetMarkets(NicheType.Com_Forums, 1990, 2020, gameContext, GetPopularRarelyUsedAppProfile);
            SetMarkets(NicheType.Com_Blogs, 1995, 2020, gameContext, GetPopularRarelyUsedAppProfile);
            SetMarkets(NicheType.Com_Dating, 2000, 2020, gameContext, GetPopularUsefulAppProfile);

            SetMarkets(NicheType.Com_Messenger, 2000, 2030, gameContext, messenger);
            SetMarkets(NicheType.Com_SocialNetwork, 2003, 2025, gameContext, socialNetworks);



            //new ProductPositioning[] {
            //    //new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
            //    //new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
            //    //new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
            //    //new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
            //},
        }

        public static void InitializeEntertainmentIndustry(GameContext gameContext)
        {
            var niches = new NicheType[] {
                // gambling
                NicheType.Ent_Betting,
                NicheType.Ent_Casino,
                //NicheType.Ent_Lottery,
                NicheType.Ent_Poker,

                // gaming
                //NicheType.Ent_FreeToPlay,
                //NicheType.Ent_MMOs,
                //NicheType.Ent_Publishing,

                // Video Streaming
                NicheType.Ent_VideoHosting,
                NicheType.Ent_StreamingService,
                NicheType.Ent_TVStreamingService,
            };
            AttachNichesToIndustry(IndustryType.Entertainment, niches, gameContext);

            var videoHosting = new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.Low, AppComplexity.Hard, NicheSpeed.ThreeYears);

            var streaming = new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);
            var tvStreaming = new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Mid, AppComplexity.Average, NicheSpeed.Year);

            //SetMarkets(NicheType.Ent_Lottery, 2000, 2018, GetGamblingCompanyProfile);
            SetMarkets(NicheType.Ent_Casino, 2001, 2020, gameContext, GetGamblingCompanyProfile);
            SetMarkets(NicheType.Ent_Betting, 2000, 2020, gameContext, GetGamblingCompanyProfile);
            SetMarkets(NicheType.Ent_Poker, 2001, 2025, gameContext, GetGamblingCompanyProfile);

            //SetMarkets(NicheType.Ent_FreeToPlay, 2001, 2100,
            //    AudienceSize.Million100, Monetisation.Adverts, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);

            //SetMarkets(NicheType.Ent_MMOs, 2000, 2030,
            //    AudienceSize.Million, Monetisation.Service, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);


            SetMarkets(NicheType.Ent_VideoHosting, 2004, 2030, gameContext, videoHosting);

            SetMarkets(NicheType.Ent_StreamingService, 2011, 2030, gameContext, streaming);
            SetMarkets(NicheType.Ent_TVStreamingService, 2013, 2030, gameContext, tvStreaming);
        }

        public static void InitializeFinancesIndustry(GameContext gameContext)
        {
            var niches = new NicheType[] {
                NicheType.ECom_Exchanging,
                NicheType.ECom_OnlineBanking,
                NicheType.ECom_PaymentSystem,

                //NicheType.ECom_Blockchain,
                //NicheType.ECom_TradingBot,
            };
            AttachNichesToIndustry(IndustryType.Finances, niches, gameContext);

            var payment =
                new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.HalfYear);

            var banking = payment.Copy().Global().IncomeMid().Dynamic();

            SetMarkets(NicheType.ECom_OnlineBanking, 1992, 2030, gameContext, banking);
            SetMarkets(NicheType.ECom_PaymentSystem, 1995, 2030, gameContext, payment);

            SetMarkets(NicheType.ECom_Exchanging, 1998, 2030, gameContext, payment);
        }

        public static void InitializeTourismIndustry(GameContext gameContext)
        {
            var niches = new NicheType[] {
                NicheType.ECom_BookingTransportTickets,
                NicheType.ECom_BookingHotels,
                NicheType.ECom_BookingTours,
                NicheType.ECom_BookingAppartments,
            };
            AttachNichesToIndustry(IndustryType.Tourism, niches, gameContext);

            var booking =
                new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);

            SetMarkets(NicheType.ECom_BookingTransportTickets, 1998, 2030, gameContext, booking);
            SetMarkets(NicheType.ECom_BookingHotels, 1998, 2030, gameContext, booking);
            SetMarkets(NicheType.ECom_BookingTours, 1998, 2030, gameContext, booking);
            SetMarkets(NicheType.ECom_BookingAppartments, 2008, 2050, gameContext, booking);
        }

        public static void InitializeEcommerceIndustry(GameContext gameContext)
        {
            var niches = new NicheType[] {
                NicheType.ECom_Marketplace,
                NicheType.ECom_OnlineTaxi,
            };
            AttachNichesToIndustry(IndustryType.Ecommerce, niches, gameContext);

            var marketplace =
                new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.High, AppComplexity.Hard, NicheSpeed.Year);

            var taxi =
                new MarketProfile(AudienceSize.Global, Monetisation.Service, Margin.Low, AppComplexity.Hard, NicheSpeed.Year);

            SetMarkets(NicheType.ECom_Marketplace, 1995, 2050, gameContext, marketplace);
            SetMarkets(NicheType.ECom_OnlineTaxi, 1995, 2050, gameContext, taxi);
        }


        // Full Audience
        public static long GetFullAudience(MarketProfile profile, int nicheId)
        {
            AudienceSize audienceSize = profile.AudienceSize;

            return Randomise((long)audienceSize, nicheId);
        }

        public static int GetTechCost(MarketProfile profile, int nicheId)
        {
            return (int)Randomise((int)profile.AppComplexity, nicheId);
        }


        public static float GetProductPrice(MarketProfile profile, float adCost, int nicheId)
        {
            Monetisation monetisationType = profile.MonetisationType;
            Margin margin = profile.Margin;

            var baseCost = adCost * (100 + (int)margin);

            return baseCost / 100f;
        }


        public static float GetAdCost(MarketProfile profile, int nicheId)
        {
            Monetisation monetisationType = profile.MonetisationType;

            var baseValue = (int)monetisationType;

            //var repaymentTime = GetSelfPaymentTime(monetisationType);
            //baseValue *= repaymentTime;

            return Randomise(baseValue * 1000, nicheId) / 12f / 1000f;
        }

        public static float GetSelfPaymentTime(Monetisation monetisationType)
        {
            switch (monetisationType)
            {
                case Monetisation.Adverts: return 10;
                case Monetisation.Service: return 8;
                case Monetisation.Enterprise: return 5;
                case Monetisation.Paid: return 3;
            }

            return 1;
        }

        public static int GetYear(int year) => ScheduleUtils.GetDateByYear(year);

        public static GameEntity SetMarkets(
            NicheType nicheType,
            int startDate,
            int duration,
            GameContext gameContext,
            AudienceSize AudienceSize, Monetisation MonetisationType, Margin Margin, NicheSpeed Iteration, AppComplexity ProductComplexity
        )
        {
            var profile = new MarketProfile
            {
                AudienceSize = AudienceSize,
                NicheSpeed = Iteration,
                Margin = Margin,
                MonetisationType = MonetisationType,
                AppComplexity = ProductComplexity
            };

            return SetMarkets(nicheType, startDate, duration, gameContext, profile);
        }

        public static GameEntity SetMarkets(NicheType nicheType,
            int startDate,
            int duration,
            GameContext gameContext,
            MarketProfile settings
            )
        {
            var nicheId = GetNicheId(nicheType);

            var clients = GetFullAudience(settings, nicheId);
            var techCost = GetTechCost(settings, nicheId);
            var adCosts = GetAdCost(settings, nicheId);
            var price = GetProductPrice(settings, adCosts, nicheId);


            var n = SetNicheCosts(nicheType, price, clients, techCost, adCosts, gameContext);


            var positionings = new Dictionary<int, ProductPositioning>
            {
                [0] = new ProductPositioning
                {
                    isCompetitive = false,
                    marketShare = 100,
                    name = Enums.GetSingleFormattedNicheName(nicheType)
                }
            };

            var clientsContainer = new Dictionary<int, long>
            {
                [0] = clients
            };

            n.ReplaceNicheSegments(positionings);
            n.ReplaceNicheClientsContainer(clientsContainer);
            n.ReplaceNicheLifecycle(GetYear(startDate), n.nicheLifecycle.Growth, GetYear(duration));
            n.ReplaceNicheBaseProfile(settings);

            return n;
        }


        public static long Randomise(long baseValue, int nicheId)
        {
            return Companies.GetRandomValue(baseValue, nicheId, 0);
        }

        public static int GetNicheId(NicheType nicheType)
        {
            return (int)nicheType;
        }

        // profiles

        public static MarketProfile GetPopularRarelyUsedAppProfile => new MarketProfile
        {
            AudienceSize = AudienceSize.Million100,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.Low,

            AppComplexity = AppComplexity.Average,

            NicheSpeed = NicheSpeed.ThreeYears,
        };

        public static MarketProfile GetPopularUsefulAppProfile => new MarketProfile
        {
            AudienceSize = AudienceSize.Million100,
            MonetisationType = Monetisation.Adverts,
            Margin = Margin.Low,

            AppComplexity = AppComplexity.Hard,

            NicheSpeed = NicheSpeed.Year,
        };

        public static MarketProfile GetGamblingCompanyProfile => new MarketProfile
        {
            AudienceSize = AudienceSize.Million,
            MonetisationType = Monetisation.IrregularPaid,
            Margin = Margin.Mid,

            AppComplexity = AppComplexity.Average,

            NicheSpeed = NicheSpeed.Year,
        };

        public static MarketProfile GetSmallUtilityForOneDevProfile => new MarketProfile(AudienceSize.SmallUtil, Monetisation.Adverts, Margin.Low, AppComplexity.Solo, NicheSpeed.ThreeYears);
    }
}
