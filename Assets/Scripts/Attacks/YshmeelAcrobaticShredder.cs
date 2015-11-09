using UnityEngine;
using System.Collections;

public class YshmeelAcrobaticShredder : BaseAttack
{

    public override void Init()
    {
        attackName = "Acrobatic Shredder";
        runningSpeed = 1;
        damage = 115f;
        manaCost = 17f;
        attackDistance = 1f;
    }
}
