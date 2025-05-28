using UnityEngine;

public class inimigo : MonoBehaviour
{


    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] bala balaPrefab;
    [SerializeField] Transform PontoTiro;
    [SerializeField] timer timer1;
    [SerializeField] Vector2 target = Vector2.zero;

    GameObject player;
    int speed = 150;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim.Play("Inimigo move");
    }

    private void Update()
    {
        if (player != null)
        {
            transform.up = target;
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Mover(player.transform.position);
        }

    }


    void Mover(Vector2 playerPos)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);


        Vector2 dif = playerPos - pos;

        target = new Vector2(dif.x, dif.y);
        target = target.normalized;


        rb.linearVelocity = target * speed * Time.fixedDeltaTime;
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
        Destroy(gameObject);
    }

}
