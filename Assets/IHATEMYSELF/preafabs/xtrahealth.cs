using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xtrahealth : MonoBehaviour
{
    [SerializeField] private float healVal = 2;
    private readonly string player = "Player";
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == player){
            healthManager.Instance.addHealth(healVal);
            healthManager.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }
}
