using UnityEngine;

public class Zumbi : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    Vector2 target = Vector2.zero;

    [SerializeField] GameObject player;
    int speed = 250;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
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


        rb.linearVelocity = target * speed * Time.fixedDeltaTime;
    }

}
