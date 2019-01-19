using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipStats
{
  [HideInInspector]
  public int currHealth;

  public string name;
  public int maxHealth;
  public int lives;
  public Vector2 position;
  public float movespeed;
  public int score;
  public int powerLevel;
}
