using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData : MonoBehaviour {
    public float initialTime;

    [Serializable]
    public struct ShipData
    {
        public float spawnTime;
        public Transform spawnPoint;
        public GameObject spawnType;
        public GameObject pattern;
    }

    public List<ShipData> waveData;

    public void Update()
    {
        foreach (ShipData s in waveData)
        {
            if (initialTime + s.spawnTime < Time.time)
            {
                waveData.Remove(s);
                SpawnShip(s);
            }
        }
        if (waveData.Count == 0)
        {
            Destroy(this);
        }
    }

    public void Spawn()
    {
        Instantiate(this);
    }

    private void SpawnShip(ShipData s)
    {
        GameObject ship = Instantiate(s.spawnType, s.spawnPoint.position, s.spawnPoint.rotation);
        EnemyPatternComponent epc = ship.AddComponent<EnemyPatternComponent>();
        epc.patternData = s.pattern;
        ShipStats ss = ship.GetComponent<ShipStats>();
        ss.position = s.spawnPoint.position;
    }
}
