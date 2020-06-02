using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompanyView : View
    //, IPointerClickHandler
{
    GameEntity company;

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

    public void SetEntity(GameEntity company)
    {
        this.company = company;
        //Debug.Log($"Set entity: {company.company.Name}");

        GetComponent<FollowableCompany>().SetCompany(company);

        Animations.SetEntity(company);

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        if (company == null)
            return;

        CompanyName.text = company.company.Name;
        CompanyName.GetComponent<LinkToProjectView>().CompanyId = company.company.Id;

        if (company.hasProduct)
            RenderProductCompany();
    }

    void RenderProductCompany()
    {
        ProductStats.Render(company);
    }

    //void OnDisable()
    //{
    //    expand = false;
    //    Render();
    //}
}
