using Assets;
using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GetFastCash : CompanyUpgradeButton
{
    public Text AmountOfLoans;

    public override void Execute()
    {
        var fund = Investments.GetRandomInvestmentFund(Q);

        var valuation = Economy.GetCompanyCost(Q, MyCompany);
        var offer = sum;

        var balance = Economy.BalanceOf(MyCompany);
        var maxCashLimit = valuation * 7 / 100;

        bool hasCashOverflow = balance > maxCashLimit;

        // render cash overflow popup
        if (hasCashOverflow)
        {
            NotificationUtils.AddPopup(Q, new PopupMessageInfo("You have too much cash!", "Spend it first"));
            return;
        }

        SoundManager.Play(Sound.MoneyIncome);

        bool hasShareholders = MyCompany.shareholders.Shareholders.Count > 1;
        if (!hasShareholders)
        {
            Companies.AddInvestmentProposal(Q, MyCompany.company.Id,
                new InvestmentProposal
                {
                    InvestorBonus = InvestorBonus.None,
                    Offer = offer,
                    ShareholderId = fund,
                    Valuation = valuation,
                    WasAccepted = false
                });
            Companies.AcceptInvestmentProposal(Q, MyCompany.company.Id, fund);
        }
        else
        {
            for (var i = 0; i < MyCompany.shareholders.Shareholders.Count; i++)
            {
                var investorId = MyCompany.shareholders.Shareholders.Keys.ToArray()[i];
                var inv = Companies.GetInvestorById(Q, investorId);

                bool isCeo = inv.shareholder.InvestorType == InvestorType.Founder;

                if (!isCeo)
                {
                    Companies.AddInvestmentProposal(Q, MyCompany.company.Id,
                        new InvestmentProposal
                        {
                            InvestorBonus = InvestorBonus.None,
                            Offer = offer,
                            ShareholderId = investorId,
                            Valuation = valuation,
                            WasAccepted = false
                        });
                    Companies.AcceptInvestmentProposal(Q, MyCompany.company.Id, investorId);
                }
            }

            //Companies.AddResources(MyCompany, sum);
        }

    }

    void RenderAmountOfLoans()
    {
        var valuation = Economy.GetCompanyCost(Q, MyCompany);
        var offer = sum;

        var balance = Economy.BalanceOf(MyCompany);
        var maxCashLimit = valuation * 7 / 100;

        var amountOfLoans = (maxCashLimit - balance) / offer;
        AmountOfLoans.text = amountOfLoans.ToString();
    }

    int fraction = 2;

    long sum => Economy.GetCompanyCost(Q, MyCompany) * fraction / 100;

    public override string GetBenefits()
    {
        return $"Get {Format.Money(sum)}";
        //return $"Get {Format.Money(sum)} for {fraction}% of company";
    }

    public override string GetButtonTitle()
    {
        RenderAmountOfLoans();
        //AmountOfLoans.text = "2";

        return "Raise Investments";
    }

    public override string GetHint()
    {
        return "";
    }

    public override bool GetState()
    {
        return true;
    }
}
