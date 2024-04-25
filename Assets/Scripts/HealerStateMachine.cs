using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;

public class HealerStateMachine : SimpleStateMachine
{
    public IdleState idleState;
    public ChaseState chaseState;
    public HammerTimeState hammerTimeState;
    void Awake()
    {
        states.Add(idleState);
        states.Add(chaseState);
        states.Add(hammerTimeState);

        ChangeState(nameof(idleState));
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
