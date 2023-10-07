using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : IFactory<Bullet, GameObject>
{
    public Dictionary<string, GameObject> FactoryDictionary { get; set; }
    public GameObject Parent { get; set; }

    private GameSettings gameSettings;

    public BulletFactory(GameSettings _gameSettings)
    {
        gameSettings = _gameSettings;

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

    public Bullet Create(string _key)
    {
        if (FactoryDictionary.ContainsKey(_key))
        {
            return new Bullet(FactoryDictionary[_key], Parent.transform, gameSettings);
        }
        else
        {
            Debug.LogError("TileFactory Create: key " + _key + " doesn't exist");
            return null;
        }
    }
}
