using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipStats
{
  [HideInInspector]
  public int currHealth;

  public int maxHealth;
  public int lives;
  public Vector2 position;
  public float movespeed;
  public int score;
  public Sprite sprite;
  public int powerLevel;
}
