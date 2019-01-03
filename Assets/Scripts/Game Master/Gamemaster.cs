using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : MonoBehaviour
{
  public Transform playerSpawnLocation;
  public GameObject player;

  public static GameObject bullets;
  public static GameObject ships;

  private Player p;

  private void Awake()
  {
    bullets = transform.Find("bullets").gameObject;
    ships = transform.Find("ships").gameObject;
  }

  // Use this for initialization
  void Start()
  {
    GameObject pObj = Instantiate(player, playerSpawnLocation.position, playerSpawnLocation.rotation);
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
