using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : AttackComponent {
    Player p;
    WeaponComponent w;
    private void Start()
    {
        p = GetComponent<Player>();
        w = weapon.GetComponent<WeaponComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p.stats.lives == 0)
        {
            return;
        }

        if (Input.GetKey("space"))
        {
            w.Fire(p.stats.powerLevel);
        }
    }
}
