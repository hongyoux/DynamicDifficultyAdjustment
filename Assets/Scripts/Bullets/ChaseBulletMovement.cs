using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletComponent))]
public class ChaseBulletMovement : MonoBehaviour
{
  BulletComponent b;
  bool stopped;

  private void Awake()
  {
    b = GetComponent<BulletComponent>();
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
    float acceleratedAmount = b.stats.acceleration * Time.deltaTime;
    Vector3 change = b.stats.direction * ((b.stats.velocity + b.stats.acceleration) * Time.deltaTime);
    currentPos += change;

    b.stats.position = currentPos;
    transform.position = currentPos;

    CheckOutOfBounds();
  }

  public void SetTarget(Vector3 pos)
  {
    Vector2 direction = pos - transform.position;
    b.stats.direction = direction.normalized;
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
