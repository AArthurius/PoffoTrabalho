using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour
{
    private class Wrapper
    {
        public int[] Array;
    }
    

    [SerializeField] private float speed = 300f;
    [SerializeField] private TextMeshProUGUI vidaTexto;
    [SerializeField] private TextMeshProUGUI scoreTexto;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator feetanim;
    [SerializeField] private Bala balaPrefab;
    [SerializeField] private Transform pontoTiro;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject painel;

    private Rigidbody2D rb;

    private int vida = 100;
    private bool vivo = true;
    public bool atirando = false;
    private int currentScore;

    public static Player Instance { get; private set; } //por temos uma referência estática a esse script, podemos acessá-lo de qualquer lugar.
    private static int[] highScores;
    private const string HIGH_SCORES_SAVE_FILE_PATH = "/highscores.json";

    public static int[] getHighScores() => highScores;
    
    private void Start()
    {
        //essa classe é um singleton, apenas uma instância dela pode existir,
        //para garantir isso guardamos a instância em uma variável estática e
        //checamos se a instância atual é a instância guardada.
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        LoadHighScores();
        
        currentScore = 0;
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim.Play("Player Idle");
        feetanim.Play("Player feet idle");
        scoreTexto.text = currentScore.ToString();
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

        if (vida <= 0)
        {
            gameOver();
        }

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
            feetanim.Play("player feet move");
        }
        else
        {
            anim.Play("Player Idle");
            feetanim.Play("Player feet idle");
        }

    }

    private void MouseOrientation()
    {
        if (vivo == false) { 
            return; 
        }
        Vector3 mousePos = Input.mousePosition; //  Passa pro Vetor a posi��o do mouse na cena.
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); // Pega a posi��o do Mouse na camera

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
        Instantiate(balaPrefab, pontoTiro.position, pontoTiro.rotation);

    }
    
    public void UpdateScore(int amount)
    {
        currentScore += amount;
        scoreTexto.text = currentScore.ToString();
    }

    public void SaveHighScores()
    {
        //loopar pelo highscore
        for(int i = 0; i < highScores.Length; i++)
        {
            if (highScores[i] >= currentScore) continue;
            
            if (i != highScores.GetUpperBound(0))
            {
                //loopar pelo highscore do último ao primeiro, tornando o número atual igual ao próximo número
                //basicamente movendo os números 1 para a direita
                for (int j = highScores.GetUpperBound(0); j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                }
            }
            highScores[i] = currentScore;
            break; //já achamos a posição da pontuação atual, não há necessidade de loopar de novo
        }
        //o wrapper é uma classe apenas com um array int[], pois json não serializa int[]
        Wrapper wrapper = new Wrapper();
        wrapper.Array = highScores;
        
        //transforma o wrapper em um texto .json para que possa ser salvo
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(Application.persistentDataPath + HIGH_SCORES_SAVE_FILE_PATH, json);
    }

    public static void LoadHighScores()
    {
        highScores = new int[10];
        
        if (File.Exists(Application.persistentDataPath + HIGH_SCORES_SAVE_FILE_PATH))
        {
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(System.IO.File.ReadAllText(Application.persistentDataPath + HIGH_SCORES_SAVE_FILE_PATH));

            if (wrapper?.Array == null) return;
            
            //se por algum motivo tiver mais números do que o necessário no wrapper esse for resolve isso
            for (int i = 0; i < wrapper.Array.Length; i++)
            {
                if (i >= 10) break;
                highScores[i] = wrapper.Array[i];
            }
        }
    }

    private void gameOver()
    {
        SaveHighScores();
        vivo = false;
        painel.SetActive(true);
        Time.timeScale = 0;
        gameObject.SetActive(false);
    }
}
