using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class WorkerAvatarView : MonoBehaviour {
    public Text LevelText;
    public Image SpecialisationImage;
    public Text Name;

    public Sprite ManagerIcon;
    public Sprite MarketerIcon;
    public Sprite ProgrammerIcon;

    private void Start()
    {
        ManagerIcon = Resources.Load<Sprite>("collaboration");
        MarketerIcon = Resources.Load<Sprite>("promotion");
        ProgrammerIcon = Resources.Load<Sprite>("Coding");
    }

    Sprite GetSpecialisationSprite(WorkerSpecialisation specialisation)
    {
        switch (specialisation)
        {
            case WorkerSpecialisation.Manager:
                return ManagerIcon;
            case WorkerSpecialisation.Marketer:
                return MarketerIcon;
            case WorkerSpecialisation.Programmer:
                return ProgrammerIcon;

            default:
                return ProgrammerIcon;
        }
    }

    public void Render(string fullName, int level, WorkerSpecialisation specialisation)
    {
        LevelText.text = level.ToString();
        SpecialisationImage.sprite = GetSpecialisationSprite(specialisation);

        Name.text = fullName;
    }
}
