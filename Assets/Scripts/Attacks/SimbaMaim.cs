using UnityEngine;
using System.Collections;

public class SimbaMaim : BaseAttack
{
    public override void Init()
    {
        attackName = "Maim";
        runningSpeed = 2;
        damage = 16f;
        manaCost = 8f;
        attackDistance = 1f;
    }
}
