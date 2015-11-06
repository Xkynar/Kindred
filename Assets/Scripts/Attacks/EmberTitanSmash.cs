using UnityEngine;
using System.Collections;

public class EmberTitanSmash : BaseAttack
{

    public override void Init()
    {
        attackName = "Titan Smash";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
