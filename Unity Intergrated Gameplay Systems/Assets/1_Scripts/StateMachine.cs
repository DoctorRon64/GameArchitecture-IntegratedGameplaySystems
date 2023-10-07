using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public IState<T> currentState;

    private Dictionary<string, IState<T>> statesDict = new Dictionary<string, IState<T>>();
    public T Owner { get; private set; }

    public StateMachine(T _owner)
    {
        Owner = _owner;
    }

    public void AddState(string _stateType, IState<T> _stateInstance)
    {
        if (!CheckIfContainsState(_stateInstance))
        {
            statesDict.Add(_stateType, _stateInstance);
            _stateInstance.SetOwner(Owner);
        }
    }

    public void SwitchState(string _newState)
    {
        if (CheckIfContainsState(_newState))
        {
            if (currentState != statesDict[_newState])
            {
                currentState?.OnExit();
                currentState = statesDict[_newState];
                currentState?.OnStart();
            }
        }
    }

    public bool CheckIfContainsState(IState<T> _state)
    {
        if (statesDict.ContainsValue(_state))
        {
            return true;
        }
        return false;
    }

    public bool CheckIfContainsState(string _state)
    {
        if (statesDict.ContainsKey(_state))
        {
            return true;
        }
        Debug.LogError("Couldn't find " + _state + " in " + this.ToString());
        return false;
    }

    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }
}