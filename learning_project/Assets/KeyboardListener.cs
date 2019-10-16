using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene("main_menu");
        }
        else if (Input.GetKey(KeyCode.R) && SceneManager.GetActiveScene().name == "SampleScene") {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
