using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ship
{
  private PlayerAgent pa;

  private void Start()
  {
    pa = GetComponent<PlayerAgent>();
  }

  // Update is called once per frame
  void Update()
  {
    if (stats.lives >= 1)
    {
      if (stats.currHealth <= 0)
      {
        stats.lives -= 1;
        stats.currHealth = stats.maxHealth;
      }
    }
    else
    {
      GetComponent<Renderer>().enabled = false;
      Gamemaster.Instance.Stop();

      pa.Done();
    }
  }

  public override void TakeDamage(int damage) 
  {
    Logger.Instance.LogDamage(stats.currHealth);
    pa.SetReward(-.00005f * damage);
    base.TakeDamage(damage);
  }
}
