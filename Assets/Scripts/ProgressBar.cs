using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    float progress;
    Slider slider;

    public Text Text;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        progress = 0;
    }

    public void SetValue (float val)
    {
        // val from 0 to 100f
        progress = val;

        if (!slider)
            slider = GetComponent<Slider>();

        slider.value = progress / 100;

        Text.text = (int)progress + "%";
    }
}
