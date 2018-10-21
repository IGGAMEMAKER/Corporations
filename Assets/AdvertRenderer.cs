using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvertRenderer : ListRenderer {
    int ADS_PER_LINE = 5;

    public override void UpdateObject<Advert>(GameObject gameObject, Advert ModelName)
    {
        itemsPerLine = ADS_PER_LINE;

        GameObject image = gameObject.transform.GetChild(0).gameObject;
        GameObject text = gameObject.transform.GetChild(1).gameObject;
        text.GetComponent<Text>().text = "Instantiated text";

        GameObject Button = gameObject.transform.GetChild(2).gameObject;
    }
}

public abstract class ListRenderer : MonoBehaviour
{
    public GameObject PrefabInstance;
    public List<GameObject> gameObjects;

    public int itemsPerLine;

    // Use this for initialization
    void Start()
    {

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

        for (var i = 0; i < gameObjects.Count; i++)
        {
            int x = i % itemsPerLine;
            int y = i / itemsPerLine;

            GameObject g = InstantiateObject(x, y);
            UpdateObject(g, objects);
        }
    }
}
