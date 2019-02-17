using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickupMovement : BulletComponent
{
  float currVelocity;
  float maxVelocity;

  private void Start()
  {
    currVelocity = stats.velocity;
    maxVelocity = -2f;
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

    currVelocity = Mathf.Max(maxVelocity, currVelocity); //Cap velocity at -2.

    CheckOutOfBounds();
  }

  void CheckOutOfBounds()
  {
    // Only destroy object if it falls beyond pickup range
    if (transform.position.y < -10)
    {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "PlayerShip")
    {
      Gamemaster.Instance.UpdatePlayerScore(10);
      Destroy(gameObject);
    }
  }
}
