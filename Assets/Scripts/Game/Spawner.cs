using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    private float currentSpawnDelay;
    [SerializeField] private float stageDelay;
    [SerializeField] private float spawnTime;
    [SerializeField] private float stageTime;
    
    [SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private GameObject burstPrefab;
    [SerializeField] private GameObject bulletPrefab;

    private GameObject enemy;
    public static List<EnemyController> unusedEnemies;
    //public static List<DestroyAfterTime> unusedBursts = new List<DestroyAfterTime>();
    //public static List<Bullet> unusedBullets;
    
    [SerializeField] private int stage;
    [SerializeField] private int maxStage;

    [SerializeField] private GameObject player;

    [SerializeField] private Transform grid;

    [SerializeField] private List<GameObject> surfaces = new List<GameObject>(2);
    private bool curSurface;

    public static event Action<int> NewStage; 
    
    private int Stage
    {
        get { return stage;}
        set
        {
            stage = value;
            NewStage?.Invoke(stage);
            currentSpawnDelay -= 0.7f;
        }
    }

    private Vector2 minDistance;
    
    private void OnEnable()
    {
        UIController.Restart += Restart;
        TilemapChecker.TilemapExit += UpdateTilemap;
    }

    private void OnDisable()
    {
        UIController.Restart -= Restart;
        TilemapChecker.TilemapExit -= UpdateTilemap;
    }
    
    private void Awake()
    {
        stage = 0;
        stageTime = stageDelay;
        currentSpawnDelay = spawnDelay;
        var center = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth/2f, Camera.main.pixelHeight/2f, 0));
        minDistance = center - Camera.main.ScreenToWorldPoint(Vector3.zero) + new Vector3(5, 5, 0);
        
        surfaces[0].transform.position = Vector3.zero;
        surfaces[1].transform.position = new Vector3(120, 0, 0);
        curSurface = false;

        unusedEnemies = new List<EnemyController>();
        /*unusedBullets = FindObjectsOfType<Bullet>().ToList();
        Debug.Log("Unused bullets: " + unusedBullets.Count);
        foreach (var bullet in unusedBullets)
        {
            bullet.gameObject.SetActive(false);
        }*/
    }
    
    private void Update()
    {
        if(!player)
            return;
        if (stageTime > 0)
        {
            stageTime -= Time.deltaTime;
        }
        else
        {
            if (stage < maxStage)
                Stage++;
            stageTime = stageDelay;
        }
        if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
        }
        else
        {
            var cameraPos = Camera.main.transform.position;
            float randAngle = Random.Range(0, 361);
            var spawnPos = new Vector3(Mathf.Cos(randAngle) * minDistance.x, Mathf.Sin(randAngle) * minDistance.y, 0);
            if (unusedEnemies.Count == 0)
            {
                Instantiate(enemyPrefab, player.transform.position + spawnPos, new Quaternion(), this.transform);
            }
            else
            {
                var existingEnemy = unusedEnemies[0];
                existingEnemy.gameObject.SetActive(true);
                existingEnemy.transform.position = player.transform.position + spawnPos;
                unusedEnemies.Remove(existingEnemy);
            }
            spawnTime = currentSpawnDelay;
        }
    }

    private void Restart()
    {
        var enemies = this.gameObject.GetComponentsInChildren<EnemyController>();
        foreach (var e in enemies)
        {
            e.FakeDestroy();
        }

        var bullets = GameObject.FindGameObjectsWithTag("Projectile");;
        foreach (var bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
        
        var bursts = GameObject.FindGameObjectsWithTag("Effect");;
        foreach (var burst in bursts)
        {
            burst.gameObject.SetActive(false);
        }
        
        surfaces[0].transform.position = Vector3.zero;
        surfaces[1].transform.position = new Vector3(120, 0, 0);
        curSurface = false;
        
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        stageTime = stageDelay;
        Stage = 0;
        currentSpawnDelay = spawnDelay;
    }
    
    private void UpdateTilemap()
    {
        if(!player)
            return;
        if (curSurface)
        {
            surfaces[0].transform.position = player.transform.position;
        }
        else
        {
            surfaces[1].transform.position = player.transform.position;
        }

        curSurface = !curSurface;
    }

    /*public void CreateBurst(Transform transform)
    {
        if (unusedBursts.Count == 0)
        {
            Instantiate(burstPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.Log(unusedBursts.Count + " " + unusedBursts[0]);
            var burst = unusedBursts[0];
            burst.gameObject.SetActive(true);
            burst.transform.position = transform.position;
            burst.transform.rotation = transform.rotation;
            Debug.Log(burst.gameObject.name  + " is removed from Unused");
            unusedBursts.Remove(burst);
        }
    }*/

    /*public void CreateBullet(Vector3 position, Vector3 offset, Quaternion rotation)
    {
        if (unusedBullets.Count == 0)
        {
            var bullet = Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
            bullet.GetComponent<Bullet>().SetDirection(offset);
            Debug.Log("Create a bullet " + bullet.gameObject.name);
        }
        else
        {
            var bullet = unusedBullets[0];
            bullet.gameObject.SetActive(true);
            bullet.transform.position = transform.position + offset;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<Bullet>().SetDirection(offset);
            Debug.Log(bullet.gameObject.name  + " is active again");
            unusedBullets.Remove(bullet);
        }
    }*/
}
