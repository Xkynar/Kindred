using UnityEngine;
using System.Collections;

public class FrostyColdShoulder : BaseAttack
{

    public override void Init()
    {
        attackName = "Cold Shoulder";
        runningSpeed = 2;
        damage = 40f;
        manaCost = 15f;
        attackDistance = 1f;
    }
}
