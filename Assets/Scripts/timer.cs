using UnityEngine;

public class timer : MonoBehaviour
{
    [SerializeField] inimigo inimigo;
    [SerializeField] Player player;

    public float InimigoTime = 3.0f;
    public float PlayerTime = 0.2f;

    void Update()
    {

        InimigoTime -= Time.deltaTime;
        PlayerTime -= Time.deltaTime;

        if (InimigoTime <= 0.0f)
        {
            timerEndedInimigo();
        }
        if (PlayerTime <= 0.0f) { 
            timerEndedPlayer(); 
        }

    }

    public void resetTimer()
    {
        InimigoTime = 3.0f;
        PlayerTime = 0.3f;
    }

    void timerEndedInimigo()
    {
        if (inimigo != null) inimigo.attack();
    }

    void timerEndedPlayer()
    {
        if (player != null) player.atirando = false;
    }
}

