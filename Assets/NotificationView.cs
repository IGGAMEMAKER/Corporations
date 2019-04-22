using UnityEngine;
using UnityEngine.UI;

public class NotificationView : MonoBehaviour {
    void Start()
    {
        // attach to notifications


    }

    void Render()
    {

    }

    public void UpateMessage(string message)
    {

        Debug.Log("Update Message: " + message);

        Text text = gameObject.GetComponentInChildren<Text>();
        text.text = message;

        text.gameObject.AddComponent<TextBlink>();
    }
}
