using UnityEngine;

public class bala : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

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
    }
}
