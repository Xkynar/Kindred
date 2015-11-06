using UnityEngine;
using System.Collections;

public class CyclopsSlap : BaseAttack
{

    public override void Init()
    {
        attackName = "Slap";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
