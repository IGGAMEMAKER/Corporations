using UnityEngine;
using UnityEngine.UI;

public interface IButtonInteractable
{
    bool Interactable();
}

public class ButtonInteractivityController : MonoBehaviour
{
    IButtonInteractable[] buttonInteractables;
    Button Button;

    void Start()
    {
        buttonInteractables = GetComponents<IButtonInteractable>();
        Button = GetComponent<Button>();
    }

    void Update()
    {
        bool interactable = CheckInteractability();

        Button.interactable = interactable;
    }

    bool CheckInteractability()
    {
        foreach (var c in buttonInteractables)
            if (!c.Interactable()) return false;

        return true;
    }
}

public class DiplomacyTargetChosen : MonoBehaviour, IButtonInteractable
{
    public Dropdown dropdown;

    public bool Interactable()
    {
        return true;
    }
}
