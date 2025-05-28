using UnityEngine;

public class inimigo : MonoBehaviour
{


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private bala balaPrefab;
    [SerializeField] private Transform PontoTiro;
    [SerializeField] private timer timer1;
    [SerializeField] private Vector2 target = Vector2.zero;

    private int speed = 150;

    private void Start()
    {
        anim.Play("Inimigo move");
    }

    private void Update()
    {
        if (Player.instance)
        {
            transform.up = target;
        }
    }

    private void FixedUpdate()
    {
        if (Player.instance)
        {
            Mover(Player.instance.transform.position);
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
        Instantiate(balaPrefab, PontoTiro.position, PontoTiro.rotation);
    }

    public void kill()
    {
        Player.instance.UpdateScore(10);
        Destroy(gameObject);
    }

}
