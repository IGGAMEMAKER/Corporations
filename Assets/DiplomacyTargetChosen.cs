using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        buttonInteractables = GetComponents<IButtonInteractable>();
        Button = GetComponent<Button>();
    }

    bool CheckInteractability()
    {
        foreach (var c in buttonInteractables)
            if (!c.Interactable()) return false;

        return true;
    }

    void Update()
    {
        bool interactable = CheckInteractability();

        Button.interactable = interactable;
    }
}

public class DiplomacyTargetChosen : MonoBehaviour, IButtonInteractable
{
    public Dropdown dropdown;

    public bool Interactable()
    {
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
