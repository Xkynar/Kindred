using UnityEngine;
using System.Collections;

public class KermitWindmill : BaseAttack
{

    public override void Init()
    {
        attackName = "Windmill";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
