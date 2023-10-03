using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBaseState 
{
    public abstract void EnterState(BulletStateManager bullet);
    public abstract void UpdateState(BulletStateManager bullet);
    public abstract void OnTriggerState(BulletStateManager bullet, Collider other);
    public abstract void ExitState(BulletStateManager bullet);
}
