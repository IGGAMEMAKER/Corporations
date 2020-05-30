using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompanyView : View
    //, IPointerClickHandler
{
    bool expand = false;
    bool canEdit = false;

    GameEntity company;

    [Header("Flagship Only")]
    public CanvasGroup competitorListView;

    public CanvasGroup Workers;
    public RenderCompanyWorkerListView workerListView;
    public Text CompanyName;

    public RenderCompanyAnimations Animations;

    public GameObject RaiseInvestments;

    public GameObject FirmLogo;

    RenderProductStatsInCompanyView _productStats;
    RenderProductStatsInCompanyView ProductStats
    {
        get
        {
            if (_productStats == null)
            {
                _productStats = GetComponent<RenderProductStatsInCompanyView>();
            }

            return _productStats;
        }
    }

    public void SetEntity(GameEntity company, bool canEdit)
    {
        this.company = company;

        this.canEdit = canEdit;

        //Debug.Log($"Set entity: {company.company.Name}");

        GetComponent<FollowableCompany>().SetCompany(company);

        workerListView.SetEntity(company);
        Animations.SetEntity(company);

        expand = false;
        RenderWorkersAndCompetitors();

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void RenderWorkersAndCompetitors()
    {
        DrawCanvasGroup(Workers, expand);

        bool showCompetitors = !expand && Flagship.isRelease;

        if (competitorListView != null)
            DrawCanvasGroup(competitorListView, showCompetitors);
    }

    public void ToggleState()
    {
        if (canEdit)
        {
            expand = !expand;

            RenderWorkersAndCompetitors();
            ResizeFirmLogo();

        }

        RenderInvestmentsButton();
    }

    void RenderInvestmentsButton()
    {
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);
        var playerCanExploreAdvancedTabs = hasReleasedProducts;
        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);

        var canRaiseInvestments = playerCanExploreAdvancedTabs || bankruptcyLooming;
        Draw(RaiseInvestments, canRaiseInvestments && expand);
    }

    void Render()
    {
        if (company == null)
            return;

        CompanyName.text = company.company.Name;
        CompanyName.GetComponent<LinkToProjectView>().CompanyId = company.company.Id;

        RenderInvestmentsButton();

        if (company.hasProduct)
        {
            RenderProductCompany();
        }
    }

    void ResizeFirmLogo()
    {
        var scale = 1f;

        bool isGlobalMode = !expand;
        if (isGlobalMode)
        {
            var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q);

            // share = 0
            var minSize = 0.85f;

            // share = 100
            var maxSize = 2.5f;

            scale = minSize + (maxSize - minSize) * marketShare / 100;
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }

    void RenderProductCompany()
    {
        ProductStats.Render(company);

        ResizeFirmLogo();
    }

    //void OnDisable()
    //{
    //    expand = false;
    //    Render();
    //}
}
