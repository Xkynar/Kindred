using UnityEngine;
using System.Collections;

public class CyclopsBash : BaseAttack
{

    public override void Init()
    {
        attackName = "Bash";
        runningSpeed = 1;
        damage = 20f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
