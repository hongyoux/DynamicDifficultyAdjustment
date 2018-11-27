using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInputComponent : MonoBehaviour {
    // Update is called once per frame
	void Update() {
        Player p = this.GetComponent<Player>();

        if (p.stats.lives == 0)
        {
            return;
        }

        float speed = p.stats.movespeed * Time.deltaTime;
        Vector2 newPos = p.stats.position;

        // Get ships new position
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            newPos.y += speed;
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            newPos.x -= speed;
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            newPos.y -= speed;
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            newPos.x += speed;
        }

        newPos.x = Mathf.Clamp(newPos.x, -5f, 5f);
        newPos.y = Mathf.Clamp(newPos.y, -9.5f, 9.5f);

        p.stats.position = newPos;
        transform.position = newPos;
    }
}
