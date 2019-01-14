using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyWeapon : WeaponComponent
{
  private Gamemaster gm;

  Vector3 targetPos;

  private void Start()
  {
    GameObject g = GameObject.Find("GameMaster");
    gm = g.GetComponent<Gamemaster>();
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
    ChaseBulletMovement cbm = b.GetComponent<ChaseBulletMovement>();
    Player p = gm.GetPlayer();
    if (p != null)
    {
      targetPos = p.transform.position;
    }
    cbm.SetTarget(targetPos);
  }
}
