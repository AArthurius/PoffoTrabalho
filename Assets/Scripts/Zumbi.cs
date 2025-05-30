using UnityEngine;

public class Zumbi : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    public GameObject attackHitbox;
    [SerializeField] Animator anim;

    Vector2 target = Vector2.zero;

    [SerializeField] GameObject player;
    int speed = 250;
    public int stop = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackHitbox.SetActive(false);
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
        if (player != null){ 
        Mover(player.transform.position);
        }
    
    }


    void Mover(Vector2 playerPos) {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);


        Vector2 dif = playerPos - pos;

        target = new Vector2 (dif.x, dif.y);
        target = target.normalized;


        rb.linearVelocity = target * speed * stop * Time.fixedDeltaTime;
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
        stop = 0;
        anim.Play("attack");
    }

    public void kill()
    {
        Destroy(gameObject);
    }
}
