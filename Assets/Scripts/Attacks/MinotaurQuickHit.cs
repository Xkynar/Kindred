using UnityEngine;
using System.Collections;

public class MinotaurQuickHit : BaseAttack
{

    protected override void Init()
    {
        attackName = "Quick Hit";
        runningSpeed = 1;
    }
}
