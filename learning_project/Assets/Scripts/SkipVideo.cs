using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipVideo : MonoBehaviour
{
    public void GotoStoryScene()
    {
        SceneManager.LoadScene("Story");
    }
}
