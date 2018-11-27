using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class AttackComponent : MonoBehaviour {
    GameObject weapon;

    void Start()
    {
        Ship p = GetComponent<Ship>();
        weapon = Instantiate(p.weapon, p.spawnPoint);
    }

    // Update is called once per frame
    void Update () {
        Player p = GetComponent<Player>();
        if (p.stats.lives == 0)
        {
            return;
        }

        if (Input.GetKey("space")) {
            BaseWeapon w = weapon.GetComponent<BaseWeapon>();
            w.Fire(p.stats.powerLevel);
        }
	}
}
