using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyWeapon : WeaponComponent
{
  public override void Fire(int level)
  {
    if (!CanFire())
    {
      return;
    }
    CoolingDown();

    SpawnBullet(spawnPoints[0]);
  }
}