using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackgroundManager : MonoBehaviour
{
    public Vector3 spawnPlace;
    public Vector3 spawnPlaceInverse;
    public int random;

    public ObjectPooler[] objPool;
    [SerializeField] private float time = 0;
    bool grounded;
    public float repeatTime = 2;

    public Vector3 spawnPlaceSide;
    public ObjectPooler[] objPoolSide;
    [SerializeField] private float timeSide = 0;
    public float repeatTimeSide = 2;
    private void Start()
    {
        GameEvent.current.OnGroundBoolEnter += OnGroundEnter;
        GameEvent.current.OnGroundBoolExit += OnGroundExit;
    }

    void FixedUpdate()
    {
        if (!grounded)
        {
            time += Time.fixedDeltaTime;
            if (time > repeatTime)
            {
                time = 0;
                Spawn();
            }
        }
        if (!grounded)
        {
            timeSide += Time.fixedDeltaTime;
            if (timeSide > repeatTimeSide)
            {
                timeSide = 0;
                SpawnSide();
            }
        }
    }

    private void OnGroundEnter()
    {
        grounded = true;
        MoveUpPlatform.speedBack = 0f;
    }
    private void OnGroundExit()
    {
        grounded = false;
        MoveUpPlatform.speedBack = 3f;
    }

    void Spawn()
    {
        random = Random.Range(0, objPool.Length);
        GameObject newBackground;

        if (random == 0 || random == 1)
        {
            switch (random)
            {
                case 0:
                    newBackground = objPool[0].GetPooledObject();
                    newBackground.transform.position = new Vector3(3f, -9f, 9f);
                    newBackground.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                    newBackground.SetActive(true);
                    break;
                case 1:
                    newBackground = objPool[1].GetPooledObject();
                    newBackground.transform.position = new Vector3(3f, -9f, 9f);
                    newBackground.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                    newBackground.SetActive(true);
                    break;
                default:
                    break;
            }
        }           

        switch (random)
        {            
            case 2:
                newBackground = objPool[2].GetPooledObject();
                newBackground.transform.position = spawnPlace;
                newBackground.transform.rotation = transform.rotation;
                newBackground.SetActive(true);
                break;
            case 3:
                newBackground = objPool[3].GetPooledObject();
                newBackground.transform.position = spawnPlace;
                newBackground.transform.rotation = transform.rotation;
                newBackground.SetActive(true);
                break;
            case 4:
                newBackground = objPool[4].GetPooledObject();
                newBackground.transform.position = spawnPlace;
                newBackground.transform.rotation = transform.rotation;
                newBackground.SetActive(true);
                break;
            case 5:
                newBackground = objPool[5].GetPooledObject();
                newBackground.transform.position = spawnPlace;
                newBackground.transform.rotation = transform.rotation;
                newBackground.SetActive(true);
                break;
            default:
                break;
        }
    }
    void SpawnSide()
    {
        random = Random.Range(0, objPoolSide.Length);

        GameObject newSide = objPoolSide[random].GetPooledObject();
        newSide.transform.position = spawnPlaceSide;
        newSide.transform.rotation = transform.rotation;
        newSide.SetActive(true);
    }
}
