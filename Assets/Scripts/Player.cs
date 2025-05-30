using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 300f;
    [SerializeField] TextMeshProUGUI vidaTexto;
    [SerializeField] Animator anim;
    [SerializeField] Animator Feetanim;
    [SerializeField] bala balaPrefab;
    [SerializeField] Transform PontoTiro;
    [SerializeField] timer timer;
    [SerializeField] GameObject painel;

    Rigidbody2D rb;

    bool vivo = true;
    int vida = 100;
    public bool atirando = false;

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim.Play("Player Idle");
        Feetanim.Play("Player feet idle");
    }
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (atirando == false)
            {
                shoot();
            }
        }
        MouseOrientation();
        vidaTexto.text = vida.ToString();

        if (vida <= 0)
        {
            gameOver();
        }

    }
    void FixedUpdate()
    {
        Movimentacao();
    }


    public void Movimentacao()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.linearVelocity = input.normalized * speed * Time.fixedDeltaTime;

        if(rb.linearVelocity.magnitude > 0)
        {
            anim.Play("Player move");
            Feetanim.Play("player feet move");
        }
        else
        {
            anim.Play("Player Idle");
            Feetanim.Play("Player feet idle");
        }

    }

    void MouseOrientation()
    {
        if (vivo == false) { 
            return; 
        }
        Vector3 mousePos = Input.mousePosition; //  Passa pro Vetor a posição do mouse na cena.
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); // Pega a posição do Mouse na camera

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y); // cria um vetor apontando do Player para o Mouse

        transform.up = direction; // faz com que o objeto  rotacione e aponte para a parte de cima do objeto 
    }


    public void levarDano(int dano)
    {
        vida -= dano;
    }

    void shoot()
    {
        atirando = true;
        timer.resetTimer();
        Instantiate(balaPrefab, PontoTiro.position, PontoTiro.rotation);

    }

    void gameOver()
    {
        vivo = false;
        painel.SetActive(true);
        Time.timeScale = 0;
        gameObject.SetActive(false);
    }
}
