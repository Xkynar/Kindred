using UnityEngine;
using System.Collections;

public class BruceAerialDismemberment : BaseAttack
{

    public override void Init()
    {
        attackName = "Aerial Dismemberment";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
