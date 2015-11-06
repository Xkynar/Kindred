using UnityEngine;
using System.Collections;

public class BonesHellRaiser : BaseAttack
{
    public override void Init()
    {
        attackName = "Hell Raiser";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
