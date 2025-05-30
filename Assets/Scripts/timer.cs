using UnityEngine;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
    [SerializeField] private Inimigo inimigo;
    [SerializeField] private Player player;

     public float inimigoTime = 3.0f;
     public float playerTime = 0.2f;

    private void Update()
    {

        inimigoTime -= Time.deltaTime;
        playerTime -= Time.deltaTime;

        if (inimigoTime <= 0.0f)
        {
            timerEndedInimigo();
        }
        if (playerTime <= 0.0f) { 
            timerEndedPlayer(); 
        }

    }

    public void resetTimer()
    {
        inimigoTime = 3.0f;
        playerTime = 0.3f;
    }

    private void timerEndedInimigo()
    {
        if (inimigo != null) inimigo.attack();
    }

    private void timerEndedPlayer()
    {
        if (player != null) player.atirando = false;
    }
}

