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

      int index = -1;
      switch(stats.type)
      {
        case ShipType.BASIC:
          index = 0;
          break;
        case ShipType.CHASE:
          index = 1;
          break;
        case ShipType.SWIRL:
          index = 2;
          break;
        case ShipType.SWEEP:
          index = 3;
          break;
      }

      if (index == -1)
      {
        // Didn't identify bullet, wut
        Debug.Log("DIDNT FIND THE BULLET TYPE?!");
      }
      else
      {
        Gamemaster.Instance.damageDealtByBullets[index]++;
      }

      Gamemaster.Instance.PlayerHitReward();

      Destroy(gameObject);
    }
  }
}
