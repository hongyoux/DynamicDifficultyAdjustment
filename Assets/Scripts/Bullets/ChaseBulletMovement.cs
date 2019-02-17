using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBulletMovement : EnemyBulletComponent
{
  bool stopped;

  private void Awake()
  {
    stopped = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (stopped)
    {
      return;
    }
    Vector3 currentPos = transform.position;
    float acceleratedAmount = stats.acceleration * Time.deltaTime;
    Vector3 change = stats.direction * (stats.velocity * Time.deltaTime);
    currentPos += change;

    stats.position = currentPos;
    transform.position = currentPos;

    CheckOutOfBounds();
  }

  public void SetTarget(Vector3 pos)
  {
    Vector2 direction = pos - transform.position;
    stats.direction = direction.normalized;
    stopped = false;
  }

  void CheckOutOfBounds()
  {
    if (transform.position.x < -5 || transform.position.x > 5 ||
        transform.position.y < -10 || transform.position.y > 10)
    {
      Destroy(gameObject);
    }
  }

  public void StopTargetting()
  {
    stopped = true;
  }
}
