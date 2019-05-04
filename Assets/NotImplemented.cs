using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Hint))]
public class NotImplemented : MonoBehaviour
{
    void Start()
    {
        GetComponent<Hint>().SetHint("This game feature is not implemented yet");

        if (GetComponent<Button>() != null)
            GetComponent<Button>().interactable = false;
    }
}
