using Assets;
using Assets.Core;
using UnityEngine.UI;

public class MissingWorkerRoleView : View
{
    public Text RoleName;
    public Text RoleDescription;

    public Hint RoleDescriptionHint;

    private WorkerRole _role;
    public void SetEntity(WorkerRole role)
    {
        _role = role;

        RoleName.text = Humans.GetFormattedRole(role);
        RoleDescription.text = Teams.GetRoleDescription(role, Q, true, Flagship);
        RoleDescriptionHint.SetHint(Teams.GetRoleDescription(role, Q, true, Flagship));
    }

    public void OnShowCandidates()
    {
        PlaySound(Sound.Bubble1);
        
        ScreenUtils.SetIntegerWithoutUpdatingScreen(Q,(int)_role, "role");
        
        OpenModal("Candidates");
    }
}