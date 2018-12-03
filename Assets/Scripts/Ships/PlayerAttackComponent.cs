using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : AttackComponent {
    // Update is called once per frame
    void Update()
    {
        Player p = GetComponent<Player>();
        if (p.stats.lives == 0)
        {
            return;
        }

        if (Input.GetKey("space"))
        {
            WeaponComponent w = weapon.GetComponent<WeaponComponent>();
            w.Fire(p.stats.powerLevel);
        }
    }
}
