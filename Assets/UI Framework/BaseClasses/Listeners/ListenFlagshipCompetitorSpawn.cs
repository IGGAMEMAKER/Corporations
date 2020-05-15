public class ListenFlagshipCompetitorSpawn: Controller
    , IAnyCompanyListener
{
    public override void AttachListeners()
    {
        AnyChangeListener().AddAnyCompanyListener(this);
    }

    public override void DetachListeners()
    {
        AnyChangeListener().RemoveAnyCompanyListener(this);
    }

    void IAnyCompanyListener.OnAnyCompany(GameEntity entity, int id, string name, CompanyType companyType)
    {
        if (entity.hasProduct && entity.product.Niche == Flagship.product.Niche)
            Render();
    }
}
