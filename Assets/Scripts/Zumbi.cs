using UnityEngine;

public class Zumbi : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    public GameObject attackHitbox;
    [SerializeField] private Animator anim;

    private Vector2 target = Vector2.zero;

    private int speed = 250;
    public int stop = 1;

    private void Start()
    {
        attackHitbox.SetActive(false);
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
        if (Player.Instance){ 
        Mover(Player.Instance.transform.position);
        }
    
    }


    private void Mover(Vector2 playerPos) {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);


        Vector2 dif = playerPos - pos;

        target = new Vector2 (dif.x, dif.y);
        target = target.normalized;


        rb.linearVelocity = target * (speed * stop * Time.fixedDeltaTime);
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
        Player.Instance.UpdateScore(5);
        Destroy(gameObject);
    }
}
