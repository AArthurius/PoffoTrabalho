using UnityEngine;

public class bala : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 20f;

    void Start()
    {
        Destroy(gameObject, lifetime); // destr�i a bala ap�s X segundos
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // L�gica de colis�o (ex.: dar dano)
        Destroy(gameObject); // destr�i a bala ao colidir
    }
}
