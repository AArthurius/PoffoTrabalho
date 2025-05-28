using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("não");
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            
            print("sim");
            player.levarDano(damage);
        }
    }
}
