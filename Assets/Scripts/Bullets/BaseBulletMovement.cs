﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletComponent))]
public class BaseBulletMovement : MonoBehaviour
{
  BulletComponent b;
  private void Start()
  {
    b = GetComponent<BulletComponent>();
  }
  // Update is called once per frame
  void Update()
  {
    Vector2 newPos = b.stats.position;
    Vector2 change = b.stats.direction * (b.stats.velocity * Time.deltaTime);

    newPos += change;
    b.stats.position = newPos;
    transform.position = newPos;

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
