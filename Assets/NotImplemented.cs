using UnityEngine;

[RequireComponent(typeof(Hint))]
public class NotImplemented : MonoBehaviour
{
    void Start()
    {
        GetComponent<Hint>().SetHint("This game feature is not implemented yet");
    }
}
