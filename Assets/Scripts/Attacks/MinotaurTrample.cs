using UnityEngine;
using System.Collections;

public class MinotaurTrample : BaseAttack
{

    public override void Init()
    {
        attackName = "Trample";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
