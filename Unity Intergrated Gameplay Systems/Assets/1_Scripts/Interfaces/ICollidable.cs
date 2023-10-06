using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICollidable<T>
{
    public Collider2D collider { get; set; }
    public Action<Collider2D, T> OnCollision { get; set; }
    public void CheckCollisions();
}
