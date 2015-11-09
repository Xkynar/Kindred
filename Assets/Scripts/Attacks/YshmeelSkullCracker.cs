using UnityEngine;
using System.Collections;

public class YshmeelSkullCracker : BaseAttack
{

    public override void Init()
    {
        attackName = "Skull Cracker";
        runningSpeed = 2;
        damage = 60f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
