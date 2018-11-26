using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletComponent))]
public class BulletMovement : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        BulletComponent b = GetComponent<BulletComponent>();

        Vector2 newPos = b.stats.position;
        Vector2 change = b.stats.direction * (b.stats.velocity * Time.deltaTime);

        newPos += change;
        b.stats.position = newPos;
        transform.position = newPos;

        CheckOutOfBounds();
	}

    void CheckOutOfBounds()
    {
        if (transform.position.x < -5 || transform.position.x > 5 ||
            transform.position.y < -10 || transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }
}
