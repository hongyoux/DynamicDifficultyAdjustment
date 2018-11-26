using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {
    public float cooldown;
    private float currCooldown;
    public GameObject bullet;
    public Transform[] spawnPoints;

    // Update is called once per frame
    void Update () {
        currCooldown = Mathf.Clamp(currCooldown - Time.deltaTime, 0f, float.MaxValue);
    }

    public void Fire(int level)
    {
        if (currCooldown == 0)
        {
            // Can fire
            switch (level)
            {
                case 1:
                    {
                        SpawnBullet(spawnPoints[0]);
                        currCooldown = cooldown;
                        break;
                    }
                case 2:
                    {
                        SpawnBullet(spawnPoints[1]);
                        SpawnBullet(spawnPoints[2]);
                        currCooldown = cooldown;
                        break;
                    }
                case 3:
                    {
                        SpawnBullet(spawnPoints[0]);
                        SpawnBullet(spawnPoints[1]);
                        SpawnBullet(spawnPoints[2]);
                        currCooldown = cooldown;
                        break;
                    }
                case 4:
                    {
                        SpawnBullet(spawnPoints[1]);
                        SpawnBullet(spawnPoints[2]);
                        SpawnBullet(spawnPoints[3]);
                        SpawnBullet(spawnPoints[4]);
                        currCooldown = cooldown;
                        break;
                    }
                case 5:
                    {
                        SpawnBullet(spawnPoints[0]);
                        SpawnBullet(spawnPoints[1]);
                        SpawnBullet(spawnPoints[2]);
                        SpawnBullet(spawnPoints[3]);
                        SpawnBullet(spawnPoints[4]);
                        currCooldown = cooldown;
                        break;
                    }
            }
        }
    }

    private void SpawnBullet(Transform spawnPoint)
    {
        GameObject g = Instantiate(bullet, spawnPoint);
        BulletComponent b = g.GetComponent<BulletComponent>();
        b.stats.position = spawnPoint.position;
    }
}
