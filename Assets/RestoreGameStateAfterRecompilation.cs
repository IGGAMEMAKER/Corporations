using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RestoreGameStateAfterRecompilation : View
{
    public GameObject Panels;
    public GameObject TopPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11) || Input.GetKeyDown(KeyCode.Tab))
        {
            State.SaveGame(Q);
            Debug.Log("Game Saved");
        }

        if (Input.GetKeyDown(KeyCode.F10) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            LoadPreviousState();
        }
    }

    void Start()
    {
        bool wasForcedToShutDownByRecompilation = false;

        if (wasForcedToShutDownByRecompilation)
        {
            // restore state automatically
            LoadPreviousState();
        }
        else
        {
            GenerateNewWorld();
        }
    }

    void LoadPreviousState()
    {
        State.LoadGame(Q);
        Debug.Log("Game Loaded");

        // reactivate panels here
        TopPanel.SetActive(false);
        TopPanel.SetActive(true);

        Panels.SetActive(false);
        Panels.SetActive(true);
    }

    void GenerateNewWorld()
    {
        // tutorial
        var e = Q.CreateEntity();

        e.AddTutorial(new Dictionary<TutorialFunctionality, bool>());
        e.AddEventContainer(new Dictionary<string, bool>());

        // date
        var DateEntity = Q.CreateEntity();
        DateEntity.AddDate(0, 3);
        DateEntity.AddSpeed(3);
        DateEntity.AddTargetDate(0);

        ScheduleUtils.PauseGame(Q);

        // menu
        var c = ScreenUtils.GetMenu(Q);

        // Notifications and Popups
        var n = Q.CreateEntity();

        n.AddNotifications(new List<NotificationMessage>());
        n.AddPopup(new List<PopupMessage>());

        // reports
        var r = Q.CreateEntity();

        r.AddReports(new List<AnnualReport>());

        // campaign stats
        var statsContainer = Q.CreateEntity();

        statsContainer.AddCampaignStats(new Dictionary<CampaignStat, int>
        {
            [CampaignStat.Acquisitions] = 0,
            [CampaignStat.Bankruptcies] = 0,
            [CampaignStat.PromotedCompanies] = 0,
            [CampaignStat.SpawnedFunds] = 0
        });

        SpawnMarkets();

        SpawnProducts();

        SpawnCompanies();
    }

    void SpawnCompanies()
    {
        Debug.Log("Simulate Development");
        var skipDays = (Balance.START_YEAR - 1991) * 360;

        MockySimulation(skipDays, Q);

        Debug.Log("Simulation done");
    }


    void MockySimulation(int skipDays, GameContext GameContext)
    {
        var markets = Markets.GetNiches(GameContext);
        var skipMonths = skipDays / 30;

        var date = ScheduleUtils.GetCurrentDate(GameContext);
        List<NicheType> activatedMarkets = new List<NicheType>();

        // simulate market development
        foreach (var m in markets)
        {
            var spawnTime = m.nicheLifecycle.OpenDate;

            var monthsOfWork = (date - spawnTime) / 30;
            var accumulator = monthsOfWork;

            // niche state promotion
            var profile = m.nicheBaseProfile.Profile;

            while (accumulator > 0)
            {
                //Debug.Log("while");
                Markets.PromoteNicheState(m);
                accumulator -= Markets.GetNicheDuration(m);
            }



            // filling with companies
            var amountOfCompanies = UnityEngine.Random.Range(0, monthsOfWork / 12);

            for (var i = 0; i < amountOfCompanies; i++)
                Markets.FillMarket(m, GameContext);

            var costs = m.nicheCosts;
            var isMockup = costs.AcquisitionCost == 1 && costs.Audience == 1 && costs.BaseIncome == 1 && costs.TechCost == 1;
            if (!isMockup)
                activatedMarkets.Add(m.niche.NicheType);
        }
        Debug.Log("Mocky simulation: simulated markets");

        // simulate products

        var products = Companies.GetProductCompanies(GameContext);
        foreach (var p in products)
        {
            var niche = Markets.GetNiche(GameContext, p.product.Niche);
            var spawnTime = niche.nicheLifecycle.OpenDate;

            var monthsOfWork = (date - spawnTime) / 30;
            //Debug.Log($"Market={p.product.Niche}  Date: " + date + " openDate " + spawnTime + "  monthsOfWork = " + monthsOfWork);

            if (monthsOfWork < 0)
                monthsOfWork = 0;

            // set concepts
            var iterationTime = Products.GetBaseIterationTime(niche);

            var concept = monthsOfWork * 30 / iterationTime;
            var randConcept = 1 + UnityEngine.Random.Range(0, concept);

            //for (var i = 0; i < randConcept; i++)
            //    Products.UpdgradeProduct(p, GameContext, true);

            // set brands
            // commented, because UpgradeProduct already adds brand powers
            //var brand = Mathf.Clamp(UnityEngine.Random.Range(0, monthsOfWork / 2), 0, 35);
            //MarketingUtils.AddBrandPower(p, brand);

            // set clients
            //var flow = (float) MarketingUtils.GetClientFlow(GameContext, p.product.Niche);
            ////var clients = monthsOfWork * flow * UnityEngine.Random.Range(0.5f, 1.5f);
            //var growth = 1.03f;
            //var clients = MarketingUtils.GetClients(p) * Mathf.Pow(growth, monthsOfWork);

            //MarketingUtils.AddClients(p, (long)clients);
        }

        Debug.Log("Mocky simulation: simulated product");

        //PrintMarketData(markets, activatedMarkets);
    }

    void PrintMarketData(GameEntity[] markets, List<NicheType> activatedMarkets)
    {
        Dictionary<int, int> years = new Dictionary<int, int>();
        foreach (var m in markets)
        {
            var openDate = ScheduleUtils.GetYearOf(m.nicheLifecycle.OpenDate);

            if (years.ContainsKey(openDate))
                years[openDate]++;
            else
                years[openDate] = 1;
        }

        var pre2000markets = 0;
        var post2000markets = 0;

        foreach (var m in years.OrderBy(p => p.Key))
        {
            var year = m.Key;
            var amount = m.Value;

            if (year < 2000)
                pre2000markets += amount;
            else
                post2000markets += amount;

            //Debug.Log($"Year {m.Key}: {m.Value} markets");
        }

        Debug.Log("Pre 2000 markets: " + pre2000markets);
        Debug.Log("Post 2000 markets: " + post2000markets);

        List<NicheType> list = new List<NicheType>();
        List<NicheType> notActivated = new List<NicheType>();


        foreach (NicheType n in (NicheType[])System.Enum.GetValues(typeof(NicheType)))
        {
            if (Enums.GetFormattedNicheName(n) == n.ToString())
                list.Add(n);

            if (!activatedMarkets.Contains(n))
                notActivated.Add(n);
        }

        if (list.Count != 0)
            Debug.Log(list.Count + " markets need to be described: " + string.Join(",", list));

        if (notActivated.Count != 0)
            Debug.Log(notActivated.Count + " markets need to be activated: " + string.Join(",", notActivated));
    }

    void SpawnProducts()
    {
        // products
        var facebook = GenerateProductCompany("facebook", NicheType.Com_SocialNetwork);
        var twitter = GenerateProductCompany("twitter", NicheType.Com_SocialNetwork);
        var vk = GenerateProductCompany("vk", NicheType.Com_SocialNetwork);

        var tg = GenerateProductCompany("telegram", NicheType.Com_Messenger);
        GenerateProductCompany("whatsapp", NicheType.Com_Messenger);
        var fbMessenger = GenerateProductCompany("facebook messenger", NicheType.Com_Messenger);

        int google = GenerateProductCompany("Google", NicheType.Tech_SearchEngine).company.Id;
        int yahoo = GenerateProductCompany("Yahoo", NicheType.Tech_SearchEngine).company.Id;
        GenerateProductCompany("Yandex", NicheType.Tech_SearchEngine);

        var microsoftOs = GenerateProductCompany("Windows", NicheType.Tech_OSDesktop);


        // investors
        int investorId1 = GenerateInvestmentFund("Morgan Stanley", 1000000);
        int investorId2 = GenerateInvestmentFund("Goldman Sachs", 2000000);
        int investorId3 = GenerateInvestmentFund("Morgan J.P.", 3000000);

        int alphabet = GenerateHoldingCompany("Alphabet");
        AttachToHolding(alphabet, google);

        AddShareholder(alphabet, investorId1, 100);
        AddShareholder(alphabet, investorId2, 200);

        int googleGroupId = PromoteToGroup(google);

        var facebookInc = GenerateHoldingCompany("Facebook Inc");
        AttachToHolding(facebookInc, facebook);
        AttachToHolding(facebookInc, fbMessenger);

        var microsoft = GenerateHoldingCompany("Microsoft Inc");
        AttachToHolding(microsoft, microsoftOs);

        var mailru = GenerateHoldingCompany("MailRu");
        AttachToHolding(mailru, vk);




        AddShareholder(yahoo, investorId2, 500);
        AddShareholder(yahoo, investorId3, 1500);
        AddShareholder(yahoo, investorId1, 100);

        // -------------------------------------------

        SpawnInvestmentFunds(8, 10000000, 100000000);
        SpawnInvestors(50, 1000000, 10000000);

        AutoFillNonFilledShareholders();
        AutoFillProposals();

        SetSpheresOfInfluence();
    }

    void Print(string s)
    {
        Debug.Log("INI: " + s);
    }

    void PlayAs(string companyName) => PlayAs(Companies.GetCompanyByName(Q, companyName));
    void PlayAs(int companyId) => PlayAs(Companies.Get(Q, companyId));
    void PlayAs(GameEntity company) => Companies.PlayAs(company, Q);


    void SetSpheresOfInfluence()
    {
        var financial = Companies.GetInvestmentFunds(Q);
        //var managing = CompanyUtils.GetGroupCompanies(GameContext);

        foreach (var c in financial)
        {
            Companies.AddFocusIndustry(GetRandomIndustry(), c);

            AutoFillFocusNichesByIndustry(c);
        }

        //foreach (var c in managing)
        //{
        //    CompanyUtils.AddFocusIndustry(GetRandomIndustry(), c);

        //    AutoFillSomeFocusNichesByIndustry(c);
        //    //AutoFillFocusNichesByIndustry(c);
        //}
    }

    void AutoFillFocusNichesByIndustry(GameEntity company)
    {
        var niches = Markets.GetNichesInIndustry(company.companyFocus.Industries[0], Q);

        foreach (var n in niches)
            Companies.AddFocusNiche(n.niche.NicheType, company, Q);
    }

    void AutoFillSomeFocusNichesByIndustry(GameEntity company)
    {
        var niches = Markets.GetNichesInIndustry(company.companyFocus.Industries[0], Q);

        //CompanyUtils.AddFocusNiche(RandomEnum<NicheComponent>.PickRandomItem(niches).NicheType, company);

        foreach (var n in niches)
            Companies.AddFocusNiche(n.niche.NicheType, company, Q);
    }

    IndustryType GetRandomIndustry()
    {
        return RandomEnum<IndustryType>.GenerateValue();
    }

    GameEntity GenerateProductCompany(string name, NicheType nicheType)
    {
        var product = Companies.GenerateProductCompany(Q, name, nicheType);

        Companies.SetStartCapital(product, Q);

        return product;
    }

    int GenerateInvestmentFund(string name, long money)
    {
        return Companies.GenerateInvestmentFund(Q, name, money).shareholder.Id;
    }

    int GenerateHoldingCompany(string name)
    {
        return Companies.GenerateHoldingCompany(Q, name).company.Id;
    }

    void AttachToHolding(int parent, GameEntity child) => AttachToHolding(parent, child.company.Id);
    void AttachToHolding(int parent, int child)
    {
        Companies.AttachToGroup(Q, parent, child);

        var c = Companies.Get(Q, child);
        var p = Companies.Get(Q, parent);

        if (c.hasProduct)
            Companies.AddFocusNiche(c.product.Niche, p, Q);
    }


    void AddShareholder(int companyId, int investorId, int shares)
    {
        //Debug.Log($"Add Shareholder {investorId} with {shares} shares to {companyId}");

        Companies.AddShareholder(Q, companyId, investorId, shares);
    }

    int PromoteToGroup(int companyId)
    {
        return Companies.PromoteProductCompanyToGroup(Q, companyId);
    }

    long GetRandomFundSize(int min, int max)
    {
        int value = UnityEngine.Random.Range(min, max);

        return System.Convert.ToInt64(value);
    }

    void SpawnInvestmentFunds(int amountOfFunds, int investmentMin, int investmentMax)
    {
        for (var i = 0; i < amountOfFunds; i++)
            GenerateInvestmentFund(RandomUtils.GenerateInvestmentCompanyName(), GetRandomFundSize(investmentMin, investmentMax));
    }

    void SpawnInvestors(int amountOfInvestors, int investmentMin, int investmentMax)
    {
        for (var i = 0; i < amountOfInvestors; i++)
            Investments.GenerateAngel(Q);
    }

    int GetRandomInvestmentFund()
    {
        return Investments.GetRandomInvestmentFund(Q);
    }

    int GetRandomInvestorId()
    {
        return GetRandomInvestmentFund();
    }

    private void AutoFillProposals()
    {
        foreach (var c in Companies.GetNonFinancialCompanies(Q))
            Companies.SpawnProposals(Q, c.company.Id);
    }

    void AutoFillNonFilledShareholders()
    {
        Companies.AutoFillNonFilledShareholders(Q, false);
    }

    /// -------------------------------------------

    void SpawnMarkets()
    {
        InitializeIndustries();

        InitializeFundamentalIndustry();
        InitializeCommunicationsIndustry();
        InitializeEntertainmentIndustry();

        InitializeEcommerceIndustry();
        InitializeFinancesIndustry();
        InitializeTourismIndustry();

        InitializeUsefulAppsIndustry();

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

    void InitializeIndustries()
    {
        foreach (IndustryType industry in (IndustryType[])System.Enum.GetValues(typeof(IndustryType)))
            Q.CreateEntity().AddIndustry(industry);
    }

    GameEntity SetNicheCosts(NicheType niche, float newBasePrice, long newClientBatch, int newTechCost, float newAdCost) => SetNicheCosts(GetNiche(niche), newBasePrice, newClientBatch, newTechCost, newAdCost);
    GameEntity SetNicheCosts(GameEntity e, float newBasePrice, long newClientBatch, int newTechCost, float newAdCost)
    {
        e.ReplaceNicheCosts(newBasePrice, newClientBatch, newTechCost, newAdCost);

        return e;
    }


    GameEntity GetNiche(NicheType nicheType) => Markets.GetNiche(Q, nicheType);

    GameEntity AttachNicheToIndustry(NicheType niche, IndustryType industry)
    {
        var e = GetNiche(niche);

        e.ReplaceNiche(
            e.niche.NicheType,
            industry,
            e.niche.MarketCompatibilities,
            e.niche.CompetingNiches,
            e.niche.Parent
            );



        return e;
    }

    void AttachNichesToIndustry(IndustryType industry, NicheType[] nicheTypes)
    {
        foreach (var n in nicheTypes)
            AttachNicheToIndustry(n, industry);
    }


    GameEntity ForkNiche(NicheType parent, NicheType child)
    {
        var e = GetNiche(child);

        e.ReplaceNiche(
            e.niche.NicheType,
            e.niche.IndustryType,
            e.niche.MarketCompatibilities,
            e.niche.CompetingNiches,
            parent
            );

        return e;
    }

    void AddSynergicNiche(GameEntity entity, NicheType niche, int compatibility)
    {
        var list = entity.niche.MarketCompatibilities;
        var n = entity.niche;

        list.Add(new MarketCompatibility { Compatibility = compatibility, NicheType = niche });

        entity.ReplaceNiche(n.NicheType, n.IndustryType, list, n.CompetingNiches, n.Parent);
    }

    void SetNichesAsSynergic(NicheType niche1, NicheType niche2, int compatibility)
    {
        var n1 = GetNiche(niche1);
        var n2 = GetNiche(niche2);

        AddSynergicNiche(n1, niche2, compatibility);
        AddSynergicNiche(n2, niche1, compatibility);
    }


    // --------- industries --------------------------
    private void InitializeFundamentalIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Tech_SearchEngine,
            NicheType.Tech_OSDesktop,
            NicheType.Tech_Clouds,
            NicheType.Tech_Browser,
            NicheType.Tech_MobileOS,
        };
        AttachNichesToIndustry(IndustryType.Technology, niches);

        var cloud =
            new MarketProfile(AudienceSize.SmallEnterprise, Monetisation.Enterprise, Margin.High, AppComplexity.Hard, NicheSpeed.Year);
        var searchEngine =
            new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.High, AppComplexity.Humongous, NicheSpeed.ThreeYears);
        var desktop =
            new MarketProfile(AudienceSize.Global, Monetisation.Paid, Margin.High, AppComplexity.Humongous, NicheSpeed.ThreeYears);
        var browser =
            new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.High, AppComplexity.Hard, NicheSpeed.Year);

        SetMarkets(NicheType.Tech_OSDesktop, 1980, 2040, desktop);
        SetMarkets(NicheType.Tech_Browser, 1990, 2050, browser);
        SetMarkets(NicheType.Tech_SearchEngine, 1995, 2040, searchEngine);

        SetMarkets(NicheType.Tech_MobileOS, 2005, 2070, desktop);
        SetMarkets(NicheType.Tech_Clouds, 2006, 2050, cloud);
    }

    private void InitializeUsefulAppsIndustry()
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
        AttachNichesToIndustry(IndustryType.WorkAndLife, niches);

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



        SetMarkets(NicheType.Qol_DocumentEditors, 1980, 2040, officeDocumentEditor);
        SetMarkets(NicheType.Qol_GraphicalEditor, 1990, 2040, officeDocumentEditor);

        SetMarkets(NicheType.Qol_3DGraphicalEditor, 1990, 2050, officeDocumentEditor);


        SetMarkets(NicheType.Qol_DiskFormattingUtils, 1999, 2030, smallUtil);
        SetMarkets(NicheType.Qol_RSSReader, 1999, 2030, smallUtil);
        SetMarkets(NicheType.Qol_Archivers, 1999, 2030, smallUtil);

        SetMarkets(NicheType.Qol_PdfReader, 2000, 2030, officeDocumentEditor);
        SetMarkets(NicheType.Qol_VideoEditingTool, 2000, 2050, officeDocumentEditor);


        SetMarkets(NicheType.Qol_Encyclopedia, 2002, 2050, encyclopedia);
        SetMarkets(NicheType.Qol_AdBlocker, 2003, 2040, smallUtil);
        //SetMarkets(NicheType.Qol_OnlineGenealogy,       2003, 2040, genealogySite);


        SetMarkets(NicheType.Qol_Maps, 2005, 2040, maps);
        SetMarkets(NicheType.Qol_MusicSearch, 2009, 2040, musicSearch);

        SetMarkets(NicheType.ECom_OnlineTaxi, 1998, 2030, taxi);

        //SetMarkets(NicheType.Qol_MusicPlayers,          2003, 2040, smallUtil);
        //SetMarkets(NicheType.Qol_VideoPlayers,          2003, 2040, smallUtil);
        //SetMarkets(NicheType.Qol_Antivirus,             2003, 2040, smallUtil);
        SetMarkets(NicheType.Qol_OnlineEducation, 2008, 2040, education);
    }

    void InitializeCommunicationsIndustry()
    {
        var niches = new NicheType[] {
            NicheType.Com_Messenger,        // chatting***, info
            NicheType.Com_SocialNetwork,    // users, chatting, info
            NicheType.Com_Blogs,            // info, users, chatting
            NicheType.Com_Forums,           // info, chatting
            NicheType.Com_Email,            // chatting

            NicheType.Com_Dating,
        };
        AttachNichesToIndustry(IndustryType.Communications, niches);


        var socialNetworks =
            new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.Mid, AppComplexity.Hard, NicheSpeed.Year);

        var messenger = socialNetworks.Copy().IncomeLow().WebService().Dynamic();

        SetMarkets(NicheType.Com_Email, 1990, 2020, GetPopularUsefulAppProfile);
        SetMarkets(NicheType.Com_Forums, 1990, 2020, GetPopularRarelyUsedAppProfile);
        SetMarkets(NicheType.Com_Blogs, 1995, 2020, GetPopularRarelyUsedAppProfile);
        SetMarkets(NicheType.Com_Dating, 2000, 2020, GetPopularUsefulAppProfile);

        SetMarkets(NicheType.Com_Messenger, 2000, 2030, messenger);
        SetMarkets(NicheType.Com_SocialNetwork, 2003, 2025, socialNetworks);



        //new ProductPositioning[] {
        //    //new ProductPositioning { name = "Basic social network", marketShare = 100 }, // fb
        //    //new ProductPositioning { name = "Corporative social network", marketShare = 3, priceModifier = 10 }, // linkedIn
        //    //new ProductPositioning { name = "Text focused social network", marketShare = 15, priceModifier = 1.75f }, // twitter
        //    //new ProductPositioning { name = "Image focused social network", marketShare = 85 }, // insta
        //},
    }

    private void InitializeEntertainmentIndustry()
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
        AttachNichesToIndustry(IndustryType.Entertainment, niches);

        var videoHosting = new MarketProfile(AudienceSize.Global, Monetisation.Adverts, Margin.Low, AppComplexity.Hard, NicheSpeed.ThreeYears);

        var streaming = new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);
        var tvStreaming = new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Mid, AppComplexity.Average, NicheSpeed.Year);

        //SetMarkets(NicheType.Ent_Lottery, 2000, 2018, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Casino, 2001, 2020, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Betting, 2000, 2020, GetGamblingCompanyProfile);
        SetMarkets(NicheType.Ent_Poker, 2001, 2025, GetGamblingCompanyProfile);

        //SetMarkets(NicheType.Ent_FreeToPlay, 2001, 2100,
        //    AudienceSize.Million100, Monetisation.Adverts, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);

        //SetMarkets(NicheType.Ent_MMOs, 2000, 2030,
        //    AudienceSize.Million, Monetisation.Service, Margin.Mid, NicheSpeed.Year, AppComplexity.Average);


        SetMarkets(NicheType.Ent_VideoHosting, 2004, 2030, videoHosting);

        SetMarkets(NicheType.Ent_StreamingService, 2011, 2030, streaming);
        SetMarkets(NicheType.Ent_TVStreamingService, 2013, 2030, tvStreaming);
    }

    private void InitializeFinancesIndustry()
    {
        var niches = new NicheType[] {
            NicheType.ECom_Exchanging,
            NicheType.ECom_OnlineBanking,
            NicheType.ECom_PaymentSystem,

            //NicheType.ECom_Blockchain,
            //NicheType.ECom_TradingBot,
        };
        AttachNichesToIndustry(IndustryType.Finances, niches);

        var payment =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.HalfYear);

        var banking = payment.Copy().Global().IncomeMid().Dynamic();

        SetMarkets(NicheType.ECom_OnlineBanking, 1992, 2030, banking);
        SetMarkets(NicheType.ECom_PaymentSystem, 1995, 2030, payment);

        SetMarkets(NicheType.ECom_Exchanging, 1998, 2030, payment);
    }

    private void InitializeTourismIndustry()
    {
        var niches = new NicheType[] {
            NicheType.ECom_BookingTransportTickets,
            NicheType.ECom_BookingHotels,
            NicheType.ECom_BookingTours,
            NicheType.ECom_BookingAppartments,
        };
        AttachNichesToIndustry(IndustryType.Tourism, niches);

        var booking =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.Low, AppComplexity.Average, NicheSpeed.Year);

        SetMarkets(NicheType.ECom_BookingTransportTickets, 1998, 2030, booking);
        SetMarkets(NicheType.ECom_BookingHotels, 1998, 2030, booking);
        SetMarkets(NicheType.ECom_BookingTours, 1998, 2030, booking);
        SetMarkets(NicheType.ECom_BookingAppartments, 2008, 2050, booking);
    }

    private void InitializeEcommerceIndustry()
    {
        var niches = new NicheType[] {
            NicheType.ECom_Marketplace,
            NicheType.ECom_OnlineTaxi,
        };
        AttachNichesToIndustry(IndustryType.Ecommerce, niches);

        var marketplace =
            new MarketProfile(AudienceSize.Million100, Monetisation.Service, Margin.High, AppComplexity.Hard, NicheSpeed.Year);

        var taxi =
            new MarketProfile(AudienceSize.Global, Monetisation.Service, Margin.Low, AppComplexity.Hard, NicheSpeed.Year);

        SetMarkets(NicheType.ECom_Marketplace, 1995, 2050, marketplace);
        SetMarkets(NicheType.ECom_OnlineTaxi, 1995, 2050, taxi);
    }


    // Full Audience
    long GetFullAudience(MarketProfile profile, int nicheId)
    {
        AudienceSize audienceSize = profile.AudienceSize;

        return Randomise((long)audienceSize, nicheId);
    }

    private int GetTechCost(MarketProfile profile, int nicheId)
    {
        return (int)Randomise((int)profile.AppComplexity, nicheId);
    }


    float GetProductPrice(MarketProfile profile, float adCost, int nicheId)
    {
        Monetisation monetisationType = profile.MonetisationType;
        Margin margin = profile.Margin;

        var baseCost = adCost * (100 + (int)margin);

        return baseCost / 100f;
    }


    float GetAdCost(MarketProfile profile, int nicheId)
    {
        Monetisation monetisationType = profile.MonetisationType;

        var baseValue = (int)monetisationType;

        //var repaymentTime = GetSelfPaymentTime(monetisationType);
        //baseValue *= repaymentTime;

        return Randomise(baseValue * 1000, nicheId) / 12f / 1000f;
    }

    float GetSelfPaymentTime(Monetisation monetisationType)
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

    int GetYear(int year) => ScheduleUtils.GetDateByYear(year);

    GameEntity SetMarkets(NicheType nicheType,
    int startDate,
    int duration,
    AudienceSize AudienceSize, Monetisation MonetisationType, Margin Margin, NicheSpeed Iteration, AppComplexity ProductComplexity
    )
    {
        return SetMarkets(
            nicheType,
            startDate,
            duration,
            new MarketProfile
            {
                AudienceSize = AudienceSize,
                NicheSpeed = Iteration,
                Margin = Margin,
                MonetisationType = MonetisationType,
                AppComplexity = ProductComplexity
            }
            );
    }

    GameEntity SetMarkets(NicheType nicheType,
        int startDate,
        int duration,
        MarketProfile settings
        )
    {
        var nicheId = GetNicheId(nicheType);

        var clients = GetFullAudience(settings, nicheId);
        var techCost = GetTechCost(settings, nicheId);
        var adCosts = GetAdCost(settings, nicheId);
        var price = GetProductPrice(settings, adCosts, nicheId);


        var n = SetNicheCosts(nicheType, price, clients, techCost, adCosts);


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


    long Randomise(long baseValue, int nicheId)
    {
        return Companies.GetRandomValue(baseValue, nicheId, 0);
    }

    int GetNicheId(NicheType nicheType)
    {
        return (int)nicheType;
    }

    // profiles

    MarketProfile GetPopularRarelyUsedAppProfile => new MarketProfile
    {
        AudienceSize = AudienceSize.Million100,
        MonetisationType = Monetisation.Adverts,
        Margin = Margin.Low,

        AppComplexity = AppComplexity.Average,

        NicheSpeed = NicheSpeed.ThreeYears,
    };

    MarketProfile GetPopularUsefulAppProfile => new MarketProfile
    {
        AudienceSize = AudienceSize.Million100,
        MonetisationType = Monetisation.Adverts,
        Margin = Margin.Low,

        AppComplexity = AppComplexity.Hard,

        NicheSpeed = NicheSpeed.Year,
    };

    MarketProfile GetGamblingCompanyProfile => new MarketProfile
    {
        AudienceSize = AudienceSize.Million,
        MonetisationType = Monetisation.IrregularPaid,
        Margin = Margin.Mid,

        AppComplexity = AppComplexity.Average,

        NicheSpeed = NicheSpeed.Year,
    };

    MarketProfile GetSmallUtilityForOneDevProfile => new MarketProfile(AudienceSize.SmallUtil, Monetisation.Adverts, Margin.Low, AppComplexity.Solo, NicheSpeed.ThreeYears);

    /// --- Products -----------------------------------
}
