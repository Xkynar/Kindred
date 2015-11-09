using UnityEngine;
using System.Collections;

public class KermitSqueeze : BaseAttack
{

    public override void Init()
    {
        attackName = "Squeeze";
        runningSpeed = 2;
        damage = 15f;
        manaCost = 4f;
        attackDistance = 1f;
    }
}
