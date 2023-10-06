using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState currentState;

    private Dictionary<System.Type, IState> statesDict = new Dictionary<System.Type, IState>();

    public void AddStates(System.Type startingState, IState[] states)
    {
        foreach (IState state in states)
        {
            statesDict.Add(state.GetType(), state);
        }

        SwitchState(startingState);
    }

    public void SwitchState(System.Type newState)
    {
        if (currentState != statesDict[newState])
        {
            currentState?.OnExit();
            currentState = statesDict[newState];
            currentState?.OnStart();
        }
    }

    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }
}