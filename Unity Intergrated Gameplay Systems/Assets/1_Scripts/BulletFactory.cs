using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : IFactory<GameObject, GameObject>
{
    public Dictionary<string, GameObject> FactoryDictionary { get; set; }
    public GameObject Parent { get; set; }

    public BulletFactory()
    {
        //Dictionary
        FactoryDictionary = new Dictionary<string, GameObject>();
        InitializeDictionary();

        //Parent
        Parent = new GameObject();
        Parent.name = "Bullets";
    }

    public void InitializeDictionary()
    {
        FactoryDictionary.Add("Bullet", Resources.Load<GameObject>("Prefabs/Bullet"));
    }

    public GameObject Create(string _key)
    {
        if (FactoryDictionary.ContainsKey(_key))
        {
            GameObject prefab = FactoryDictionary[_key];
            return GameObject.Instantiate(prefab);
        }
        else
        {
            Debug.LogError("TileFactory Create: key " + _key + " doesn't exist");
            return null;
        }
    }
}
