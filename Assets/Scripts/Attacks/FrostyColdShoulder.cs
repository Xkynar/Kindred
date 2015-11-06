using UnityEngine;
using System.Collections;

public class FrostyColdShoulder : BaseAttack
{

    public override void Init()
    {
        attackName = "Cold Shoulder";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
