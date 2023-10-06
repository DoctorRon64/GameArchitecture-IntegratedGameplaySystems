using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : IFactory<Tile, TileFlyweight>
{
    public Dictionary<string, TileFlyweight> FactoryDictionary { get; set; }
    public GameObject Parent { get; set; }

    public TileFactory()
    {
        //Dictionary
        FactoryDictionary = new Dictionary<string, TileFlyweight>();
        InitializeDictionary();

        //Parent
        Parent = new GameObject();
        Parent.name = "Tiles";
    }

    public void InitializeDictionary()
    {
        FactoryDictionary.Add("Dirt", new TileFlyweight(Resources.Load<Sprite>("Sprites/Dirt")));
        FactoryDictionary.Add("Stone", new TileFlyweight(Resources.Load<Sprite>("Sprites/Stone")));
        FactoryDictionary.Add("HardStone", new TileFlyweight(Resources.Load<Sprite>("Sprites/HardStone")));
    }

    public Tile Create(string _key)
    {
        if (FactoryDictionary.ContainsKey(_key))
        {
            return new Tile(FactoryDictionary[_key].Sprite, Parent.transform);
        }
        else
        {
            Debug.LogError("TileFactory Create: key " + _key + " doesn't exist");
            return null;
        }
    }
}
