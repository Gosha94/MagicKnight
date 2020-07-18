using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Image menu;
    [SerializeField]
    private Button startGameBtn;
    [SerializeField]
    private Button quitGameBtn;
    private bool started;

    public bool isStarted
    {

        get { return started; }

        set
        {
            if (value) started = value;
            LoseGame();
        }
    }
    // Объявляем Singleton, единичный экземпляр на все приложение
    public static MainMenu instanse;

    private void Start()
    {
        instanse = this;
        startGameBtn.onClick.AddListener(delegate
        {
            menu.gameObject.SetActive(false);
            started = true;
        });

        quitGameBtn.onClick.AddListener(delegate
        {
            Application.Quit();
        });
    }
    private void LoseGame()
    {        
        SceneManager.LoadScene(0);
    }
}
