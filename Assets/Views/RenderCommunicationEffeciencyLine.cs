using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class RenderCommunicationEffeciencyLine : View
{
    public Image Line;
    public Text Effeciency;
    public Transform CEOTransform;

    public void SetEntity(GameEntity company, WorkerRole role, Transform Center)
    {
        CEOTransform = Center;
        //LookAtCenter();

        var c = GetComponent<TeamsAttachedToManagerView>();

        c.TeamType = GetTeamTypeByManagerRole(role);
        c.ViewRender();
    }

    TeamType GetTeamTypeByManagerRole(WorkerRole role)
    {
        switch (role)
        {
            case WorkerRole.MarketingLead: return TeamType.MarketingTeam;
            case WorkerRole.TeamLead: return TeamType.DevelopmentTeam;

            default: return TeamType.CrossfunctionalTeam;
        }
    }

    private void Update()
    {
        //LookAtCenter();
    }

    void LookAtCenter()
    {
        var p1 = transform.position;
        var p2 = CEOTransform.position;

        var diff = p2 - p1;

        var rotation = Mathf.Acos(diff.normalized.x);

        //Debug.Log($"Look at: from {p1.x} {p1.y} {p1.z} to {p2.x} {p2.y} {p2.z}");
        //Debug.Log($"Diff: {diff.x} {diff.y} {diff.z}");
        //Debug.Log($"Rotation: {rotation}");

        //var lineWidth = diff.magnitude - 50f;
        Line.transform.rotation = Quaternion.Euler(0, 0, rotation * Mathf.Rad2Deg);
        Line.transform.position = p1 + diff / 2;

        Draw(Line, true);
    }
}
