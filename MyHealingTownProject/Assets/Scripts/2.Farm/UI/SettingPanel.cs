using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : MonoBehaviour
{
    public GameObject settingPanel;
    bool UIActive;

    private void Start()
    {
        settingPanel.SetActive(false);
        UIActive = false;
    }

    public void OpenSettingPanel()
    {
        UIActive = !UIActive;
        settingPanel.SetActive(UIActive);
    }

    public void ReturnStartUI()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
