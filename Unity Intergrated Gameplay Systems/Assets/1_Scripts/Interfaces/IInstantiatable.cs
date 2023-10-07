using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInstantiatable
{
    public GameObject Instance { get; set; }

    public void Instantiate(GameObject _prefab, Transform _parent);
}
