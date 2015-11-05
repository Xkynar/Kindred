using UnityEngine;
using System.Collections;

public class BruceLunge : BaseAttack
{

    protected override void Init()
    {
        attackName = "Lunge";
        runningSpeed = 2;
    }

    protected override void BeforeAttack()
    {
        Debug.Log("Before Attack");
    }

    protected override void AfterAttack()
    {
        Debug.Log("After Attack");
    }

}
