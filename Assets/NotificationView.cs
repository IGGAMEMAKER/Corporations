using UnityEngine;
using UnityEngine.UI;

public class NotificationView : MonoBehaviour {
    public void UpateMessage(string message)
    {
        Debug.Log("Update Message: " + message);
        gameObject.GetComponentInChildren<Text>().text = message;
        gameObject.GetComponentInChildren<TextBlink>().Reset();
    }
}
