using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        Enemy enemy = other.GetComponent<Enemy>();

            enemy.TakeDamage(damage);
        
    }
}
