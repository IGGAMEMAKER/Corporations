using Assets.Utils;

public class ToggleMarketingFinancing : ToggleButtonController
{
    int companyId;
    
    // max financing
    int MaxFinancing => 2;

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

        var newFinancing = 0;

        if (financing == 0)
        {
            newFinancing = 1;
        }
        else if (financing == 1)
        {
            newFinancing = 2;
        }
        else
        {
            newFinancing = 0;
        }

        company.financing.Financing[Financing.Marketing] = newFinancing;
    }

    void Render()
    {
        var company = CompanyUtils.GetCompanyById(GameContext, companyId);
        var financing = company.financing.Financing[Financing.Marketing];

        var isChosen = financing == MaxFinancing;
        //ToggleIsChosenComponent(isChosen);
    }
}
