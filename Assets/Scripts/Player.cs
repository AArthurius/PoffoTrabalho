using System;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour
{
    private class Wrapper
    {
        public int[] array;
    }
    

    [SerializeField] private float speed = 300f;
    [SerializeField] private TextMeshProUGUI vidaTexto;
    [SerializeField] private TextMeshProUGUI scoreTexto;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator Feetanim;
    [SerializeField] private bala balaPrefab;
    [SerializeField] private Transform PontoTiro;
    [SerializeField] private timer timer;
    [SerializeField] private GameObject painel;

    private Rigidbody2D rb;

    private int vida = 30;
    private bool vivo = true;
    public bool atirando = false;
    private int currentScore;

    public static Player instance { get; private set; }
    private static int[] highScores;
    private const string HIGH_SCORES_SAVE_FILE_PATH = "/highscores.json";

    public static int[] getHighScores() => highScores;
    
    private void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        LoadHighScores();
        
        currentScore = 0;
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim.Play("Player Idle");
        Feetanim.Play("Player feet idle");
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
        Instantiate(balaPrefab, PontoTiro.position, PontoTiro.rotation);

    }
    
    public void UpdateScore(int amount)
    {
        currentScore += amount;
        scoreTexto.text = currentScore.ToString();
    }

    //Quando o GameOver for adicionado use essa função para adicionar a pontuação para o highScore, como ainda n temos
    //menu ainda n fiz o texto do highScore, mas isso vai ser fácil de fazer.
    public void SaveHighScores()
    {
        //Um monte de maracutáia pra n usar List<> pq Array é mais eficiente
        for(int i = 0; i < highScores.Length; i++)
        {
            if (highScores[i] >= currentScore) continue;
            
            if (i != highScores.GetUpperBound(0))
            {
                for (int j = highScores.GetUpperBound(0); j > i; j--)
                {
                    Debug.Log(j);
                    highScores[j] = highScores[j - 1];
                }
            }
            highScores[i] = currentScore;
            Debug.Log(highScores[i]);
            break;
        }
        
        Wrapper wrapper = new Wrapper();
        wrapper.array = highScores;
        
        string json = JsonUtility.ToJson(wrapper);
        System.IO.File.WriteAllText(Application.persistentDataPath + HIGH_SCORES_SAVE_FILE_PATH, json);
        Debug.Log(highScores);
        Debug.Log(json);
        Debug.Log("saved");
    }

    public static void LoadHighScores()
    {
        highScores = new int[10];
        
        if (File.Exists(Application.persistentDataPath + HIGH_SCORES_SAVE_FILE_PATH))
        {
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(System.IO.File.ReadAllText(Application.persistentDataPath + HIGH_SCORES_SAVE_FILE_PATH));

            if (wrapper?.array == null) return;
            
            for (int i = 0; i < wrapper.array.Length; i++)
            {
                if (i >= 10) break;
                highScores[i] = wrapper.array[i];
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
