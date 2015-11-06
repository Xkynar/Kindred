using UnityEngine;
using System.Collections;

public class KermitEarthquake : BaseAttack
{

    public override void Init()
    {
        attackName = "Earthquake";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
