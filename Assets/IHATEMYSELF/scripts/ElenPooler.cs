using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ElenPooler : MonoBehaviour
{
    Queue<GameObject> objQue = new Queue<GameObject>();
    Queue<GameObject> spObjQue = new Queue<GameObject>();
    [SerializeField] GameObject elenPrefabs ;
    [SerializeField] GameObject spesialElenPrefabs;
    [SerializeField] int maxPoolSize = 10;

    public static ElenPooler Intance {get; private set;}
    void Awake()
    {
        if(Intance == null || Intance != this)
            Intance = this;
        if(SceneManager.GetActiveScene().name != "Main Menu"){
            DontDestroyOnLoad(Intance);
        }else{
            Destroy(gameObject);
        }
        InitPool();
    }

    void InitPool() 
    {
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject obj = Instantiate(elenPrefabs);
            obj.SetActive(false);
            objQue.Enqueue(obj);
        }
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject obj = Instantiate(spesialElenPrefabs);
            obj.SetActive(false);
            spObjQue.Enqueue(obj);
        }
    }

    public GameObject PoolSpawner(Vector3 pos, Quaternion rot)
    {
        GameObject objToSpawn = objQue.Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rot;

        objQue.Enqueue(objToSpawn);

        return objToSpawn;
    }
    public GameObject SpesialPoolSpawner(Vector3 pos,Quaternion rot){
        GameObject objToSpawn = spObjQue.Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rot;

        spObjQue.Enqueue(objToSpawn);
        return objToSpawn;
    }

    public void PoolGetback(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        objQue.Enqueue(obj);
    }
    public void SpesialPoolGetback(GameObject obj){
        obj.gameObject.SetActive(true);
        objQue.Enqueue(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
