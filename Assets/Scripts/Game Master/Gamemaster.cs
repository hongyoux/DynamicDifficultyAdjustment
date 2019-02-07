using System.Collections.Generic;
using UnityEngine;


public class Gamemaster : MonoBehaviour
{
  private static Gamemaster gm;

  public static Gamemaster Instance;

  public Transform playerSpawnLocation;
  public GameObject player;

  public static GameObject bullets;
  public static GameObject ships;
  public static GameObject waves;

  [HideInInspector]
  public ShipFactory sf;

  [HideInInspector]
  public int totalPossiblePoints;

  [HideInInspector]
  public int[] damageDealtByBullets;

  [HideInInspector]
  public int targetTime = 300; //Seconds (5 minute gameplay session)

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
    InitOnce();
    Reset();

    SpawnPlayer();
  }

  void InitOnce()
  {
    // Setup singleton pattern
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject);
    }

    Logger.Instance.CreateLog();
    uiComponent = transform.GetComponent<UIComponent>();
    sf = GetComponentInChildren<ShipFactory>();
  }

  private void SpawnPlayer()
  {
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

    totalPossiblePoints = 0;
    damageDealtByBullets = new int[3]; // Reset to empty array of damage dealt

    sf.Reset();
  }

  public void Stop()
  {
    GamemasterAgent gma = GetComponent<GamemasterAgent>();
    gma.Done();

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
        enemiesByValue = shipStatsCopy.GetRange(0, Mathf.Min(shipStatsCopy.Count, 5));
      }
    }
  }

  private int SortByScore(Ship a, Ship b)
  {
    return -a.stats.score.CompareTo(b.stats.score);
  }

  private void updateObservableBullets()
  {
    if (bullets != null)
    {
      // Sort list of Bullets for proximity to player
      List<BulletComponent> bulletsCopy = new List<BulletComponent>(bullets.GetComponentsInChildren<EnemyBulletComponent>());
      if (bulletsCopy.Count > 0)
      {
        bulletsCopy.Sort(SortByDistance);
        bulletsNearPlayer = bulletsCopy.GetRange(0, Mathf.Min(bulletsCopy.Count, 10));
      }
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

  public int[] GetCountOfAllShips()
  {
    /**
     * Basic ship = 0
     * Chase ship = 1
     * Swirl ship = 2
     */
    int[] shipsCount = new int[3];

    if (ships != null)
    {
      List<EnemyShip> shipStatsCopy = new List<EnemyShip>(ships.GetComponentsInChildren<EnemyShip>());
      foreach(EnemyShip s in shipStatsCopy)
      {
        switch(s.type) {
          case ShipType.BASIC:
            shipsCount[0]++;
            break;
          case ShipType.CHASE:
            shipsCount[1]++;
            break;
          case ShipType.SWIRL:
            shipsCount[2]++;
            break;
        }
      }
    }

    return shipsCount;
  }
}
