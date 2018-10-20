using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAvailable : MonoBehaviour {
	
    // Use this for initialization
	void Start () {
        GameObject image = new GameObject("background");
        Image background = image.AddComponent<Image>();
        Canvas c = image.AddComponent<Canvas>();

        image.SetActive(true);

        c.overrideSorting = true;
        c.sortingOrder = -1;

        var parent = this.gameObject;
        image.transform.SetParent(parent.transform, false);

        RectTransform parentTransform = parent.GetComponent<RectTransform>();
        float width = parentTransform.rect.width + 100f;
        float height = parentTransform.rect.height + 100f;

        RectTransform rect = image.GetComponent<RectTransform>();
        rect.rect.Set(0, 0, width, height);

        Debug.LogFormat("Start UIAvailable {0}, {1}, {2}", image.activeInHierarchy, image.activeSelf, parent.activeInHierarchy);


        background.sprite = Resources.Load<Sprite>("interrupt-danger");
    }

    void OverrideSortingOrder ()
    {

    }

    private void OnEnable()
    {
        Debug.Log("OnEnable UIAvailable");

        Canvas c = this.gameObject.GetComponentInChildren<Canvas>();
        c.overrideSorting = true;
    }

    // Update is called once per frame
    void Update () {
	}
}
