using UnityEngine;
using UnityEngine.UI;
public class healthManager : MonoBehaviour
{
    public static healthManager Instance { get; set; }
    public Image health;
    [SerializeField] float totalHealth;
    private float currHealth;
    public float getCurrentHealth { get { return currHealth; } }
    private void Start()
    {
        Instance = this;
        currHealth = totalHealth;
        health.fillAmount = normalization(currHealth, totalHealth);
    }
    public void takeDamage(float damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
        {
            currHealth = 0;
            GameManager.Instance.ReloadLevel();
        }

    }
    public void addHealth(float add)
    {
        currHealth += add;
        if (currHealth >= totalHealth)
            currHealth = totalHealth;
    }
    public void UpdateUI()
    {
        health.fillAmount = normalization(currHealth, totalHealth);
    }
    float normalization(float value, float totalValue)
    {
        return (value - 0) / (totalValue - 0);
    }
}