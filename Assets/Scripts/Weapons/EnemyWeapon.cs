using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : WeaponComponent
{
    public override void Fire(int level)
    {
        if (currCooldown == 0)
        {
            SpawnBullet(spawnPoints[0]);
            currCooldown = cooldown;
        }
    }
}