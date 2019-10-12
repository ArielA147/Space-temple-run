using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // consts //
    public const float FOLLOW_PLAYER_SPEED_FACTOR = 0.5f;
    public const int ACTIVE_DISTANCE = 50;
    public const float SPEED_BOOST = 5f;
    // end - consts //

    // members //
    public Rigidbody rb;
    public GameObject player;
    public int frameNumber;
    public int framesToKill;
    public int thisFrame;

    public bool startActive;
    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("player");
        rb.velocity = Vector3.zero;
        frameNumber = 0;
        framesToKill = 300;
        thisFrame = 0;
        startActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if need to activate the enemy
        if (!startActive && Vector3.Distance(gameObject.transform.position, player.transform.position) < ACTIVE_DISTANCE)
        {
            startActive = true;
        }
        
        // not active yet, don't run follow and die logic
        if (startActive)
        {
            followAndDie();
        }
    }

    /// <summary>
    /// Main enemy logic - follow player and die after sometime 
    /// </summary>
    public void followAndDie()
    {
        thisFrame += 1;

        if (frameNumber == 5)
        {
            Rigidbody playerVelocity = player.GetComponent<Player>().rb;
            Vector3 playerOverTime = new Vector3(player.transform.position.x + playerVelocity.velocity.x * FOLLOW_PLAYER_SPEED_FACTOR,
                player.transform.position.y + playerVelocity.velocity.y * FOLLOW_PLAYER_SPEED_FACTOR,
                player.transform.position.z + playerVelocity.velocity.z * FOLLOW_PLAYER_SPEED_FACTOR);
            Vector3 thisPoint = gameObject.transform.position;
            Vector3 nextPoint = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, SPEED_BOOST);
            rb.velocity = new Vector3(nextPoint.x - thisPoint.x,
                nextPoint.y - thisPoint.y,
                nextPoint.z - thisPoint.z);
            frameNumber = 0;
            gameObject.transform.LookAt(player.transform.position);
        }
        frameNumber += 1;

        if (framesToKill == thisFrame)
        {
            Destroy(gameObject);
        }
    }

}
