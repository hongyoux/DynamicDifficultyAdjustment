using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlBulletMovement : EnemyBulletComponent
{
  public float freq;
  public float width;

  float additionalX;

  float currTime;

  private void Start()
  {
    additionalX = 0f;
    currTime = 0f;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 newPos = stats.position;
    Vector3 change = stats.direction * (stats.velocity * Time.deltaTime);
    additionalX = width * Mathf.Sin(currTime * freq);

    newPos += change;
    stats.position = newPos;

    newPos.x += additionalX;
    transform.position = newPos;

    currTime += Time.deltaTime;

    CheckOutOfBounds();
  }


  void CheckOutOfBounds()
  {
    if (transform.position.x < -5 || transform.position.x > 5 ||
        transform.position.y < -10 || transform.position.y > 10)
    {
      Destroy(gameObject);
    }
  }
}
