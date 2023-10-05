using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : IFactory<Tile, TileFlyweight>
{
    public Dictionary<string, TileFlyweight> Dictionary { get; set; }
    
    private GameObject parent;

    public TileFactory()
    {
        //Dictionary
        Dictionary = new Dictionary<string, TileFlyweight>();
        InitializeDictionary();

        //Parent
        parent = new GameObject();
        parent.name = "Tiles";
    }

    public void InitializeDictionary()
    {
        Dictionary.Add("Dirt", new TileFlyweight(Resources.Load<Sprite>("Sprites/Dirt")));
        Dictionary.Add("Stone", new TileFlyweight(Resources.Load<Sprite>("Sprites/Stone")));
        Dictionary.Add("HardStone", new TileFlyweight(Resources.Load<Sprite>("Sprites/HardStone")));
    }

    public Tile Create(string _key)
    {
        if (Dictionary.ContainsKey(_key))
        {
            return new Tile(Dictionary[_key].Sprite, parent.transform);
        }
        else
        {
            Debug.LogError("TileFactory Create: key " + _key + " doesn't exist");
            return null;
        }
    }

}
