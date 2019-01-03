using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : MonoBehaviour
{
  public Transform playerSpawnLocation;
  public GameObject player;

  public static GameObject bullets;
  public static GameObject ships;
  public static GameObject waves;

  private Player p;
  
  // Use this for initialization
  void Start()
  {
    bullets = new GameObject("bullets");
    bullets.transform.parent = transform;

    ships = new GameObject("ships");
    ships.transform.parent = transform;

    waves = new GameObject("waves");
    waves.transform.parent = transform;

    GameObject pObj = Instantiate(player, playerSpawnLocation.position, playerSpawnLocation.rotation, ships.transform);
    p = pObj.GetComponent<Player>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void UpdatePlayerScore(int score)
  {
    p.stats.score += score;
  }
}
