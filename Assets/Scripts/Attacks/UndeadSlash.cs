using UnityEngine;
using System.Collections;

public class UndeadSlash : BaseAttack
{

    public override void Init()
    {
        attackName = "Slash";
        runningSpeed = 2;
        damage = 20f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
