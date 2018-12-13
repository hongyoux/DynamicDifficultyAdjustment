using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship {
	// Update is called once per frame
	void Update () {

    }

    public override void Destroy()
    {
        gm.UpdatePlayerScore(stats.score);
        base.Destroy();
    }
}
