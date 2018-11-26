using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInputComponent : MonoBehaviour {
    // Update is called once per frame
	void Update() {
        Player p = this.GetComponent<Player>();

        if (p.ShipStats.lives == 0)
        {
            return;
        }

        float speed = p.ShipStats.movespeed * Time.deltaTime;
        Vector2 newPos = p.ShipStats.position;

        // Get ships new position
        if (Input.GetKey("w"))
        {
            newPos.y += speed;
        }
        if (Input.GetKey("a"))
        {
            newPos.x -= speed;
        }
        if (Input.GetKey("s"))
        {
            newPos.y -= speed;
        }
        if (Input.GetKey("d"))
        {
            newPos.x += speed;
        }

        newPos.x = Mathf.Clamp(newPos.x, -5f, 5f);
        newPos.y = Mathf.Clamp(newPos.y, -9.5f, 9.5f);

        p.ShipStats.position = newPos;
        transform.position = newPos;
    }
}
