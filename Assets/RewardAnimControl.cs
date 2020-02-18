using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardAnimControl : MonoBehaviour
{
    public GameObject[] activeObject;
    // Start is called before the first frame update
    void Start()
    {
        if(activeObject.Length != null){
            foreach (var item in activeObject)
            {
                item.SetActive(false);
            }
        }
    }
    public void ShowObject(){
        if(activeObject.Length != null){
            foreach (var item in activeObject)
            {
                item.SetActive(true);
            }
        }
    }
}
