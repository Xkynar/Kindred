using UnityEngine;
using System.Collections;

public class BonesRageCataclysm : BaseAttack {

    public override void Init()
    {
        attackName = "Rage Cataclysm";
        runningSpeed = 1;
        damage = 22f;
        manaCost = 12f;
        attackDistance = 1f;
    }
}
