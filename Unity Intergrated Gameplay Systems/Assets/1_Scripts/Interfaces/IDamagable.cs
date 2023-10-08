using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int Health { get; set; }
    int MaxHealth { get; set; }

    void TakeDamage(int amount);
    void OnDie();
}
