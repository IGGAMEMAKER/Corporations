using Assets.Core;
using UnityEngine;

public class NotificationRendererBankruptcy : NotificationRenderer<NotificationMessageBankruptcy>
{
    public override string GetDescription(NotificationMessageBankruptcy message)
    {
        var name = Companies.Get(Q, message.CompanyId).company.Name;

        return $"Company {name} is BANKRUPT! Everything has it's lifespan...";
    }

    public override Color GetNewsColor(NotificationMessageBankruptcy message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        bool isCompetitor = Companies.IsCompetingCompany(MyCompany, c, Q);
        var colName = isCompetitor ? Colors.COLOR_POSITIVE : Colors.COLOR_PANEL_BASE;

        return Visuals.GetColorFromString(colName);
    }

    public override string GetTitle(NotificationMessageBankruptcy message)
    {
        var name = Companies.Get(Q, message.CompanyId).company.Name;

        return $"Company {name} is BANKRUPT!";
    }

    public override void SetLink(NotificationMessageBankruptcy message, GameObject LinkToEvent)
    {
        
    }
}


public class NotificationRendererRecruitingManager : NotificationRenderer<NotificationMessageManagerRecruiting>
{
    public override string GetDescription(NotificationMessageManagerRecruiting message)
    {
        var company = Companies.Get(Q, message.CompanyId);
        var human = Humans.Get(Q, message.HumanId);

        var role = Humans.GetRole(human);
        var rating = Humans.GetRating(human);

        if (message.Successful)
        {
            return $"{Humans.GetFormattedRole(role)} {human.human.Name} ({rating}LVL) joined your competitor: {company.company.Name}";
        }

        var offer = human.workerOffers.Offers.Find(o => o.CompanyId == message.CompanyId);
        var dateFormatted = ScheduleUtils.GetFormattedDate(offer.DecisionDate);

        return $"\n<size=30>We need to send a counter offer until {dateFormatted}</size>" +
            $"\n\nor {Humans.GetFormattedRole(role)} {Humans.GetFullName(human)} ({rating}LVL) will join {company.company.Name}";
    }

    public override Color GetNewsColor(NotificationMessageManagerRecruiting message)
    {
        return Visuals.Negative();
    }

    public override string GetTitle(NotificationMessageManagerRecruiting message)
    {
        var company = Companies.Get(Q, message.CompanyId);
        var human = Humans.Get(Q, message.HumanId);

        if (message.Successful)
        {
            //return $"{Humans.GetFullName(human)} ";
            return $"Manager left your company";
        }
        return $"{company.company.Name} wants to recruit your worker";
    }

    public override void SetLink(NotificationMessageManagerRecruiting message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToHuman>().SetHumanId(message.HumanId);
    }
}


public class NotificationRendererAcquisition : NotificationRenderer<NotificationMessageBuyingCompany>
{
    public override string GetDescription(NotificationMessageBuyingCompany message)
    {
        return GetTitle(message);
    }

    public override Color GetNewsColor(NotificationMessageBuyingCompany message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        bool isCompetitor = Companies.IsCompetingCompany(MyCompany, c, Q);
        var colName = isCompetitor ? Colors.COLOR_NEGATIVE : Colors.COLOR_PANEL_BASE;

        return Visuals.GetColorFromString(colName);
    }

    public override string GetTitle(NotificationMessageBuyingCompany message)
    {
        var name = Companies.Get(Q, message.CompanyId).company.Name;
        var buyer = Companies.GetInvestorName(Q, message.BuyerInvestorId);

        return $"ACQUISITION: {buyer} bought {name} for {Format.Money(message.Bid)}!";
    }

    public override void SetLink(NotificationMessageBuyingCompany message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}

public class NotificationRendererPromoteCompany : NotificationRenderer<NotificationMessageCompanyTypeChange>
{
    public override string GetTitle(NotificationMessageCompanyTypeChange message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        return $"PROMOTION: {message.PreviousName} => {c.company.Name}";
    }

    public override string GetDescription(NotificationMessageCompanyTypeChange message)
    {
        var product = Companies.Get(Q, message.CompanyId);

        return $"{product.company.Name} promoted to GROUP of companies. They want to buy companies and make more innovative products!";
    }

    public override void SetLink(NotificationMessageCompanyTypeChange message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }

    public override Color GetNewsColor(NotificationMessageCompanyTypeChange message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        bool isCompetitor = Companies.IsCompetingCompany(MyCompany, c, Q);
        var colName = isCompetitor ? Colors.COLOR_NEGATIVE : Colors.COLOR_PANEL_BASE;

        return Visuals.GetColorFromString(colName);
    }
}
