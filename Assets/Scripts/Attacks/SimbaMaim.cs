using UnityEngine;
using System.Collections;

public class SimbaMaim : BaseAttack
{
    public override void Init()
    {
        attackName = "Maim";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
