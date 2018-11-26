using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class AttackComponent : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        Player p = GetComponent<Player>();

        if (p.ShipStats.lives == 0)
        {
            return;
        }

        if (Input.GetKey("space")) {
            p.ShipStats.score += 10;

            p.ShipStats.lives -= 1;

            Debug.Log("Attack Clicked!");
        }		
	}
}
