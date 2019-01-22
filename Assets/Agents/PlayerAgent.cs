using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PlayerAgent : Agent
{
  private Player p;
  private Gamemaster gm;
  // Start is called before the first frame update
  void Start()
  {
    p = GetComponent<Player>();
    gm = GameObject.Find("Gamemaster").GetComponent<Gamemaster>();
  }

  public override void AgentReset()
  {
    GetComponent<Renderer>().enabled = true;
    p.stats.currHealth = p.stats.maxHealth;
    p.stats.position = gm.playerSpawnLocation.position;
    transform.position = gm.playerSpawnLocation.position;
    gm.Reset();
  }
}
