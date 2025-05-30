using UnityEngine;

public class bala : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 4f;

    int dano = 10;
    void Start()
    {
        Destroy(gameObject, lifetime); // destrói a bala após X segundos
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.levarDano(dano);  // Dano que você quiser
            Destroy(gameObject);
            return;
        }
        inimigo inimigo = collision.GetComponent<inimigo>();
        if (inimigo != null)
        {
            inimigo.kill();  // Dano que você quiser
            Destroy(gameObject);
            return;
        }
        Zumbi zumbi = collision.GetComponent<Zumbi>();
        if (zumbi != null) {
            zumbi.kill();  // Dano que você quiser
            Destroy(gameObject);
            return;

        }
    }
}
