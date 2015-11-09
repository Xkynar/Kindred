using UnityEngine;
using System.Collections;

public class KermitEarthquake : BaseAttack
{

    public override void Init()
    {
        attackName = "Earthquake";
        runningSpeed = 2;
        damage = 17f;
        manaCost = 6f;
        attackDistance = 1f;
    }
}
