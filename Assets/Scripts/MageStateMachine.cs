using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;

public class MageStateMachine : SimpleStateMachine
{
    public IdleState idleState;
    public WandTimeState wandTimeState;
    void Awake()
    {
        states.Add(idleState);
        states.Add(wandTimeState);

        //ChangeState(nameof(idleState));
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
