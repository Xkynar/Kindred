using UnityEngine;
using System.Collections;

public class MinotaurPulverize : BaseAttack
{

    public override void Init()
    {
        attackName = "Pulverize";
        runningSpeed = 1;
        damage = 30f;
        manaCost = 13f;
        attackDistance = 1f;
    }
}
