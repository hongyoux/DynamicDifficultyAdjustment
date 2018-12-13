using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public ShipStats stats;
    public Transform spawnPoint;
    public GameObject weapon;

    protected Gamemaster gm;

    // Use this for initialization
    void Start () {
        stats.position = transform.position;
        GameObject g = GameObject.Find("GameMaster");
        gm = g.GetComponent<Gamemaster>();
    }

    // Update is called once per frame
    void Update () {
	}

    virtual public void Destroy()
    {
        Destroy(gameObject);
    }
}
