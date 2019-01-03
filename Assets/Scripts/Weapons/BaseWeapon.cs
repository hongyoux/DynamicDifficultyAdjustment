using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : WeaponComponent
{
  public override void Fire(int level)
  {
    if (!CanFire())
    {
      return;
    }
    CoolingDown();

    // Can fire
    switch (level)
    {
      case 1:
        {
          SpawnBullet(spawnPoints[0]);
          break;
        }
      case 2:
        {
          SpawnBullet(spawnPoints[1]);
          SpawnBullet(spawnPoints[2]);
          break;
        }
      case 3:
        {
          SpawnBullet(spawnPoints[0]);
          SpawnBullet(spawnPoints[1]);
          SpawnBullet(spawnPoints[2]);
          break;
        }
      case 4:
        {
          SpawnBullet(spawnPoints[1]);
          SpawnBullet(spawnPoints[2]);
          SpawnBullet(spawnPoints[3]);
          SpawnBullet(spawnPoints[4]);
          break;
        }
      case 5:
        {
          SpawnBullet(spawnPoints[0]);
          SpawnBullet(spawnPoints[1]);
          SpawnBullet(spawnPoints[2]);
          SpawnBullet(spawnPoints[3]);
          SpawnBullet(spawnPoints[4]);
          break;
        }
    }
  }
}
