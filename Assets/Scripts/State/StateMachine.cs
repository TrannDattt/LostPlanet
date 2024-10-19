using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State state;

    public void SetState(State _state, bool forceReset = false)
    {
        if((_state != state) || forceReset)
        {
            if (state)
            {
                state.ExitState();
            }

            state = _state;
            state.InitialState();
            state.EnterState();
        }
    }

    public List<State> GetActiveStatesBranch(List<State> states = null)
    {
        if(states == null)
        {
            states = new(); 
        }

        if (state == null)
        {
            return states;
        }
        else
        {
            states.Add(state);
            return state.stateMachine.GetActiveStatesBranch(states);
        }
    }
}
