using UnityEngine;
using System.Collections;

public class BruceDoubleCutter : BaseAttack
{

    public override void Init()
    {
        attackName = "Double Cutter";
        runningSpeed = 2;
        damage = 19f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
