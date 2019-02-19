using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class WorkerAvatarView : MonoBehaviour {
    public Text LevelText;
    public Image SpecialisationImage;
    public Text Name;

    Sprite GetSpecialisationSprite(WorkerSpecialisation specialisation)
    {
        switch (specialisation)
        {
            case WorkerSpecialisation.Manager:
                return Resources.Load<Sprite>("collaboration");

            case WorkerSpecialisation.Marketer:
                return Resources.Load<Sprite>("promotion");

            default:
                return Resources.Load<Sprite>("Coding");
        }
    }

    public void Render(string fullName, int level, WorkerSpecialisation specialisation)
    {
        LevelText.text = level.ToString();
        SpecialisationImage.sprite = GetSpecialisationSprite(specialisation);

        Name.text = fullName;
    }
}
