using System.Collections.Generic;
using UnityEngine;


public class Gamemaster : MonoBehaviour
{
  public Transform playerSpawnLocation;
  public GameObject player;

  public static GameObject bullets;
  public static GameObject ships;
  public static GameObject waves;

  public ShipFactory sf;

  [HideInInspector]
  public List<BulletComponent> bulletsNearPlayer;
  [HideInInspector]
  public List<Ship> enemiesByValue;

  private Player p;
  private PlayerAgent pa;
  private UIComponent uiComponent;
  
  // Use this for initialization
  void Awake()
  {
    Logger.Instance.CreateLog();

    uiComponent = transform.GetComponent<UIComponent>();
    sf = GetComponentInChildren<ShipFactory>();

    bullets = new GameObject("bullets");
    bullets.transform.parent = transform;

    ships = new GameObject("ships");
    ships.transform.parent = transform;

    waves = new GameObject("waves");
    waves.transform.parent = transform;

    GameObject pObj = Instantiate(player, playerSpawnLocation.position, playerSpawnLocation.rotation, ships.transform);
    p = pObj.GetComponent<Player>();
    pa = p.GetComponent<PlayerAgent>();
  }

  public void Reset()
  {
    bullets = new GameObject("bullets");
    bullets.transform.parent = transform;

    ships = new GameObject("ships");
    ships.transform.parent = transform;

    waves = new GameObject("waves");
    waves.transform.parent = transform;

    sf.Reset();
  }

  public void Stop()
  {
    Destroy(bullets);
    Destroy(ships);
    Destroy(waves);

    sf.Stop();
  }

  // Update is called once per frame
  void Update()
  {
    uiComponent.UpdateUI(p.stats.currHealth, p.stats.lives, p.stats.score, 0.0f);

    updateObservableEnemies();
    updateObservableBullets();

  }

  private void updateObservableEnemies()
  {
    if (ships != null)
    {
      List<Ship> shipStatsCopy = new List<Ship>(ships.GetComponentsInChildren<EnemyShip>());

      if (shipStatsCopy.Count > 0)
      {
        shipStatsCopy.Sort(SortByScore);
        enemiesByValue = shipStatsCopy.GetRange(0, Mathf.Min(shipStatsCopy.Count, 10));
      }
    }
  }

  private int SortByScore(Ship a, Ship b)
  {
    return -a.stats.score.CompareTo(b.stats.score);
  }

  private void updateObservableBullets()
  {
    // Sort list of Bullets for proximity to player
    List<BulletComponent> bulletsCopy = new List<BulletComponent>(bullets.GetComponentsInChildren<EnemyBulletComponent>());
    if (bulletsCopy.Count > 0)
    {
      bulletsCopy.Sort(SortByDistance);
      bulletsNearPlayer = bulletsCopy.GetRange(0, Mathf.Min(bulletsCopy.Count, 100));
    }
  }

  private int SortByDistance(BulletComponent a, BulletComponent b)
  {
    int distA = (int) Vector3.Distance(new Vector3(a.stats.position.x, a.stats.position.y, 0), p.transform.position);
    int distB = (int) Vector3.Distance(new Vector3(b.stats.position.x, b.stats.position.y, 0), p.transform.position);

    return distA.CompareTo(distB);
  }

  public Player GetPlayer()
  {
    return p;
  }

  public void UpdatePlayerScore(int score)
  {
    p.stats.score += score;
    pa.SetReward(score * .05f);
    Logger.Instance.LogScore(p.stats.score);
  }
}
