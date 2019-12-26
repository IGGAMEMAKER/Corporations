public class RenderCEO : View
{
    void OnEnable()
    {
        GetComponent<WorkerView>().SetEntity(SelectedCompany.cEO.HumanId, WorkerRole.Business);
    }
}
