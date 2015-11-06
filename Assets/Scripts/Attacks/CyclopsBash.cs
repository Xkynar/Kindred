using UnityEngine;
using System.Collections;

public class CyclopsBash : BaseAttack
{

    public override void Init()
    {
        attackName = "Bash";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
