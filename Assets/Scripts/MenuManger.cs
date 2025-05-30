using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManger : MonoBehaviour
{
    private bool paused = false;
    
    [SerializeField] private TextMeshProUGUI highScoreText;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            Time.timeScale = 1;
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    public void jogar()
    {
        SceneManager.LoadScene(0);
    }

    public void menu()
    {
        SceneManager.LoadScene(1);
    }

    public void Start()
    {
        Player.LoadHighScores();
        
        if (highScoreText)
            if (Player.getHighScores()[0] > 0)
            {
                string text = "";
                int [] highScores = Player.getHighScores();
                for (int i = 0; i < highScores.Length; i++)
                {
                    if (highScores[i] == 0) break;
                    text += $"{i+1}. {highScores[i]}\n";
                }
                highScoreText.text = text;
            }
            else
                highScoreText.text = "";
    }
}
