using UnityEngine;
using System.Collections;

public class BonesPeaceBreaker : BaseAttack {

    public override void Init()
    {
        attackName = "Peace Breaker";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
