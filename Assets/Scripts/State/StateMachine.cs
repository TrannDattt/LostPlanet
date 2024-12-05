using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public AnimState state;

    public IEnumerator SetState(AnimState _state, bool forceChange = false)
    {
        if (state == null || (_state != state && state.status == EStateStatus.Finish && _state.status == EStateStatus.Ready) || forceChange)
        {
            if(state != null)
            {
                if(forceChange)
                {
                    state.status = EStateStatus.Finish;
                }

                state.ExitState();
                yield return new WaitForSeconds(state.transitionTime);
            }
            else
            {
                //Debug.Log("null");
            }

            state = _state;
            state.InitialState();
            state.EnterState();
        }

        yield return null;
    }

    public List<AnimState> GetActiveStatesBranch(List<AnimState> states = null)
    {
        if (states == null)
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
            return state.StateMachine.GetActiveStatesBranch(states);
        }
    }
}
