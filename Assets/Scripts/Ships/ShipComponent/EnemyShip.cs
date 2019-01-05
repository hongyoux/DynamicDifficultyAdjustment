using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
  // Update is called once per frame
  void Update()
  {
    if (stats.currHealth <= 0)
    {
      Destroy();
    }
  }

  protected override void Destroy()
  {
    gm.UpdatePlayerScore(stats.score);
    base.Destroy();
  }
}
