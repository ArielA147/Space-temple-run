using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hacks : MonoBehaviour
{
    // members //
    public UpdateStats stats;
    public Player player;
    // score keys
    public bool isMpress = false;
    public bool isOpress = false;
    public bool isNpress = false;
    public bool isEpress = false;
    // time keys
    public bool isTpress = false;
    public bool isIpress = false;
    public bool isMpress2 = false;
    // space keys
    public bool isJpress = false;
    public bool isUpress = false;
    public bool isMpress3 = false;
    // pass not generated part keys
    public bool isPpress = false;
    public bool isApress = false;
    public bool isSpress = false;
    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("stats").GetComponent<UpdateStats>();
        player = GameObject.Find("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // money hack 
        if (Input.GetKey(KeyCode.M))
        {
            isMpress = true;
        }
        if (isMpress && Input.GetKey(KeyCode.O))
        {
            isOpress = true;
        }
        if (isOpress && Input.GetKey(KeyCode.N))
        {
            isNpress = true;
        }
        if (isNpress && Input.GetKey(KeyCode.E))
        {
            isEpress = true;
        }
        if (isEpress && Input.GetKey(KeyCode.Y))
        {
            isMpress = false;
            isNpress = false;
            isOpress = false;
            isEpress = false;
            player.score += 100;
        }
        // time hack
        if (Input.GetKey(KeyCode.T))
        {
            isTpress = true;
        }
        if (isTpress && Input.GetKey(KeyCode.I))
        {
            isIpress = true;
        }
        if (isIpress && Input.GetKey(KeyCode.M))
        {
            isMpress2 = true;
        }
        if (isMpress2 && Input.GetKey(KeyCode.E))
        {
            isTpress = false;
            isIpress = false;
            isMpress2 = false;
            player.timeLeftInSeconds += 30;
        }
        // space hack 
        if (Input.GetKey(KeyCode.J))
        {
            isJpress = true;
        }
        if (isJpress && Input.GetKey(KeyCode.U))
        {
            isUpress = true;
        }
        if (isUpress && Input.GetKey(KeyCode.M))
        {
            isMpress3 = true;
        }
        if (isMpress3 && Input.GetKey(KeyCode.P))
        {
            isJpress = false;
            isUpress = false;
            isMpress3 = false;
            player.transform.position += new Vector3(150, 1, 0);
        }
        // pass toturial hack 
        if (Input.GetKey(KeyCode.P))
        {
            isPpress = true;
        }
        if (isPpress && Input.GetKey(KeyCode.A))
        {
            isApress = true;
        }
        if (isApress && Input.GetKey(KeyCode.S))
        {
            isSpress = true;
        }
        if (isSpress && Input.GetKey(KeyCode.S) && player.transform.position.x < 800)
        {
            isPpress = false;
            isApress = false;
            isSpress = false;
            player.transform.position = new Vector3(800, 3, 0);
        }
    }
}
