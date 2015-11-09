using UnityEngine;
using System.Collections;

public class KermitWindmill : BaseAttack
{

    public override void Init()
    {
        attackName = "Windmill";
        runningSpeed = 1;
        damage = 30f;
        manaCost = 12f;
        attackDistance = 1f;
    }
}
