using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyWeapon : WeaponComponent
{
  Vector3 targetPos;

  private void Start()
  {
    targetPos = new Vector3(0, -10, 1);
  }

  public override void Fire(int level)
  {
    if (!CanFire())
    {
      return;
    }
    CoolingDown();

    GameObject b = SpawnBullet(spawnPoints[0]);
    if (b != null)
    {
      ChaseBulletMovement cbm = b.GetComponent<ChaseBulletMovement>();
      Player p = Gamemaster.Instance.GetPlayer();
      if (p != null)
      {
        targetPos = p.transform.position;
      }
      cbm.SetTarget(targetPos);
    }
  }
}
