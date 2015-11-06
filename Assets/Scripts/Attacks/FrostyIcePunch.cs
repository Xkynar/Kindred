using UnityEngine;
using System.Collections;

public class FrostyIcePunch : BaseAttack
{

    public override void Init()
    {
        attackName = "Ice Punch";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
