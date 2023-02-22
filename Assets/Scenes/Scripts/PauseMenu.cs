using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject player;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        ToggleAbilities(true);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        ToggleAbilities(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ToggleAbilities(bool toggle)
    {
        player.GetComponent<Movement>().enabled = toggle;
        player.GetComponent<QAbility>().enabled = toggle;
        player.GetComponent<EAbility>().enabled = toggle;
        player.GetComponent<RAbility>().enabled = toggle;
    }
}
