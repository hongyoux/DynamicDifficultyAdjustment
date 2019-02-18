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

    PlayerAgent pa = Gamemaster.Instance.player.GetComponent<PlayerAgent>();
    pa.SetReward(.0001f); //Reward extra for destroying an enemy

    //Spawn a powerup here.
    SpawnScorePickup();

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

  protected void SpawnScorePickup()
  {
    GameObject pickup = Instantiate(Gamemaster.Instance.scorePickup, transform.position, transform.rotation, Gamemaster.scoreObjs.transform);
    ScorePickupMovement spm = pickup.GetComponent<ScorePickupMovement>();

    Gamemaster.Instance.totalPossiblePoints += 10; // Even if ship doesn't break, total score goes up

    spm.stats.position = transform.position;
  }
}
