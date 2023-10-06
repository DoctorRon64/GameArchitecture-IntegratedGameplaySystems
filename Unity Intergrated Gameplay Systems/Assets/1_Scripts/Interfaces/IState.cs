using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    StateMachine StateOwner { get; set; }

    void SetOwner(StateMachine stateOwner);
    void OnStart();
    void OnUpdate();
    void OnExit();
}
