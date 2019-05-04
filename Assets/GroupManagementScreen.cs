using Assets.Utils;
using UnityEngine.UI;

public class GroupManagementScreen : View
    , IMenuListener
{
    public Text GroupBalance;
    public Text GroupName;
    public CompanyPreviewView CompanyPreviewView;

    public Text SelectedCompanyName;
    public Text SelectedCompanyBalance;
    public ColoredValuePositiveOrNegative SelectedCompanyROI;

    public Text ControlValue;

    public Button InvestButton;

    private void OnEnable()
    {
        if (MyGroupEntity == null)
        {
            MenuUtils.Navigate(GameContext, ScreenMode.DevelopmentScreen, null);

            return;
        }

        CompanyPreviewView.SetEntity(MyGroupEntity);

        GroupBalance.text = ValueFormatter.Shorten(MyGroupEntity.companyResource.Resources.money);

        Render();
    }

    void Start()
    {
        ListenMenuChanges(this);

        Destroy(CompanyPreviewView.GetComponent<LinkToProjectView>());
        CompanyPreviewView.gameObject.AddComponent<SelectCompanyController>().companyId = MyGroupEntity.company.Id;
    }

    void ToggleCEOButtons(bool show)
    {
        InvestButton.interactable = show;
    }

    void RenderCEOButtons()
    {
        if (IsHasShares() && IsDomineering())
            ToggleCEOButtons(true);
        else
            ToggleCEOButtons(false);
    }

    void RenderROI()
    {
        if (CompanyEconomyUtils.IsROICounable(SelectedCompany, GameContext))
        {
            long ROI = CompanyEconomyUtils.GetBalanceROI(SelectedCompany, GameContext);

            SelectedCompanyROI.UpdateValue(ROI);
        } else
        {
            SelectedCompanyROI.GetComponent<Text>().text = "???";
        }
    }

    void RenderControlValue()
    {
        if (SelectedCompany == MyGroupEntity || !IsHasShares())
            ControlValue.text = "---";
        else
        {
            int size = GetSizeOfShares();

            string shareholderStatus = GetShareholderStatus(size);

            ControlValue.text = $"{size}% ({shareholderStatus})";
        }
    }

    void Render()
    {
        var company = SelectedCompany ?? MyGroupEntity;

        var c = CompanyUtils.GetCompanyById(GameContext, company.company.Id);

        SelectedCompanyName.text = c.company.Name;

        RenderROI();

        RenderControlValue();

        if (SelectedCompany != MyGroupEntity)
            RenderCEOButtons();
        else
            ToggleCEOButtons(false);
    }

    bool IsHasShares()
    {
        return CompanyUtils.GetTotalShares(SelectedCompany.shareholders.Shareholders) > 0;
    }

    int GetSizeOfShares()
    {
        return CompanyUtils.GetShareSize(GameContext, SelectedCompany.company.Id, MyGroupEntity.shareholder.Id);
    }

    bool IsDomineering()
    {
        return GetSizeOfShares() > 50;
    }

    string GetShareholderStatus(int percent)
    {
        return CompanyUtils.GetShareholderStatus(percent);
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        Render();
    }
}
