using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    private GameObject currentPlayer;
    public ObjectPooler objPoolMagnet;
    public ObjectPooler objPoolImmortal;
    public ObjectPooler objPoolHP;
    void Start()
    {
        GameEvent.current.OnSelect += SelectedPlayer;
        
    }    

    public void SpawnBooster_magnet(Vector3 position)
    {        
        GameObject magnet = objPoolMagnet.GetPooledObject();
        magnet.transform.position = new Vector3(position.x, position.y + 0.8f, position.z);
        magnet.transform.rotation = transform.rotation;
        magnet.SetActive(true);
    }
    public void SpawnBooster_immortal(Vector3 position)
    {
        GameObject immortal = objPoolImmortal.GetPooledObject();
        immortal.transform.position = new Vector3(position.x, position.y + 0.36f, position.z);
        immortal.transform.rotation = transform.rotation;
        immortal.SetActive(true);
    }
    public void SpawnBooster_HP(Vector3 position)
    {
        GameObject hp = objPoolHP.GetPooledObject();
        hp.transform.position = new Vector3(position.x, position.y + 0.3f, position.z);
        hp.transform.rotation = transform.rotation;
        hp.SetActive(true);
    }
    private void SelectedPlayer()
    {
        currentPlayer = GameManager.current.currentPlayer;
    }
}
