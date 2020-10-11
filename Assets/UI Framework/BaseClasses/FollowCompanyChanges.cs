using Assets.Core;
using System.Collections.Generic;

public class FollowCompanyChanges : Controller
    , IBrandingListener
    , IMarketingListener
    , IProductListener
{
    public bool BrandingChanges = false;
    public bool ClientChanges = false;
    public bool ProductChanges = false;

    GameEntity _Company;
    GameEntity Company
    {
        get
        {
            if (_Company == null)
            {
                var c = GetComponentInParent<FollowableCompany>();

                if (c == null)
                    return null;
                else
                    _Company = c.Company;

            }

            return _Company;
        }
    }

    public void SetCompany(GameEntity company)
    {
        _Company = company;

        DetachListeners();

        AttachListeners();
    }

    public override void AttachListeners()
    {
        if (Company == null)
            return;

        if (BrandingChanges)
            Company.AddBrandingListener(this);

        if (ClientChanges)
            Company.AddMarketingListener(this);

        if (ProductChanges)
            Company.AddProductListener(this);
    }

    public override void DetachListeners()
    {
        if (Company == null)
            return;

        if (BrandingChanges && Company.hasBrandingListener)
            Company.RemoveBrandingListener(this);

        if (ClientChanges && Company.hasMarketingListener)
            Company.RemoveMarketingListener(this);

        if (ProductChanges && Company.hasProductListener)
            Company.RemoveProductListener(this);
    }

    void IBrandingListener.OnBranding(GameEntity entity, float brandPower)
    {
        Render();
    }

    void IMarketingListener.OnMarketing(GameEntity entity, Dictionary<int, long> clients1)
    {
        Render();
    }

    void IProductListener.OnProduct(GameEntity entity, NicheType niche, int concept)
    {
        Render();
    }
}
