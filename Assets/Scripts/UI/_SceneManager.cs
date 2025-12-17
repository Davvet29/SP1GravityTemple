using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeScene("GameScene");
        }
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
