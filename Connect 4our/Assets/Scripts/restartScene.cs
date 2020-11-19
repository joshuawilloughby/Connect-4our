using UnityEngine;
using UnityEngine.SceneManagement;

public class restartScene : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene("Main");
    }
}
