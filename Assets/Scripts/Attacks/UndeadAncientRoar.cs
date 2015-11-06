using UnityEngine;
using System.Collections;

public class UndeadAncientRoar : BaseAttack
{

    public override void Init()
    {
        attackName = "Ancient Roar";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
