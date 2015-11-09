using UnityEngine;
using System.Collections;

public class EmberTitanSmash : BaseAttack
{

    public override void Init()
    {
        attackName = "Titan Smash";
        runningSpeed = 2;
        damage = 40f;
        manaCost = 15f;
        attackDistance = 1f;
    }
}
