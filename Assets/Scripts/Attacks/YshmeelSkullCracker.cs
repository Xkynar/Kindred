using UnityEngine;
using System.Collections;

public class YshmeelSkullCracker : BaseAttack
{

    public override void Init()
    {
        attackName = "Skull Cracker";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
