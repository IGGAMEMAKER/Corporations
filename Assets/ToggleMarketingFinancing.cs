using Assets.Utils;

public class ToggleMarketingFinancing : ToggleButtonController
{
    int companyId;
    
    // max financing
    int MaxFinancing => 3;

    private void Start()
    {
        Render();
    }

    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;
    }

    public override void Execute()
    {
        var company = CompanyUtils.GetCompanyById(GameContext, companyId);
        var financing = company.financing.Financing[Financing.Marketing];

        company.financing.Financing[Financing.Marketing] = financing == MaxFinancing ? MaxFinancing - 1 : MaxFinancing;
    }

    void Render()
    {
        var company = CompanyUtils.GetCompanyById(GameContext, companyId);
        var financing = company.financing.Financing[Financing.Marketing];

        var isChosen = financing == MaxFinancing;
        ToggleIsChosenComponent(isChosen);
    }

}
