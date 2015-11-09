using UnityEngine;
using System.Collections;

public class BonesPeaceBreaker : BaseAttack {

    public override void Init()
    {
        attackName = "Peace Breaker";
        runningSpeed = 2;
        damage = 10f;
        manaCost = 5f;
        attackDistance = 1f;
    }
}
