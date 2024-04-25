using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using Unity.VisualScripting;

[System.Serializable]
public class HammerTimeState : SimpleState
{
    public GameObject hammer;
    public GameObject wand;
    public GameObject cross;
    private GameObject gameObject;
    private Vector3 targetRotation;
    private Vector3 beginningRotation;
    private float time;
    public override void OnStart()
    {
        time = 0.0f;
        gameObject = stateMachine.gameObject;

        if (hammer == false)
            return;
        
        hammer.GetComponent<Animator>().Play("Hammer");

        if (wand == false)
            return;
        
        wand.GetComponent<Animator>().Play("Wand");

        if (cross == false)
            return;
        
        cross.GetComponent<Animator>().Play("Healer");
    }

    public override void UpdateState(float _dt)
    {
        if (hammer == false)
            return;

        if (wand == false)
            return;

        if (cross == false)
            return;
        
        time += _dt;

        if (time > 1.0f)
            stateMachine.ChangeState("ChaseState");
    }

    public override void OnExit()
    {

    }
}
