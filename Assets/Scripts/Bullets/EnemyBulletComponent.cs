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

      Destroy(gameObject);
    }
  }
}
