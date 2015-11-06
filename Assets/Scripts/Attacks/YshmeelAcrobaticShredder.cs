using UnityEngine;
using System.Collections;

public class YshmeelAcrobaticShredder : BaseAttack
{

    public override void Init()
    {
        attackName = "Acrobatic Shredder";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
