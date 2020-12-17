using UnityEngine;

public class PopupBackgroundController : MonoBehaviour
{
    void OnEnable()
    {
        gameObject.AddComponent<PopupBackgroundAnimation>();
    }
}
