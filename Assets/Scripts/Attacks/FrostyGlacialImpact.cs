using UnityEngine;
using System.Collections;

public class FrostyGlacialImpact : BaseAttack
{

    public override void Init()
    {
        attackName = "Glacial Impact";
        runningSpeed = 1;
        damage = 60f;
        manaCost = 20f;
        attackDistance = 1f;
    }
}
