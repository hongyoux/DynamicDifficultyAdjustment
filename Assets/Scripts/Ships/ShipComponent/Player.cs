using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Ship
{
  private PlayerAgent pa;

  private void Start()
  {
    pa = GetComponent<PlayerAgent>();
  }

  // Update is called once per frame
  void Update()
  {
    if (stats.lives >= 1 && stats.currHealth <= 0)
    {
      stats.lives -= 1;
      stats.currHealth = stats.maxHealth;

      Gamemaster.Instance.Stop();
      pa.Done();

      if (stats.lives == 0)
      {
#if !UNITY_EDITOR
      Destroy(gameObject);
      SceneManager.LoadScene(2);
#else
        stats.lives = 3;
#endif
      }
    }
  }

  public override void TakeDamage(int damage) 
  {
    Logger.Instance.LogDamage(stats.currHealth);
    base.TakeDamage(damage);
  }
}
