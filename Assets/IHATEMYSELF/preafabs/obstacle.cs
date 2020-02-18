using UnityEngine;

public class obstacle : MonoBehaviour {
    [SerializeField] private float damage;
    private readonly string player = "Player";
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == player){
            healthManager.Instance.takeDamage(damage);
            healthManager.Instance.UpdateUI();
        }
    }
}