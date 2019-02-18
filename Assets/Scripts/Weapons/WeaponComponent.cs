using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
  public float cooldown;
  protected float nextFire;
  public GameObject bullet;
  public Transform[] spawnPoints;

  // Use this for initialization
  void Start()
  {
    Init();
  }

  virtual protected void Init() { }

  public void SetInitialFireTime(float x)
  {
    nextFire = Time.time + x;
  }

  public void Fire() { Fire(0); }
  virtual public void Fire(int level)
  {

  }

  public bool CanFire()
  {
    return Time.time > nextFire;
  }
  
  public void CoolingDown()
  {
    nextFire = Time.time + cooldown;
  }

  protected GameObject SpawnBullet(Transform spawnPoint)
  {
    if (Gamemaster.bullets != null)
    {
      GameObject g = Instantiate<GameObject>(bullet, Gamemaster.bullets.transform);
      g.transform.position = spawnPoint.position;

      BulletComponent b = g.GetComponent<BulletComponent>();
      b.stats.position = spawnPoint.position;

      return g;
    }
    return null;
  }
}
