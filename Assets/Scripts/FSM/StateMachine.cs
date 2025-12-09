using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    public void AddState(IState state)
    {
        states[state.GetType()] = state;
    }

    public void ChangeState<T>() where T : IState
    {
        var type = typeof(T);

        if (!states.ContainsKey(type))
            throw new Exception($"{type} State is not registered.");

        currentState?.Exit();
        currentState = states[type];
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
