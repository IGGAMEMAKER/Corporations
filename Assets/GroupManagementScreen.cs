using Assets.Utils;
using UnityEngine;
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
    public Button IncreaseDividends;
    public Button DecreaseDividends;

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

    void ToggleCEOButtons(bool show)
    {
        InvestButton.interactable = show;
        DecreaseDividends.interactable = show;
        IncreaseDividends.interactable = show;
    }

    bool IsHasShares()
    {
        return CompanyUtils.GetTotalShares(SelectedCompany.shareholders.Shareholders) > 0;
    }

    void RenderCEOButtons()
    {
        if (IsHasShares() && IsDomineering())
        {
            ToggleCEOButtons(true);
        }
        else
        {
            ToggleCEOButtons(false);
        }
    }

    void RenderROI()
    {
        if (CompanyEconomyUtils.IsROICounable(SelectedCompany, GameContext))
        {
            long ROI = CompanyEconomyUtils.GetBalanceROI(SelectedCompany, GameContext);

            Debug.Log("RENDER ROI " + ROI);

            SelectedCompanyROI.UpdateValue(ROI);
        } else
        {
            SelectedCompanyROI.GetComponent<Text>().text = "???";
        }
    }

    int GetSizeOfShares()
    {
        return CompanyUtils.GetShareSize(GameContext, SelectedCompany.company.Id, MyGroupEntity.shareholder.Id);
    }

    bool IsDomineering()
    {
        return GetSizeOfShares() > 50;
    }

    void RenderControlValue()
    {
        if (SelectedCompany == MyGroupEntity || !IsHasShares())
            ControlValue.text = "---";
        else
            ControlValue.text = GetSizeOfShares() + "%";
    }

    void Render()
    {
        var c = CompanyUtils.GetCompanyById(GameContext, SelectedCompany.company.Id);

        SelectedCompanyName.text = c.company.Name;

        RenderROI();

        RenderControlValue();

        if (SelectedCompany != MyGroupEntity)
            RenderCEOButtons();
        else
            ToggleCEOButtons(false);
    }

    void Start()
    {
        ListenMenuChanges(this);

        Destroy(CompanyPreviewView.GetComponent<LinkToCompanyPreview>());
        CompanyPreviewView.gameObject.AddComponent<SelectCompanyController>().companyId = MyGroupEntity.company.Id;
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        Render();
    }
}
