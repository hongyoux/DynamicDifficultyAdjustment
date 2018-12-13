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
    private List<ShipData> garbage;

    private void Start()
    {
        garbage = new List<ShipData>();
    }

    public void Update()
    {
        foreach (ShipData s in waveData)
        {
            if (initialTime + s.spawnTime < Time.time)
            {
                garbage.Add(s);
                SpawnShip(s);
            }
        }

        CleanUp();
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
    }

    private void CleanUp()
    {
        foreach (ShipData s in garbage)
        {
            waveData.Remove(s);
        }
        garbage.Clear();

        if (waveData.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
