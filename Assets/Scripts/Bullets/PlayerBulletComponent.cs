using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletComponent : BulletComponent
{
  float currVelocity;

  private void Start()
  {
    currVelocity = stats.velocity;
  }
  // Update is called once per frame
  void Update()
  {
    Vector2 newPos = stats.position;
    Vector2 change = stats.direction * (currVelocity * Time.deltaTime);

    newPos += change;
    stats.position = newPos;
    transform.position = newPos;

    currVelocity += (stats.acceleration * Time.deltaTime);

    CheckOutOfBounds();
  }

  void CheckOutOfBounds()
  {
    //Increasing X boundaries on bullets to be more generous around edges
    if (transform.position.x < -10 || transform.position.x > 10 ||
        transform.position.y < -10 || transform.position.y > 10)
    {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "EnemyShip")
    {
      GameObject enemy = collision.gameObject;
      Ship enemyShip = enemy.GetComponent<Ship>();

      enemyShip.TakeDamage(stats.damage);

      Destroy(gameObject);
    }
  }
}
