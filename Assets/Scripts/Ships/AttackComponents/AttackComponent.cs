using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class AttackComponent : MonoBehaviour {
    [HideInInspector]
    protected GameObject weapon;

    private void Awake()
    {
        Ship p = GetComponent<Ship>();
        weapon = Instantiate(p.weapon, p.spawnPoint);
    }
}
