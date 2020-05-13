using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompanyView : View, IPointerClickHandler
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

    void Start()
    {
        if (company == null)
            company = Flagship;

        Render();
    }

    public void SetEntity(GameEntity company, bool canEdit)
    {
        this.company = company;

        this.canEdit = canEdit;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (canEdit)
        {
            expand = !expand;

            competitorListView.RenderCompetitors(!expand);
        }

        Render();
    }

    void Render()
    {
        Draw(Workers, expand);
        workerListView.SetEntity(company);

        Animations.SetEntity(company);

        CompanyName.text = company.company.Name;

        if (company.hasProduct)
        {
            ProductStats.Render(company);
        }
    }

    void OnDisable()
    {
        expand = false;
        Render();
    }
}
