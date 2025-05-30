using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 4f;

    private int dano = 10;

    private void Start()
    {
        Destroy(gameObject, lifetime); // destr�i a bala ap�s X segundos
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.levarDano(dano);  // Dano que voc� quiser
            Destroy(gameObject);
            return;
        }
        Inimigo inimigo = collision.GetComponent<Inimigo>();
        if (inimigo != null)
        {
            inimigo.kill();  // Dano que voc� quiser
            Destroy(gameObject);
            return;
        }
        Zumbi zumbi = collision.GetComponent<Zumbi>();
        if (zumbi != null) {
            zumbi.kill();  // Dano que voc� quiser
            Destroy(gameObject);
            return;

        }
    }
}
