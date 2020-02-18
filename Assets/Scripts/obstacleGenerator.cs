using UnityEngine;

public class obstacleGenerator : MonoBehaviour {
    public GameObject[] objectObstacle;
    private void Start()
    {
        
        int rand = UnityEngine.Random.Range(0, objectObstacle.Length);
        Instantiate(objectObstacle[rand],transform.position,Quaternion.identity);
    }
}