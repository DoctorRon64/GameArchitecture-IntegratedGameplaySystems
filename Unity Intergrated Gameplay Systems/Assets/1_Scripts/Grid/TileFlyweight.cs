using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I dont even know if you can call this a flyweight
public class TileFlyweight 
{
    public GameObject Prefab { get; private set; }
    public int MaxHealth { get; private set; }

    public TileFlyweight(GameObject _prefab, int _maxHealth)
    {
        Prefab = _prefab;
        MaxHealth = _maxHealth;
    }
}
