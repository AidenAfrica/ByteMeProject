using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadIntroductionScene()
    {
        SceneManager.LoadScene("introduction");
    }
}