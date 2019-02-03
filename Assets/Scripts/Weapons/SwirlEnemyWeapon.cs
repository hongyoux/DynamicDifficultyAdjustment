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

    SpawnBullet(spawnPoints[0]);
    GameObject rightBullet = SpawnBullet(spawnPoints[1]);
    if (rightBullet != null)
    {
      SwirlBulletMovement sbm = rightBullet.GetComponent<SwirlBulletMovement>();
      sbm.width *= -1;
    }
  }
}
