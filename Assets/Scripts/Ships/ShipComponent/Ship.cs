using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
  public ShipStats stats;
  public Transform spawnPoint;
  public GameObject weapon;

  protected Gamemaster gm;

  // Use this for initialization
  void Awake()
  {
    stats.position = transform.position;
    GameObject g = GameObject.Find("GameMaster");
    gm = g.GetComponent<Gamemaster>();

    stats.currHealth = stats.maxHealth;
  }

  // Update is called once per frame
  void Update()
  {
  }

  virtual protected void Destroy()
  {
    Destroy(gameObject);
  }

  virtual public void TakeDamage(int damage)
  {
    stats.currHealth -= damage;
  }
}
