using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class EnemyInputComponent : MonoBehaviour
{
  private Ship ship;
  private int swayLength = 1;
  private Vector2 currentDirection;
  private float currCooldown;

  private void Start()
  {
    currentDirection = new Vector2(-1, 0);
    ship = GetComponent<Ship>();
    currCooldown = swayLength;
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 newPos = ship.stats.position;
    Vector2 deltaMove = Time.deltaTime * ship.stats.movespeed * currentDirection;

    newPos += deltaMove;

    ship.stats.position = newPos;
    transform.position = newPos;

    currCooldown -= Time.deltaTime;
    if (currCooldown < 0)
    {
      currentDirection *= -1;
      currCooldown = swayLength * 2;
    }
  }
}
