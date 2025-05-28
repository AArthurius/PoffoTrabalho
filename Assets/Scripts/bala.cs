using UnityEngine;

public class bala : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 20f;

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
        // Lógica de colisão (ex.: dar dano)
        Destroy(gameObject); // destrói a bala ao colidir
    }
}
