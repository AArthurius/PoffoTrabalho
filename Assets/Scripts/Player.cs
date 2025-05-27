using UnityEngine;

public class Player : MonoBehaviour
{
    // Teclas de movimento 
    KeyCode up = KeyCode.W;
    KeyCode down = KeyCode.S;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;

    [SerializeField] float speed = 300f;
     Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MouseOrientation();
       
    }
    void FixedUpdate()
    {
        Movimentacao();
    }

    public void Movimentacao()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.linearVelocity = input.normalized * speed * Time.fixedDeltaTime;
    }

    void MouseOrientation()
    {
        Vector3 mousePos = Input.mousePosition; //  Passa pro Vetor a posição do mouse na cena.
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); // Pega a posição do Mouse na camera

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y); // cria um vetor apontando do Player para o Mouse

        transform.up = direction; // faz com que o objeto  rotacione e aponte para a parte de cima do objeto 
    }

}
