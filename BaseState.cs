using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public abstract class BaseState<T1, T2>
{
    public T2 label { get; private set; }

    //list of protected member states

    protected BaseState(T2 label)
    {
        this.label = label;
    }

    protected void GetComponents()
    {

    }

    public abstract void EnterState(T1 entity);
    public abstract void UpdateState(T1 entity);
    public abstract void UpdateStateFixed(T1 entity);
    public abstract void ExitState(T1 entity);
}
