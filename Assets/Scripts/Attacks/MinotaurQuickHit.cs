using UnityEngine;
using System.Collections;

public class MinotaurQuickHit : BaseAttack
{

    public override void Init()
    {
        attackName = "Quick Hit";
        runningSpeed = 2;
        damage = 20f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
