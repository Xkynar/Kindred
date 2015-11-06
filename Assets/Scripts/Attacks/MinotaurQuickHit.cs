using UnityEngine;
using System.Collections;

public class MinotaurQuickHit : BaseAttack
{

    public override void Init()
    {
        attackName = "Quick Hit";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
