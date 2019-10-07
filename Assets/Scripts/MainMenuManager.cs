using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("A pressed !");
            SceneManager.LoadScene(1);
        }
    }

    public void SwitchScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
