using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManger : MonoBehaviour
{


    bool paused = false;


    // Update is called once per frame
    void Update()
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
}
