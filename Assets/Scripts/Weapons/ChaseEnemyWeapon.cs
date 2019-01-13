using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyWeapon : WeaponComponent
{
  private Gamemaster gm;
  private void Start()
  {
    GameObject g = GameObject.Find("GameMaster");
    gm = g.GetComponent<Gamemaster>();
  }

  public override void Fire(int level)
  {
    if (!CanFire())
    {
      return;
    }
    CoolingDown();

    GameObject b = SpawnBullet(spawnPoints[0]);
    ChaseBulletMovement cbm = b.GetComponent<ChaseBulletMovement>();
    Vector3 playerPos = gm.GetPlayer().transform.position;
    cbm.SetTarget(playerPos);
  }
}
