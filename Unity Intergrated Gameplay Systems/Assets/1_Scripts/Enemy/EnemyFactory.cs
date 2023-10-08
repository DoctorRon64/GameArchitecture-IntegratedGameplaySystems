using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : IFactory<Enemy, GameObject>
{
    public Dictionary<string, GameObject> FactoryDictionary { get; set; }
    public GameObject Parent { get; set; }

    //References
    private GameSettings gameSettings;

    public EnemyFactory(GameSettings _gameSettings)
    {
        gameSettings = _gameSettings;

        //Dictionary
        FactoryDictionary = new Dictionary<string, GameObject>();
        InitializeDictionary();

        //Parent
        Parent = new GameObject();
        Parent.name = "Enemys";
    }

    public void InitializeDictionary()
    {
        FactoryDictionary.Add("Enemy", Resources.Load<GameObject>("Prefabs/Enemy"));
    }

    public Enemy Create(string _key)
    {
        if (FactoryDictionary.ContainsKey(_key))
        {
            return new Enemy(FactoryDictionary[_key], Parent.transform, gameSettings);
        }
        else
        {
            Debug.LogError("EnemyFactory Create: key " + _key + " doesn't exist");
            return null;
        }
    }
}
