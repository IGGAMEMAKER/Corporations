using System.Collections.Generic;

public class FillCompanyShareholders : View
{
    ShareholdersListView ShareholdersListView;

    private void OnEnable()
    {
        Render();
    }

    void Render()
    {
        //var shareholders = GetShareholders();

        //int totalShares = GetTotalShares(shareholders);

        //ShareholdersListView = GetComponent<ShareholdersListView>();
        //ShareholdersListView.SetItems(shareholders, totalShares);
    }

    Dictionary<int, int> GetShareholders()
    {
        Dictionary<int, int> shareholders = new Dictionary<int, int>();

        if (SelectedCompany.hasShareholders)
            shareholders = SelectedCompany.shareholders.Shareholders;

        return shareholders;
    }

    int GetTotalShares(Dictionary<int, int> shareholders)
    {
        int totalShares = 0;

        foreach (var e in shareholders)
            totalShares += e.Value;

        return totalShares;
    }
}
