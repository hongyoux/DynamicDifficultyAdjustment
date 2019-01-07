using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlEnemyWeapon : WeaponComponent
{

  public override void Fire(int level)
  {
    if (!CanFire())
    {
      return;
    }
    CoolingDown();

    GameObject leftBullet = SpawnBullet(spawnPoints[0]);
    GameObject rightBullet = SpawnBullet(spawnPoints[1]);
    SwirlBulletMovement sbm = rightBullet.GetComponent<SwirlBulletMovement>();
    sbm.width *= -1;
  }
}
