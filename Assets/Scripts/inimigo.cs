using UnityEngine;
using UnityEngine.Serialization;

public class Inimigo : MonoBehaviour
{


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Bala balaPrefab;
    [SerializeField] private Transform pontoTiro;
    [SerializeField] private Timer timer1;
    [SerializeField] private Vector2 target = Vector2.zero;

    private int speed = 150;


    private void Start()
    {
        anim.Play("Inimigo move");
    }

    private void Update()
    {
        if (Player.Instance)
        {
            transform.up = target;
        }
    }

    private void FixedUpdate()
    {
        if (Player.Instance)
        {
            Mover(Player.Instance.transform.position);
        }

    }


    private void Mover(Vector2 playerPos)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);


        Vector2 dif = playerPos - pos;

        target = new Vector2(dif.x, dif.y);
        target = target.normalized;


        rb.linearVelocity = target * (speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        print(collision);
        if (collision.CompareTag("Player"))
        {
            attack();
        }
    }

    public void attack()
    {
        timer1.resetTimer();
        Instantiate(balaPrefab, pontoTiro.position, pontoTiro.rotation);
    }

    public void kill()
    {
        Player.Instance.UpdateScore(10);
        Destroy(gameObject);
    }

}
