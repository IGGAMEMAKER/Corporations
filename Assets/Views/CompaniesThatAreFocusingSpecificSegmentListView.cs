using UnityEngine;

public class CompaniesThatAreFocusingSpecificSegmentListView : ListView
{
    int segmentId;
    public override void SetItem<T>(Transform t, T entity)
    {
        //t.GetComponent<CompanyViewOnMap>().SetEntity(entity as GameEntity, false, false);
        t.GetComponent<CompanyViewOnAudienceMap>().SetEntity(entity as GameEntity, segmentId);
    }

    public void SetCompanies(GameEntity[] companies, int segmentId)
    {
        this.segmentId = segmentId;
        SetItems(companies);
    }
}