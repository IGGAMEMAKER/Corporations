using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ShareholderPreviewView : View
{
    int ShareholderId;
    int Shares;
    int TotalShares;

    public Text NameLabel;
    public Text SharesLabel;
    public Hint SharesAmountHint;

    LinkToProjectView LinkToCompanyPreview;

    GameEntity ShareholderEntity;

    public void SetEntity(int shareholderId, int shares)
    {
        ShareholderId = shareholderId;
        Shares = shares;

        TotalShares = CompanyUtils.GetTotalShares(SelectedCompany.shareholders.Shareholders);
        ShareholderEntity = CompanyUtils.GetInvestorById(GameContext, shareholderId);

        Render();
    }

    int GetCompanyIdByInvestorId(int shareholderId)
    {
        return CompanyUtils.GetCompanyIdByInvestorId(GameContext, shareholderId);
    }

    void AddLinkIfPossible()
    {
        if (ShareholderEntity == null)
            return;

        if (!ShareholderEntity.hasCompany)
            return;

        GameObject o = NameLabel.gameObject;

        if (o.GetComponent<LinkToProjectView>() == null)
            LinkToCompanyPreview = o.AddComponent<LinkToProjectView>();
                
        LinkToCompanyPreview.CompanyId = GetCompanyIdByInvestorId(ShareholderId);
    }

    void Render()
    {
        NameLabel.text = ShareholderEntity.shareholder.Name;
        SharesLabel.text = Shares * 100 / TotalShares + "%";
        SharesAmountHint.SetHint("");

        AddLinkIfPossible();
    }
}
