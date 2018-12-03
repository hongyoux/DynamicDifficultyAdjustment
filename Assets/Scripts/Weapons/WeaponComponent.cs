using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour {
    public float cooldown;
    protected float nextFire;
    public GameObject bullet;
    public Transform[] spawnPoints;

    // Use this for initialization
    void Start () {
        nextFire = 0;
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

    protected void SpawnBullet(Transform spawnPoint)
    {
        GameObject g = Instantiate<GameObject>(bullet);
        g.transform.position = spawnPoint.position;

        BulletComponent b = g.GetComponent<BulletComponent>();
        b.stats.position = spawnPoint.position;
    }
}
