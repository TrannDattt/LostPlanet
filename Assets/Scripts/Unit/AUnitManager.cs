using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUnitManager : MonoBehaviour
{
    private StateMachine stateMachine;
    public AnimState State => stateMachine.state;

    public void SetState(AnimState _state, bool forceReset = false)
    {
        stateMachine.SetState(_state, forceReset);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
