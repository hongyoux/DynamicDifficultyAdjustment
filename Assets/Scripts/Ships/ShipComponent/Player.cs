using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ship
{
  // Update is called once per frame
  void Update()
  {
    if (stats.currHealth <= 0)
    {
      if (stats.lives > 1)
      {
        stats.lives -= 1;
        stats.currHealth = stats.maxHealth;
      }
      else
      {
        Destroy();

        // Quit right after
        Application.Quit();
      }
    }
  }
  public override void TakeDamage(int damage) 
  {
    gm.LogDamage(stats.currHealth);
    base.TakeDamage(damage);
  }
}
