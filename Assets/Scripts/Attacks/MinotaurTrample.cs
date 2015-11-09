using UnityEngine;
using System.Collections;

public class MinotaurTrample : BaseAttack
{

    public override void Init()
    {
        attackName = "Trample";
        runningSpeed = 2;
        damage = 25f;
        manaCost = 11f;
        attackDistance = 1f;
    }
}
