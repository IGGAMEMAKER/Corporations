using UnityEngine;
using UnityEngine.UI;

public class MockText : MonoBehaviour
{
    public Text Text;

    public void SetEntity(string text)
    {
        Text.text = text;
    }
}
