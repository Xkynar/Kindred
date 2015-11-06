using UnityEngine;
using System.Collections;

public class KermitLimbCrusher : BaseAttack
{

    public override void Init()
    {
        attackName = "Limb Crusher";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
