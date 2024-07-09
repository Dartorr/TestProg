using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFSM : MonoBehaviour
{
    public AState currentState = null;

    public void ChangeState(AState newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public abstract class AState
    {
        public virtual void Enter() { }
        public virtual void Exit() { }
    }

}
