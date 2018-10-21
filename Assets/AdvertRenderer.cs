using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvertRenderer : ListRenderer {
    public override int itemsPerLine
    {
        get { return 5; }
        set { }
    }

    public override void UpdateObject<Advert>(GameObject gameObject, Advert ModelName)
    {
        Debug.Log("UpdateObject Advert");

        GameObject image = gameObject.transform.GetChild(0).gameObject;
        GameObject text = gameObject.transform.GetChild(1).gameObject;
        text.GetComponent<Text>().text = "Instantiated text";

        GameObject Button = gameObject.transform.GetChild(2).gameObject;
    }
}

public abstract class ListRenderer : MonoBehaviour
{
    public GameObject PrefabInstance;
    List<GameObject> gameObjects;

    public abstract int itemsPerLine { get; set; }

    // Use this for initialization
    void Start()
    {
        gameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RemoveCurrentObjects()
    {
        for (int i = 0; i < gameObjects.Count; i++)
            Destroy(gameObjects[i]);

        gameObjects.Clear();
    }

    GameObject InstantiateObject(int x, int y)
    {
        float spacing = 50f;

        Vector3 pos = new Vector3(x, y, 0) * spacing;
        GameObject g = Instantiate(PrefabInstance, pos, Quaternion.identity);

        g.transform.SetParent(this.gameObject.transform, false);

        gameObjects.Add(g);

        return g;
    }

    abstract public void UpdateObject<T>(GameObject gameObject, T ModelName);

    public void UpdateList<T> (List<T> objects)
    {
        RemoveCurrentObjects();

        for (var i = 0; i < objects.Count; i++)
        {
            int x = i % itemsPerLine;
            int y = i / itemsPerLine;

            GameObject g = InstantiateObject(x, y);
            UpdateObject(g, objects);
        }
    }
}
