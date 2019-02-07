using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
  public float timeBetweenSpawns;

  public ShipType type;

  // Update is called once per frame
  void Update()
  {
    if (stats.currHealth <= 0)
    {
      Destroy();
    }
  }

  protected override void Destroy()
  {
    Logger.Instance.LogKill(stats.name);
    Gamemaster.Instance.UpdatePlayerScore(stats.score);
    base.Destroy();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "PlayerShip")
    {
      GameObject player = collision.gameObject;
      Ship playerShip = player.GetComponent<Ship>();
      playerShip.TakeDamage(2);
    }
  }
}
