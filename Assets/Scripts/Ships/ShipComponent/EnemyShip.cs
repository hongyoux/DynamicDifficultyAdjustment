using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship {
	// Use this for initialization
	void Start () {
        stats.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
