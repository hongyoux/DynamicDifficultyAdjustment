using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : AttackComponent {
	// Update is called once per frame
	void Update () {
        WeaponComponent w = weapon.GetComponent<WeaponComponent>();
        w.Fire();
	}
}
