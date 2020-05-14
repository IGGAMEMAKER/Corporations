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
    public RenderFlagshipCompetitorListView competitorListView;

    public GameObject Workers;
    public RenderCompanyWorkerListView workerListView;
    public Text CompanyName;

    public RenderCompanyAnimations Animations;

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

        workerListView.SetEntity(company);
        Animations.SetEntity(company);

        expand = false;
        Draw(Workers, expand);

        Render();
    }

    public void ToggleState()
    {
        if (canEdit)
        {
            expand = !expand;

            Draw(Workers, expand);
            Draw(competitorListView, !expand);
        }
    }

    void Render()
    {
        if (company != null)
        {
            CompanyName.text = company.company.Name;

            if (company.hasProduct)
            {
                RenderProductCompany();
            }
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void RenderProductCompany()
    {
        ProductStats.Render(company);

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

        FirmLogo.transform.localScale = new Vector3(scale, scale, scale);
    }

    //void OnDisable()
    //{
    //    expand = false;
    //    Render();
    //}
}
