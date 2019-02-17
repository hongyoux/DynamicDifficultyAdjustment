using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepEnemyWeapon : WeaponComponent
{
  private float fireStart;
  private float intBtwn;
  private int fireNum;
  private int count;
  private bool active;

  private void Start()
  {
    fireNum = 5;
    intBtwn = .1f;
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
      SpawnBullet(spawnPoints[0]);
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

  
}