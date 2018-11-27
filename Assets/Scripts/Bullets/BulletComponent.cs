using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour {
    public BulletStats stats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyShip")
        {
            GameObject enemy = collision.gameObject;
            Ship enemyShip = enemy.GetComponent<Ship>();
            enemyShip.stats.health -= stats.damage;

            if (enemyShip.stats.health <= 0)
            {
                enemyShip.Destroy();
            }

            Destroy(gameObject);
        }
    }
}
