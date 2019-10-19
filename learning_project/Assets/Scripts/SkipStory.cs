using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipStory : MonoBehaviour
{
    public void GotoMenuScene()
    {
        SceneManager.LoadScene("main_menu");
    }
}
