using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletMovement : EnemyBulletComponent
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
}
