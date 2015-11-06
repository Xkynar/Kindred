using UnityEngine;
using System.Collections;

public class EmberFireKnuckle : BaseAttack
{

    public override void Init()
    {
        attackName = "Fire Knuckle";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
