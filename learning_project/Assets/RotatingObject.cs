using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    // members //
    public float spinSpeed = 50f;
    // end - members //
     // Update is called once per frame
    void Update()
    {
        // find all the elements in the scene
        UnityEngine.Object[] gameObjects = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (UnityEngine.Object gameObject in gameObjects)
        {
            GameObject gameObjectInstance = (GameObject)gameObject;
            switch (gameObjectInstance.name)
            {
                case "clock":
                case "bomb":
                case "coin":
                    gameObjectInstance.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
                    break;
                default:
                    break;

            }
        }
    }
}
