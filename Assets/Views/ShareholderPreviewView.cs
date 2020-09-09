using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class ShareholderPreviewView : View
{
    int ShareholderId;
    BlockOfShares Shares;
    int TotalShares;

    public Text NameLabel;
    public Text SharesLabel;
    public Hint SharesAmountHint;

    LinkToProjectView LinkToCompanyPreview;

    GameEntity ShareholderEntity;

    public void SetEntity(int shareholderId, BlockOfShares shares)
    {
        ShareholderId = shareholderId;
        Shares = shares;

        TotalShares = Companies.GetTotalShares(SelectedCompany.shareholders.Shareholders);
        ShareholderEntity = Companies.GetInvestorById(Q, shareholderId);

        Render();
    }

    int GetCompanyIdByInvestorId(int shareholderId)
    {
        return Investments.GetCompanyByInvestorId(Q, shareholderId).company.Id;
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
        SharesLabel.text = Shares.amount * 100 / TotalShares + "%";
        SharesAmountHint.SetHint("");
        SharesAmountHint.gameObject.GetComponent<LinkToBuyShares>().SetInvestorId(ShareholderEntity.shareholder.Id);

        AddLinkIfPossible();
    }
}
