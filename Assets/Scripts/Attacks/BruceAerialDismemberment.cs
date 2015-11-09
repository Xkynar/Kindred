using UnityEngine;
using System.Collections;

public class BruceAerialDismemberment : BaseAttack
{

    public override void Init()
    {
        attackName = "Aerial Dismemberment";
        runningSpeed = 1;
        damage = 22f;
        manaCost = 12f;
        attackDistance = 1f;
    }
}
