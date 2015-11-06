using UnityEngine;
using System.Collections;

public class MinotaurPulverize : BaseAttack
{

    public override void Init()
    {
        attackName = "Pulverize";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
