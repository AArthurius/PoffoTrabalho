using UnityEngine;

public class Zumbi : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;


    GameObject player;
    int speed = 250;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void FixedUpdate()
    {
        Mover(player.transform.position);
    }


    void Mover(Vector2 playerPos) {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);


        Vector2 dif = playerPos - pos;

        Vector2 target = new Vector2 (dif.x, dif.y);
        target = target.normalized;


        rb.linearVelocity = target * speed * Time.fixedDeltaTime;
    }

}
