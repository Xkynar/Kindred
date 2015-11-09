using UnityEngine;
using System.Collections;

public class EmberFireKnuckle : BaseAttack
{

    public override void Init()
    {
        attackName = "Fire Knuckle";
        runningSpeed = 2;
        damage = 17f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
