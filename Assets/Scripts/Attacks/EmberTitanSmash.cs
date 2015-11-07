using UnityEngine;
using System.Collections;

public class EmberTitanSmash : BaseAttack
{

    public override void Init()
    {
        attackName = "Titan Smash";
        runningSpeed = 1;
        damage = 40f;
        manaCost = 12f;
        attackDistance = 1f;
    }
}
