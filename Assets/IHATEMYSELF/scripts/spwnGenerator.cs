using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwnGenerator : MonoBehaviour
{
    [SerializeField] Transform spwnPos;
    public GameObject[] spwnPosGenerator;

    void Start() {
        int rand = Random.Range(0, spwnPosGenerator.Length);
        Instantiate(spwnPosGenerator[rand], spwnPos.position + new Vector3(30f,0), Quaternion.identity);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
