using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerManager : MonoBehaviour
{
    [System.Serializable]
    public class pool {
        public string tag;
        public GameObject prefabs;
        public int count;

        public Vector3[] pos;
    }

    public List<pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (pool item in pools)
        {
            Queue<GameObject> spawnPool = new Queue<GameObject>();

            for (int i = 0; i < item.count; i++)
            {
                GameObject obj = Instantiate(item.prefabs, item.pos[i], Quaternion.identity);
                spawnPool.Enqueue(obj);
            }

            poolDictionary.Add(item.tag, spawnPool);
        }
    }
}
