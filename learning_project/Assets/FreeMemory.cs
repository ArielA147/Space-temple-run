using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMemory : MonoBehaviour
{
    // consts //
    public int CLEAR_FRAME_RATE = 60;
    // end - consts //

    // members //
    public GameObject player;
    public int frameCounter;
    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        frameCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCounter == CLEAR_FRAME_RATE)
        {
            // find all the elements in the scene
            UnityEngine.Object[] gameObjects = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (UnityEngine.Object gameObject in gameObjects)
            {
                GameObject gameObjectInstance = (GameObject)gameObject;
                // special cases 
                if (gameObjectInstance.name == "lights" || gameObjectInstance.name.Contains("manager") || gameObjectInstance.name.Contains("sound"))
                {
                    continue;
                }
                // check if behind the ship (enought) - clear it and by so free the memory
                if (player.transform.position.x - gameObjectInstance.transform.position.x > 100)
                {
                    Destroy(gameObject);
                }
            }
            frameCounter = 0;
        }
        frameCounter += 1;
    }
}
