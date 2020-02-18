using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elenGenerator : MonoBehaviour
{
    const float TRIGGER_DISTANCE = 50f;
    const float PLAYER_MIN_DISTANCE = 30f;
    const float MIN_Y = 4f;
    const float MAX_Y = -10f;
    public bool spesialSpawn = false;

    void Start()
    {
        if (spesialSpawn)
        {
            var obj = ElenPooler.Intance.SpesialPoolSpawner(this.transform.position, Quaternion.identity);
        }
        else
        {
            var obj = ElenPooler.Intance.PoolSpawner(this.transform.position, Quaternion.identity);
        }

    }
    void FixedUpdate()
    {
        //If apa obj di spawn
    }

    void SpawnObject()
    {

    }
}
