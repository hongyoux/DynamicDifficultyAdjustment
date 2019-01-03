using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletComponent : BulletComponent
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "PlayerShip")
    {
      GameObject player = collision.gameObject;
      Ship playerShip = player.GetComponent<Ship>();
      playerShip.stats.health -= stats.damage;

      if (playerShip.stats.health <= 0)
      {
        playerShip.Destroy();
      }

      Destroy(gameObject);
    }
  }
}
