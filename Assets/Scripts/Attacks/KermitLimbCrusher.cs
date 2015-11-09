using UnityEngine;
using System.Collections;

public class KermitLimbCrusher : BaseAttack
{

    public override void Init()
    {
        attackName = "Limb Crusher";
        runningSpeed = 2;
        damage = 20f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
