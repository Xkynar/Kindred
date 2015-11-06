using UnityEngine;
using System.Collections;

public class UndeadSlash : BaseAttack
{

    public override void Init()
    {
        attackName = "Slash";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
