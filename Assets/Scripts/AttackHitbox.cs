using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] Player player;

    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.levarDano(damage);
        }
    }
}
