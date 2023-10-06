using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFlyweight 
{
    public Sprite Sprite { get; private set; }
    public int MaxHealth { get; private set; }

    public TileFlyweight(Sprite _sprite, int _maxHealth)
    {
        Sprite = _sprite;
        MaxHealth = _maxHealth;
    }
}
