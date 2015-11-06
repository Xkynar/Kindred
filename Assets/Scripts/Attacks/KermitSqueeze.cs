using UnityEngine;
using System.Collections;

public class KermitSqueeze : BaseAttack
{

    public override void Init()
    {
        attackName = "Squeeze";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
