using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public ShipStats stats;
    public Transform spawnPoint;
    public GameObject weapon;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    virtual public void Destroy()
    {
        Destroy(gameObject);
    }
}
