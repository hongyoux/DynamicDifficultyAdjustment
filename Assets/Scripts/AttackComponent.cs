using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class AttackComponent : MonoBehaviour {
    float cooldown;

    void Start()
    {
        cooldown = 0f;
    }

    // Update is called once per frame
    void Update () {
        Player p = GetComponent<Player>();

        if (p.ShipStats.lives == 0)
        {
            return;
        }

        if (Input.GetKey("space") && cooldown == 0) {
            GameObject newBullet = Instantiate(p.tempBullet, p.spawnPoint);
            Bullet b = newBullet.GetComponent<Bullet>();
            b.stats.position = p.spawnPoint.position;

            cooldown = .2f;
        }

        cooldown = Mathf.Clamp(cooldown - Time.deltaTime, 0f, float.MaxValue);
	}
}
