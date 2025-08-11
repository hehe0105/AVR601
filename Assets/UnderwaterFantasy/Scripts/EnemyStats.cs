using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [Header("»ù±¾ÊôÐÔ")]
    public int maxHP = 100;
    public int atk = 10;

    private int currentHP;

    HealthBarScale hb;
   
    void Awake()
    {
        currentHP = maxHP;
        hb = GetComponentInChildren<HealthBarScale>();
        if (hb) hb.SetHealth(currentHP, maxHP);
    }

    

    public void TakeDamage(int damage) {
        currentHP -= damage;
        //Debug.Log($"{gameObject.name}get{damage}damage,currentHP:{currentHP}");

        if (hb) hb.SetHealth(currentHP, maxHP);
        if (currentHP<=0) {
            Die();
        }
    }
    void Die() {
        //Debug.Log($"{gameObject.name}has been destoryed!");
        Destroy(gameObject);
    }
}
