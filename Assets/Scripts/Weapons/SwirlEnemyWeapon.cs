using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlEnemyWeapon : WeaponComponent
{
  private float fireStart;
  private float intBtwn;
  private int fireNum;
  private int count;
  private bool active;
  protected override void Init()
  {
    fireNum = 5;
    intBtwn = .2f;
    count = 0;
  }

  private void Update()
  {
    if (!active)
    {
      return;
    }
    if (count == fireNum)
    {
      active = false;
      count = 0;
      return;
    }
    if (Time.time >= intBtwn * count + fireStart)
    {
      fireBullet();
      count++;
    }

  }

  public override void Fire(int level)
  {
    if (!CanFire())
    {
      return;
    }
    CoolingDown();

    fireStart = Time.time;
    active = true;
  }

  private void fireBullet()
  {
    SpawnBullet(spawnPoints[0]);
    GameObject rightBullet = SpawnBullet(spawnPoints[1]);
    if (rightBullet != null)
    {
      SwirlBulletMovement sbm = rightBullet.GetComponent<SwirlBulletMovement>();
      sbm.width *= -1;
    }
  }
}
