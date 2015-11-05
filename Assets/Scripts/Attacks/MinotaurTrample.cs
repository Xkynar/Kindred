using UnityEngine;
using System.Collections;

public class MinotaurTrample : BaseAttack
{

    protected override void Init()
    {
        attackName = "Trample";
        runningSpeed = 1;
    }
}
