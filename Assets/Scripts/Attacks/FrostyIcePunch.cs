using UnityEngine;
using System.Collections;

public class FrostyIcePunch : BaseAttack
{

    public override void Init()
    {
        attackName = "Ice Punch";
        runningSpeed = 2;
        damage = 17f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
