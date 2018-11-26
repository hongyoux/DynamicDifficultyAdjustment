using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class AttackComponent : MonoBehaviour {
    GameObject weapon;

    void Start()
    {
        Player p = GetComponent<Player>();
        weapon = Instantiate(p.tempWeapon, p.spawnPoint);
    }

    // Update is called once per frame
    void Update () {
        Player p = GetComponent<Player>();
        if (p.shipStats.lives == 0)
        {
            return;
        }

        if (Input.GetKey("space")) {
            BaseWeapon w = weapon.GetComponent<BaseWeapon>();
            w.Fire(p.shipStats.powerLevel);
        }
	}
}
