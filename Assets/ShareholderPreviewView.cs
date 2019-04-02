using Entitas;
using System;
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

    public LinkToCompanyPreview LinkToCompanyPreview;

    GameEntity ShareholderEntity;

    public void SetEntity(int shareholderId, int shares, int totalShares)
    {
        var investorGroup = GameContext.GetEntities(GameMatcher.Shareholder);

        ShareholderId = shareholderId;
        Shares = shares;
        TotalShares = totalShares;

        ShareholderEntity = Array.Find(investorGroup, s => s.shareholder.Id == shareholderId);

        Render();
    }

    int GetCompanyIdByInvestorId(int shareholderId)
    {
        return Array.Find(GameContext.GetEntities(GameMatcher.Shareholder), e => e.shareholder.Id == shareholderId).company.Id;
    }

    void AddLinkIfPossible()
    {
        if (ShareholderEntity == null)
            return;

        if (!ShareholderEntity.hasCompany)
            return;

        GameObject o = NameLabel.gameObject;

        if (o.GetComponent<LinkToCompanyPreview>() == null)
            LinkToCompanyPreview = o.AddComponent<LinkToCompanyPreview>();
                
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
