using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : IFactory<Player, GameObject>
{
    public Dictionary<string, GameObject> FactoryDictionary { get; set; }
    public GameObject Parent { get; set; }

    //References
    private InputManager inputManager;

    public PlayerFactory(InputManager _inputManager)
    {
        inputManager = _inputManager;

        //Dictionary
        FactoryDictionary = new Dictionary<string, GameObject>();  
        InitializeDictionary();

        //Parent
        Parent = new GameObject();
        Parent.name = "Players";
    }
    
    public void InitializeDictionary()
    {
        FactoryDictionary.Add("Player", Resources.Load<GameObject>("Prefabs/Player"));
    }

    public Player Create(string _key)
    {
        if (FactoryDictionary.ContainsKey(_key))
        {
            return new Player(FactoryDictionary[_key], inputManager, Parent.transform);
        }
        else
        {
            Debug.LogError("TileFactory Create: key " + _key + " doesn't exist");
            return null;
        }
    }
}
