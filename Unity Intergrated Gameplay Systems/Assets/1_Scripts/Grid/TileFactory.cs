using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : IFactory<Tile, TileFlyweight>
{
    public Dictionary<string, TileFlyweight> FactoryDictionary { get; set; }
    public GameObject Parent { get; set; }

    //References
    private GameSettings gameSettings;

    public TileFactory(GameSettings _gameSettings)
    {
        gameSettings = _gameSettings;

        //Dictionary
        FactoryDictionary = new Dictionary<string, TileFlyweight>();
        InitializeDictionary();

        //Parent
        Parent = new GameObject();
        Parent.name = "Tiles";
    }

    public void InitializeDictionary()
    {
        FactoryDictionary.Add("Dirt", new TileFlyweight(Resources.Load<GameObject>("Prefabs/Dirt"), gameSettings.Dirt.MaxHealth));
        FactoryDictionary.Add("Stone", new TileFlyweight(Resources.Load<GameObject>("Prefabs/Stone"), gameSettings.Stone.MaxHealth));
        FactoryDictionary.Add("HardStone", new TileFlyweight(Resources.Load<GameObject>("Prefabs/HardStone"), gameSettings.HardStone.MaxHealth));
        FactoryDictionary.Add("Invis", new TileFlyweight(Resources.Load<GameObject>("Prefabs/Invis"), 1000000));
    }

    public Tile Create(string _key)
    {
        if (FactoryDictionary.ContainsKey(_key))
        {
            return new Tile(FactoryDictionary[_key].Prefab, Parent.transform, FactoryDictionary[_key].MaxHealth);
        }
        else
        {
            Debug.LogError("TileFactory Create: key " + _key + " doesn't exist");
            return null;
        }
    }
}
