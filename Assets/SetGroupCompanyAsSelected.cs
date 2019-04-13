using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class SetGroupCompanyAsSelected : View
    , IMenuListener
{
    public Text GroupBalance;
    public Text GroupName;
    public CompanyPreviewView CompanyPreviewView;

    public Text SelectedCompanyName;
    public Text SelectedCompanyBalance;
    public ColoredValuePositiveOrNegative SelectedCompanyROI;

    private void OnEnable()
    {
        if (MyGroupEntity == null)
        {
            MenuUtils.Navigate(GameContext, ScreenMode.DevelopmentScreen, null);

            return;
        }

        //MenuUtils.SetSelectedCompany(MyGroupEntity.company.Id, GameContext);

        CompanyPreviewView.SetEntity(MyGroupEntity);

        GroupBalance.text = ValueFormatter.Shorten(MyGroupEntity.companyResource.Resources.money);
        //GroupName.text = MyGroupEntity.company.Name;

        Render();
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

    void Render()
    {
        var c = CompanyUtils.GetCompanyById(GameContext, SelectedCompany.company.Id);

        SelectedCompanyName.text = c.company.Name;

        RenderROI();
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
