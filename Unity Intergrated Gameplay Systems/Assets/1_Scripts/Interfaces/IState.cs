using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    T StateOwner { get; set; }

    void SetOwner(T _stateOwner);
    void OnStart();
    void OnUpdate();
    void OnExit();
}
