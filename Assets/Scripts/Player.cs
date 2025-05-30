using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 300f;
    [SerializeField] private TextMeshProUGUI vidaTexto;
    [SerializeField] private TextMeshProUGUI scoreTexto;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator Feetanim;
    [SerializeField] private bala balaPrefab;
    [SerializeField] private Transform PontoTiro;
    [SerializeField] private timer timer;

    private Rigidbody2D rb;

    private int vida = 100;
    public bool atirando = false;

    public static int currentScore;
    private static int[] highScores = new int[3];
    public static Player instance {get; private set;}

    private void Start()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        scoreTexto.text = currentScore.ToString();
        currentScore = 0;
        rb = GetComponent<Rigidbody2D>();
        anim.Play("Player Idle");
        Feetanim.Play("Player feet idle");
    }

    private void Update()
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

    }

    private void FixedUpdate()
    {
        Movimentacao();
    }


    public void Movimentacao()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.linearVelocity = input.normalized * (speed * Time.fixedDeltaTime);

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

    private void MouseOrientation()
    {
        Vector3 mousePos = Input.mousePosition; //  Passa pro Vetor a posi��o do mouse na cena.
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); // Pega a posi��o do Mouse na camera
                    //não tem que normalizar?
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y); // cria um vetor apontando do Player para o Mouse

        transform.up = direction; // faz com que o objeto  rotacione e aponte para a parte de cima do objeto 
    }


    public void levarDano(int dano)
    {
        vida -= dano;
    }

    private void shoot()
    {
        atirando = true;
        timer.resetTimer();
        Instantiate(balaPrefab, PontoTiro.position, PontoTiro.rotation);

    }

    public void UpdateScore(int amount)
    {
        currentScore += amount;
        scoreTexto.text = currentScore.ToString();
    }

    //Quando o GameOver for adicionado use essa função para adicionar a pontuação para o highScore, como ainda n temos
    //menu ainda n fiz o texto do highScore, mas isso vai ser fácil de fazer.
    private void AddToHighScores(int score)
    {
        //Um monte de maracutáia pra n usar List<> pq Array é mais eficiente
        for(int i = 0; i < highScores.Length; i++)
        {
            if (highScores[i] >= score) continue;
            
            if (i != highScores.Length - 1)
            {
                for (int j = highScores.Length - 1; j >= i; j--)
                {
                    highScores[j - 1] = highScores[j];
                }
            }
            
            highScores[i] = score;
        }
    }
}
