using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletComponent))]
public class SwirlBulletMovement : MonoBehaviour
{
  public float freq;
  public float width;

  float additionalX;
  BulletComponent b;

  float currTime;

  private void Start()
  {
    b = GetComponent<BulletComponent>();

    additionalX = 0f;
    currTime = 0f;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 newPos = b.stats.position;
    Vector3 change = b.stats.direction * (b.stats.velocity * Time.deltaTime);
    additionalX = width * Mathf.Sin(currTime * freq);

    newPos += change;
    b.stats.position = newPos;

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
