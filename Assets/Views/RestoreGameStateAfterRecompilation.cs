using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RestoreGameStateAfterRecompilation : View
{
    public GameObject Panels;
    public GameObject TopPanel;

    public ShowNewCampaignOnlyIfInProductionMode ShowNewCampaignOnlyIfInProductionMode;

    //void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.F11) || Input.GetKeyDown(KeyCode.Tab))
    //    //{
    //    //    State.SaveGame(Q);
    //    //    Debug.Log("Game Saved");
    //    //}

    //    //if (Input.GetKeyDown(KeyCode.F10) || Input.GetKeyDown(KeyCode.LeftControl))
    //    //{
    //    //    LoadPreviousState();
    //    //}
    //}

    void Start()
    {
        bool wasForcedToShutDownByRecompilation = true;

        //GenerateNewWorld();

        if (wasForcedToShutDownByRecompilation)
        {
            // restore state automatically
            //LoadPreviousState();
            ShowNewCampaignOnlyIfInProductionMode.ContinueGame();
        }
    }

    void LoadPreviousState()
    {
        State.LoadGameData(Q);
        Debug.Log("Game Loaded");

        if (TopPanel == null && Panels == null)
            return;
        
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
        var popups = Q.CreateEntity();

        e.AddTutorial(new Dictionary<TutorialFunctionality, bool>());
        e.AddEventContainer(new Dictionary<string, bool>());

        // date

        e.AddSimpleCooldownContainer(new Dictionary<string, SimpleCooldown>());

        e.AddDate(0);
        e.AddSpeed(2);
        e.AddProfiling(0, new StringBuilder(), new Dictionary<string, long>());

        //e.AddTargetDate(0);

        // game is paused already
        //ScheduleUtils.PauseGame(Q);

        e.AddGameEventContainer(new List<GameEvent>());

        // menu
        ScreenUtils.CreateMenu(e);
        //var c = ScreenUtils.GetMenu(Q);

        // Notifications and Popups
        e.AddNotifications(new List<NotificationMessage>());
        e.AddPopup(new List<PopupMessage>());
        e.AddSeenPopups(new List<PopupType>());

        // reports & stats
        e.AddReports(new List<AnnualReport>());
        e.AddCampaignStats(new Dictionary<CampaignStat, int>
        {
            [CampaignStat.Acquisitions] = 0,
            [CampaignStat.Bankruptcies] = 0,
            [CampaignStat.PromotedCompanies] = 0,
            [CampaignStat.SpawnedFunds] = 0
        });

        SpawnMarkets();

        SpawnHistoricalCompanies();

        SpawnCompanies();
    }

    void SpawnCompanies()
    {
        Debug.Log("Simulate Development");

        var skipDays = (C.START_YEAR - 1991) * 360;

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

            while (accumulator > 0)
            {
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
            var niche = Markets.Get(GameContext, p.product.Niche);
            var spawnTime = niche.nicheLifecycle.OpenDate;

            var monthsOfWork = (date - spawnTime) / 30;
            //Debug.Log($"Market={p.product.Niche}  Date: " + date + " openDate " + spawnTime + "  monthsOfWork = " + monthsOfWork);

            if (monthsOfWork < 0)
                monthsOfWork = 0;
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

    void SpawnHistoricalCompanies()
    {
        // products
        var facebook = GenerateProductCompany("facebook", NicheType.Com_SocialNetwork);
        var twitter = GenerateProductCompany("twitter", NicheType.Com_SocialNetwork);
        var vk = GenerateProductCompany("vk", NicheType.Com_SocialNetwork);

        var tg = GenerateProductCompany("telegram", NicheType.Com_Messenger);
        GenerateProductCompany("whatsapp", NicheType.Com_Messenger);
        var fbMessenger = GenerateProductCompany("facebook messenger", NicheType.Com_Messenger);

        var google = GenerateProductCompany("Google", NicheType.Tech_SearchEngine);
        var yahoo = GenerateProductCompany("Yahoo", NicheType.Tech_SearchEngine);
        GenerateProductCompany("Yandex", NicheType.Tech_SearchEngine);

        var microsoftOs = GenerateProductCompany("Windows", NicheType.Tech_OSDesktop);


        // investors
        var investorId1 = GenerateInvestmentFund("Morgan Stanley", 1000000);
        var investorId2 = GenerateInvestmentFund("Goldman Sachs", 2000000);
        var investorId3 = GenerateInvestmentFund("Morgan J.P.", 3000000);

        var alphabet = GenerateHoldingCompany("Alphabet");
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
            Companies.AddFocusNiche(company, n.niche.NicheType, Q);
    }

    void AutoFillSomeFocusNichesByIndustry(GameEntity company)
    {
        var niches = Markets.GetNichesInIndustry(company.companyFocus.Industries[0], Q);

        //CompanyUtils.AddFocusNiche(RandomEnum<NicheComponent>.PickRandomItem(niches).NicheType, company);

        foreach (var n in niches)
            Companies.AddFocusNiche(company, n.niche.NicheType, Q);
    }

    IndustryType GetRandomIndustry()
    {
        return RandomEnum<IndustryType>.GenerateValue();
    }

    GameEntity GenerateProductCompany(string name, NicheType nicheType)
    {
        var product = Companies.GenerateProductCompany(Q, name, nicheType);

        var niche = Markets.Get(Q, product);
        var startCapital = Markets.GetStartCapital(niche, Q) * Random.Range(150, 200) / 100;

        Companies.SetResources(product, startCapital, "start capital in game restore");

        return product;
    }

    GameEntity GenerateInvestmentFund(string name, long money)
    {
        return Companies.GenerateInvestmentFund(Q, name, money);
    }

    GameEntity GenerateHoldingCompany(string name)
    {
        return Companies.GenerateHoldingCompany(Q, name);
    }

    void AttachToHolding(GameEntity p, GameEntity c)
    {
        Companies.AttachToGroup(Q, p, c);
    }


    void AddShareholder(GameEntity company, GameEntity investorId, int shares)
    {
        //Debug.Log($"Add Shareholder {investorId} with {shares} shares to {companyId}");
        Companies.AddShares(company, investorId, shares);
    }

    int PromoteToGroup(GameEntity company)
    {
        return Companies.PromoteProductCompanyToGroup(Q, company);
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

    private void AutoFillProposals()
    {
        //foreach (var c in Companies.GetNonFinancialCompanies(Q))
        //    Companies.SpawnProposals(Q, c);
    }

    void AutoFillNonFilledShareholders()
    {
        Companies.AutoFillNonFilledShareholders(Q, false);
    }

    /// -------------------------------------------

    void SpawnMarkets()
    {
        Markets.SpawnMarkets(Q);
        Markets.SpawnMarketingChannels(Q);
    }

    /// --- Products -----------------------------------
}
