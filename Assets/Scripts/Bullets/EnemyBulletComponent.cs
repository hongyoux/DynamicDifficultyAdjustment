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
      playerShip.TakeDamage(stats.damage);

      switch (stats.type)
      {
        case ShipType.BASIC:
          Gamemaster.Instance.damageDealtByBullets[0]++;
          break;
        case ShipType.CHASE:
          Gamemaster.Instance.damageDealtByBullets[1]++;
          break;
        case ShipType.SWIRL:
          Gamemaster.Instance.damageDealtByBullets[2] += 2;
          break;
        case ShipType.SWEEP:
          Gamemaster.Instance.damageDealtByBullets[3]++;
          break;
      }
      Gamemaster.Instance.PlayerHitReward();

      Destroy(gameObject);
    }
  }
}
