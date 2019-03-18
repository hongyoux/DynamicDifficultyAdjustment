using System;
using System.Collections.Generic;
using UnityEngine;


public class Gamemaster : MonoBehaviour
{
  private static Gamemaster gm;

  public static Gamemaster Instance;

  public Transform playerSpawnLocation;
  public GameObject player;
  public GameObject scorePickup;

  public static GameObject bullets;
  public static GameObject ships;
  public static GameObject waves;
  public static GameObject scoreObjs;

  [HideInInspector]
  public float timeStart;

  [HideInInspector]
  public ShipFactory sf;

  [HideInInspector]
  public int totalPossiblePoints;

  [HideInInspector]
  public int[] damageDealtByBullets;

  [HideInInspector]
  public int targetTime; //Seconds (2 minute gameplay session)

  [HideInInspector]
  public int previousHP;

  public List<BulletComponent> bulletsNearPlayer;
  public List<Ship> enemiesByValue;
  public List<ScorePickupMovement> scorePickupsNearPlayer;

  private Player p;
  private PlayerAgent pa;
  private GamemasterAgent gma;
  private UIComponent uiComponent;

  private bool doOnce;

  // Use this for initialization
  void Awake()
  {
    targetTime = 60;
    doOnce = true;
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
    gma = GetComponent<GamemasterAgent>();
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

    scoreObjs = new GameObject("scoreObjs");
    scoreObjs.transform.parent = transform;

    totalPossiblePoints = 0;
    damageDealtByBullets = new int[4]; // Reset to empty array of damage dealt

    previousHP = -1;

    timeStart = Time.time;

    sf.Reset();
  }

  public void Stop()
  {
    gma.Done();

    Destroy(bullets);
    Destroy(ships);
    Destroy(waves);
    Destroy(scoreObjs);

    sf.Stop();
  }

  // Update is called once per frame
  void Update()
  {
    if (doOnce)
    {
      doOnce = false;
    }

    uiComponent.UpdateUI(p.stats.currHealth, p.stats.lives, p.stats.score, getGameTime());

    updateObservableEnemies();
    updateObservableBullets();
    updateObservableScoreObjects();

    if (Input.GetKeyDown(KeyCode.Space))
    {
      Stop();
      pa.Done();
    }
  }

  private void updateObservableScoreObjects()
  {
    if (scoreObjs != null)
    {
      List<ScorePickupMovement> scorePickupsCopy = new List<ScorePickupMovement>(scoreObjs.GetComponentsInChildren<ScorePickupMovement>());
      if (scorePickupsCopy.Count > 0)
      {
        scorePickupsCopy.Sort(SortByDistance);
        scorePickupsNearPlayer = scorePickupsCopy.GetRange(0, Mathf.Min(scorePickupsCopy.Count, 5));
      }
    }
  }

  private void updateObservableEnemies()
  {
    if (ships != null)
    {
      List<Ship> shipStatsCopy = new List<Ship>(ships.GetComponentsInChildren<EnemyShip>());

      if (shipStatsCopy.Count > 0)
      {
        shipStatsCopy.Sort(SortByDistance);
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
      List<BulletComponent> threatBullets = new List<BulletComponent>();
      foreach (BulletComponent x in bullets.GetComponentsInChildren<EnemyBulletComponent>())
      {
        if (FilterByThreat(x))
        {
          threatBullets.Add(x);
        }
      }

      if (threatBullets.Count > 0)
      {
        threatBullets.Sort(SortByTime);
        bulletsNearPlayer = threatBullets.GetRange(0, Mathf.Min(threatBullets.Count, 10));
      }
    }
  }

  private int SortByDistance(BulletComponent a, BulletComponent b)
  {
    float distA = Vector3.Distance(new Vector3(a.stats.position.x, a.stats.position.y, 0), p.transform.position);
    float distB = Vector3.Distance(new Vector3(b.stats.position.x, b.stats.position.y, 0), p.transform.position);

    return distA.CompareTo(distB);
  }
  private int SortByDistance(Ship a, Ship b)
  {
    float distA = Vector3.Distance(new Vector3(a.stats.position.x, a.stats.position.y, 0), p.transform.position);
    float distB = Vector3.Distance(new Vector3(b.stats.position.x, b.stats.position.y, 0), p.transform.position);

    return distA.CompareTo(distB);
  }

  private int SortByTime(BulletComponent a, BulletComponent b)
  {
    float distA = Vector3.Distance(new Vector3(a.stats.position.x, a.stats.position.y, 0), p.transform.position);
    float distB = Vector3.Distance(new Vector3(b.stats.position.x, b.stats.position.y, 0), p.transform.position);

    float timeA = distA / a.stats.velocity;
    float timeB = distB / a.stats.velocity;

    return timeA.CompareTo(timeB);
  }

  private bool FilterByThreat(BulletComponent a)
  {
    Vector3 dirToPlayer = p.transform.position - new Vector3(a.stats.position.x, a.stats.position.y, 0f);
    dirToPlayer.Normalize();

    Vector3 direction = new Vector3(a.stats.direction.x, a.stats.direction.y, 0f).normalized;

    float angleRad = Mathf.Acos(Vector3.Dot(dirToPlayer, direction));

    return Mathf.Rad2Deg * angleRad < 3f;
  }

  public Player GetPlayer()
  {
    return p;
  }

  public void UpdatePlayerScore(int score)
  {
    p.stats.score += score;
    pa.SetReward(score * .00001f);
    Logger.Instance.LogScore(p.stats.score);
  }

  public float getGameTime()
  {
    return Time.time - timeStart;
  }

  public int[] GetCountOfAllShips()
  {
    /**
     * Basic ship = 0
     * Chase ship = 1
     * Swirl ship = 2
     * Sweep ship = 3
     */
    int[] shipsCount = new int[4];

    if (ships != null)
    {
      List<EnemyShip> shipStatsCopy = new List<EnemyShip>(ships.GetComponentsInChildren<EnemyShip>());
      foreach (EnemyShip s in shipStatsCopy)
      {
        switch (s.type)
        {
          case ShipType.BASIC:
            shipsCount[0]++;
            break;
          case ShipType.CHASE:
            shipsCount[1]++;
            break;
          case ShipType.SWIRL:
            shipsCount[2]++;
            break;
          case ShipType.SWEEP:
            shipsCount[3]++;
            break;
        }
      }
    }

    return shipsCount;
  }

  public void PlayerHitReward()
  {
    float expectedDamage = GetExpectedDamage();

    if (previousHP - p.stats.currHealth < expectedDamage)
    {
      gma.AddReward(.001f);
    }
    else
    {
      gma.AddReward(-.001f);
    }
  }

  public float GetExpectedDamage()
  {
    float percentGameTime = getGameTime() / targetTime;

    float playerDmg = p.stats.maxHealth - p.stats.currHealth;

    float expectedDmg = (percentGameTime * p.stats.maxHealth) - playerDmg;

    return expectedDmg;
  }

  public float GetExpectedDamagePercent()
  {
    return GetExpectedDamage() / p.stats.maxHealth;
  }

  public void SpawnWaveReward()
  {
    gma.AddReward(.005f);
  }
}