using UnityEngine;
using System.Collections;

public class CyclopsSlap : BaseAttack
{

    public override void Init()
    {
        attackName = "Slap";
        runningSpeed = 2;
        damage = 17f;
        manaCost = 5f;
        attackDistance = 1f;
    }
}
