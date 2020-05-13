using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompanyView : View, IPointerClickHandler
{
    bool showWorkers = false;
    bool canEdit = false;

    GameEntity company;


    public GameObject Workers;
    public Text CompanyName;

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
            showWorkers = !showWorkers;
        }

        Render();
    }

    void Render()
    {
        Draw(Workers, showWorkers);

        CompanyName.text = company.company.Name;

        if (company.hasProduct)
        {
            ProductStats.Render(company);
        }
    }

    void OnDisable()
    {
        showWorkers = false;
        Render();
    }
}
