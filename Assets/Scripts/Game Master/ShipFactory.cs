using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour {
    // Hack to get dictionary to show up in the inspector
    [Serializable]
    public struct ShipDetails
    {
        public string name;
        public GameObject shipObject;
    }

    [Serializable]
    public struct SpawnPoint
    {
        public string name;
        public GameObject location;
    }

    [Serializable]
    public struct Pattern
    {
        public string name;
        public GameObject pattern;
    }

    public ShipDetails[] ListOfShips;
    public SpawnPoint[] SpawnPoints;
    public Pattern[] patterns;

    public List<GameObject> waves;

    private Dictionary<string, GameObject> shipList;
    private Dictionary<string, Transform> spawnPoints;
    private Dictionary<string, GameObject> patternList;

	// Use this for initialization
	void Start () {
        shipList = new Dictionary<string, GameObject>();
        spawnPoints = new Dictionary<string, Transform>();
        patternList = new Dictionary<string, GameObject>();

		foreach(ShipDetails sd in ListOfShips) {
            shipList.Add(sd.name, sd.shipObject);
        }
        foreach(SpawnPoint sp in SpawnPoints)
        {
            spawnPoints.Add(sp.name, sp.location.transform);
        }
        foreach(Pattern p in patterns)
        {
            patternList.Add(p.name, p.pattern);
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach(GameObject w in waves)
        {
            WaveData wd = w.GetComponent<WaveData>();
            if (wd.initialTime < Time.time)
            {
                wd.Spawn();

                waves.Remove(w);
            }
        }
	}
}
