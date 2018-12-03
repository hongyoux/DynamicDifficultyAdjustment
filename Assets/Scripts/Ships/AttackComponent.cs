using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class AttackComponent : MonoBehaviour {
    [HideInInspector]
    protected GameObject weapon;

    void Start()
    {
        Ship p = GetComponent<Ship>();
        weapon = Instantiate(p.weapon, p.spawnPoint);
    }
}
