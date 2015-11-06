using UnityEngine;
using System.Collections;

public class BruceDoubleCutter : BaseAttack
{

    public override void Init()
    {
        attackName = "Double Cutter";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
