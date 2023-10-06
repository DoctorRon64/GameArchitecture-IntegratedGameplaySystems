using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICollidable
{
    public Collider2D collider { get; set; }
    public Action<Collider2D> OnCollision { get; set; }
    public void CheckCollisions();
}
