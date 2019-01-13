using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicEnemyAttackComponent : AttackComponent
{
  WeaponComponent w;
  private void Start()
  {
    w = weapon.GetComponent<WeaponComponent>();
  }
  // Update is called once per frame
  void Update()
  {
    if (w.CanFire())
    {
      w.Fire();
    }
  }
}
