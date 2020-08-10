using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{    
    [Header("Time to Spawn Elements")]
    public float repeatTime = 2;
    public float repeatTimeNewZone = 2;
    [SerializeField] private float time = 0;

    [Header("Time to change zone")]
    [SerializeField] private float timeToNewZone = 60f;
    [SerializeField] private float timeNewZone = 0;

    [Header("Time to spawn enemy ball")]
    [SerializeField] private float timeForBall = 0f;
    [SerializeField] private float timeForBallRepeart = 30f;

    [Header("Time to spawn BOSS")]
    [SerializeField] private float timeForBoss = 0f;
    [SerializeField] private float timeForBossRepeart = 30f;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private int countToBoss = 0;

    [Header("Standart Platforms")]
    public Vector3 spawnPlace;
    public Vector3 spawnPlaceInverse;
    public int random;
    public int randomInto;    
    public ObjectPooler[] objPool0;    
    bool grounded;    

    [Header("NewZone Platforms")]
    public ObjectPooler[] objPoolNewZone;
    [SerializeField] private bool newZone;

    [Header("Enemy")]
    public ObjectPooler[] objPoolEnemy;
    [Header("Coin")]
    public ObjectPooler objPoolCoin;
    [Header("Bo0sters")]
    public BoosterManager boosterManager;
    //private PlayerControll currentPC;
    // Start is called before the first frame update
    void Start()
    {
        //currentPC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();        
        //delegate
        GameEvent.current.OnGroundBoolEnter += OnGroundEnter;
        GameEvent.current.OnGroundBoolExit += OnGroundExit;
        //
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.current.isStart)
        {
            if (!grounded && !newZone && !isBoss)
            {
                time += Time.fixedDeltaTime;
                if (time > repeatTime)
                {
                    time = 0;
                    Spawn();
                }

                timeForBall += Time.fixedDeltaTime;
                if (timeForBall > timeForBallRepeart)
                {
                    timeForBall = 0;
                    StartCoroutine("SpawnEnemyBall");
                }

            }
            else if (!grounded && newZone && !isBoss)
            {
                timeNewZone += Time.fixedDeltaTime;
                if (timeNewZone > repeatTimeNewZone)
                {
                    timeNewZone = 0;
                    SpawnNewZone();
                }
            }

            timeToNewZone -= Time.fixedDeltaTime;
            if (timeToNewZone < 0 && !isBoss)
            {
                timeToNewZone = 60f;
                SwitchSpawn();
            }
        }
    }

    private void OnGroundEnter()
    {
        grounded = true;
        MoveUpPlatform.speed = 0f;
    }
    private void OnGroundExit()
    {
        grounded = false;
        MoveUpPlatform.speed = 6f;
    }
    void SwitchSpawn()
    {
       // countToBoss++;
        newZone = !newZone;
        if(countToBoss == 3)
        {
            BossStage();
            isBoss = true;
        }
        else
            GameEvent.current.ZoneBegin();
    }
    void Spawn()
    {
        random = Random.Range(0, objPool0.Length);
        GameObject newPlatform;

        //moving platform
        if (random == objPool0.Length - 1)
        {
            newPlatform = objPool0[objPool0.Length -1].GetPooledObject();
            newPlatform.transform.position = new Vector3(0f,-9f,0f);
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
        }
        //other platform
        else
        {
            newPlatform = objPool0[random].GetPooledObject();
            newPlatform.transform.position = spawnPlace;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
        }

        //SpawnEnemy(new Vector3(randomEnemySpawn, -9f, -1f), 0, "enemy_spike");
        //SpawnEnemy(new Vector3(randomEnemySpawnInverse, -9f, -1f), 0, "enemy_spike");

        float randomEnemySpawn_spike = Random.Range(-1.85f, 0f);
        float randomEnemySpawnInverse_spike = Random.Range(0f, 1.85f);
        int randomEnemy_fan = Random.Range(0, 3);
        int randomBooster = Random.Range(0, 10);
        float randomPos_X = Random.Range(-1.9f, 1.9f);
        randomInto = Random.Range(0, 4);
        //0-long, 1-middle-long, 2-middle-short, 3-middle, 4-short
        //for front platform creating
        switch (random)
        {
            case 0: //long
                if (randomInto == 0)
                {                    
                    SpawnPlatform(spawnPlaceInverse, 4, Quaternion.Euler(0f, 180f, 0f), "short");
                }
                else if (randomInto == 1)
                {                    
                    //SpawnPlatform(spawnPlaceInverse, 2, Quaternion.Euler(0f, 180f, 0f), "middle-short");

                    //enemy
                    switch (randomEnemy_fan)
                    {
                        case 0:
                            SpawnEnemy(new Vector3(2.5f, -9f, 0), 2, Quaternion.Euler(0f, 180f, 0f), "enemy_fan");
                            break;
                        case 1:
                            SpawnEnemy(new Vector3(2.5f, -9f, 0), 3, Quaternion.Euler(0f, 180f, 0f), "enemy_fanInverse");
                            break;
                        default:
                            break;
                    }
                    
                }

                //booster
                if (randomBooster == 4)
                    boosterManager.SpawnBooster_magnet(new Vector3(-2f, -9f, 0));

                //enemy
                SpawnEnemy(new Vector3(0.9f, -9f, -1f), 1, Quaternion.Euler(0f, 0f, 0f), "enemy_lock");

                break;
            case 1: //middle-long
                if(randomInto == 0)
                {                    
                    SpawnPlatform(spawnPlaceInverse, 2, Quaternion.Euler(0f, 180f,0f), "middle-short");

                    //enemy
                    
                }
                //else if(randomInto == 1)
                //{                   
                //    SpawnPlatform(spawnPlaceInverse, 3, Quaternion.Euler(0f, 180f, 0f), "middle");
                //}

                //enemy
                SpawnEnemy(new Vector3(randomEnemySpawn_spike, -9f, -1f), 0, Quaternion.Euler(0f, 0f, 0f), "enemy_spike");

                break;
            case 2: //middle-short       
                if (randomInto == 0)
                {
                    SpawnPlatform(spawnPlaceInverse, 1, Quaternion.Euler(0f, 180f, 0f), "middle-long");

                    //enemy
                    SpawnEnemy(new Vector3(randomEnemySpawnInverse_spike, -9f, -1f), 0, Quaternion.Euler(0f, 0f, 0f), "enemy_spike");
                }
                else if (randomInto == 1)
                {
                    SpawnPlatform(spawnPlaceInverse, 3, Quaternion.Euler(0f, 180f, 0f), "middle");
                }
                else if (randomInto == 2)
                {
                    SpawnPlatform(spawnPlaceInverse, 2, Quaternion.Euler(0f, 180f, 0f), "middle-short");                    
                }

                //booster
                 if (randomBooster == 6)
                    boosterManager.SpawnBooster_HP(new Vector3(randomPos_X, -9f, 0));

                //enemy                
                switch (randomEnemy_fan)
                {
                    case 0:
                        SpawnEnemy(new Vector3(2.5f, -9f, 0), 2, Quaternion.Euler(0f, 180f, 0f), "enemy_fan");
                        break;
                    case 1:
                        SpawnEnemy(new Vector3(2.5f, -9f, 0), 3, Quaternion.Euler(0f, 180f, 0f), "enemy_fanInverse");
                        break;
                    default:
                        break;
                }
                break;
            case 3: //middle
                if (randomInto == 0)
                {
                    SpawnPlatform(spawnPlaceInverse, 3, Quaternion.Euler(0f, 180f, 0f), "middle");
                }
                else if (randomInto == 1)
                {
                    SpawnPlatform(spawnPlaceInverse, 2, Quaternion.Euler(0f, 180f, 0f), "middle-short");
                }

                //booster
                if (randomBooster == 5)
                    boosterManager.SpawnBooster_immortal(new Vector3(0, -9f, 0));

                break;
            case 4: //short                
                SpawnPlatform(spawnPlaceInverse, 0, Quaternion.Euler(0f, 180f, 0f), "long");

                //booster
                if (randomBooster == 4)
                    boosterManager.SpawnBooster_magnet(new Vector3(2f, -9f, 0));

                //enemy
                SpawnEnemy(new Vector3(1.9f, -9f, -1f), 1, Quaternion.Euler(0f, 0f, 0f), "enemy_lock");
                break;
            default:
                break;
        }
    }
    void SpawnPlatform(Vector3 spawnPosition, int arrayNum, Quaternion quaternion ,string str)
    {
        GameObject newPlatform = objPool0[arrayNum].GetPooledObject();
        newPlatform.transform.position = spawnPosition;
        newPlatform.transform.rotation = quaternion;
        newPlatform.SetActive(true);

        SpawnCoin(spawnPosition);
    }
    void SpawnNewZone()
    {
        float randomX = Random.Range(-1.9f, 1.9f);
        random = Random.Range(0, objPoolNewZone.Length);

        GameObject newZonePlatform = objPoolNewZone[random].GetPooledObject();
        newZonePlatform.transform.position = new Vector3(randomX, -9f, 0f);
        newZonePlatform.transform.rotation = transform.rotation;
        newZonePlatform.SetActive(true);

        //enemy
        int randomSpawn_fan = Random.Range(0, 15);            
        switch (randomSpawn_fan)
        {
            case 0:
                SpawnEnemy(new Vector3(2.5f, -9f, 0), 2, Quaternion.Euler(0f, 180f, 0f), "enemy_fan");
                break;
            case 1:
                SpawnEnemy(new Vector3(-2.5f, -9f, 0), 2, Quaternion.Euler(0f, 0f, 0f), "enemy_fan");
                break;
            case 2:
                SpawnEnemy(new Vector3(2.5f, -9f, 0), 3, Quaternion.Euler(0f, 180f, 0f), "enemy_fanInverse");
                break;
            case 4:
                SpawnEnemy(new Vector3(-2.5f, -9f, 0), 3, Quaternion.Euler(0f, 0f, 0f), "enemy_fanInverse");
                break;
            case 13:
                //booster
                boosterManager.SpawnBooster_magnet(new Vector3(randomX, -9f, 0));
                break;
            case 14:
                //booster
                boosterManager.SpawnBooster_HP(new Vector3(randomX, -9f, 0));
                break;
            default:
                break;
        }

        SpawnCoin(spawnPlace);
    }

    //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    public void SpawnCoin(Vector3 startPosition)
    {
        float randomCoin = Random.Range(-1.9f, 1.9f);
        GameObject coin = objPoolCoin.GetPooledObject();
        coin.transform.position = new Vector3(randomCoin, startPosition.y + 0.5f, startPosition.z);
        coin.SetActive(true);
    }
    public void SpawnEnemy(Vector3 startPosition, int arrayNum, Quaternion quaternion, string str = "null")
    {
        GameObject newSpike = objPoolEnemy[arrayNum].GetPooledObject();
        newSpike.transform.position = startPosition;
        newSpike.transform.rotation = quaternion;
        newSpike.SetActive(true);
    }

    IEnumerator SpawnEnemyBall()
    {
        GameObject enemyBall;
        for (float i = 0f; i <= 5f; i += 1f)
        {
            enemyBall = objPoolEnemy[4].GetPooledObject();
            enemyBall.transform.position = new Vector3(-2f, 5.6f, 0f);
            enemyBall.transform.rotation = transform.rotation;
            enemyBall.SetActive(true);
            yield return new WaitForSeconds(1f);
        }        
    }

    //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

    private void BossStage()
    {
        GameEvent.current.BossBegin();
    }
}
