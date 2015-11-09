using UnityEngine;
using System.Collections;

public class BonesHellRaiser : BaseAttack
{
    public override void Init()
    {
        attackName = "Hell Raiser";
        runningSpeed = 2;
        damage = 17f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
