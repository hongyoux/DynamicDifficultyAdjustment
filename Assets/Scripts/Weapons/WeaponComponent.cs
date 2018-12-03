using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour {
    public float cooldown;
    protected float currCooldown;
    public GameObject bullet;
    public Transform[] spawnPoints;

    // Use this for initialization
    void Start () {
        currCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
        currCooldown = Mathf.Clamp(currCooldown - Time.deltaTime, 0f, float.MaxValue);
    }

    public void Fire() { Fire(0); }
    virtual public void Fire(int level)
    {

    }

    protected void SpawnBullet(Transform spawnPoint)
    {
        GameObject g = Instantiate(bullet, spawnPoint);
        BulletComponent b = g.GetComponent<BulletComponent>();
        b.stats.position = spawnPoint.position;
    }
}
