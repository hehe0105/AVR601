using UnityEngine;

public class PlayerHP : MonoBehaviour
{

    public int maxHP = 200;
    public int currentHP;

    HealthBarScale hb;

    void Awake()
    {
        currentHP = maxHP;
        hb = GetComponentInChildren<HealthBarScale>();//’“µΩHealthBar
        if (hb) hb.SetHealth(currentHP, maxHP);
    }
    public void TakeDamage(int damage) {
        currentHP -= damage;
        if (hb) hb.SetHealth(currentHP, maxHP);
        if (currentHP <= 0) {
            Die();
        }
    }
    void Die() {
        Debug.Log("Play died");
        Destroy(gameObject);
    }
}
