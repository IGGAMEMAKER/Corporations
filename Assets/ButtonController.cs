using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonController : MonoBehaviour
{
    Button Button;

    public abstract void Execute();

    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(Execute);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(Execute);
    }
}
