using UnityEngine;
using UnityEngine.UI;

public class NotificationView : MonoBehaviour {
    public void UpateMessage(string message)
    {
        Debug.Log("Update Message: " + message);

        Text text = gameObject.GetComponentInChildren<Text>();
            text.text = message;
        text.gameObject.AddComponent<TextBlink>();
    }
}
