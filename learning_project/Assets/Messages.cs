using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    // members //
    public GameObject player;
    public GameObject firstMessage;
    public GameObject clockMessage;
    public GameObject coinMessage;
    public GameObject bombMessage;
    public GameObject enemyMessage;
    public GameObject controlMessage;
    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        firstMessage = GameObject.Find("first_message");
        clockMessage = GameObject.Find("message_time");
        coinMessage = GameObject.Find("message_coins");
        bombMessage = GameObject.Find("message_bomb");
        enemyMessage = GameObject.Find("message_enemy");
        controlMessage = GameObject.Find("control_message");

    }

    // Update is called once per frame
    void Update()
    {
        if (firstMessage != null && player.transform.position.x > 10)
        {
            Destroy(firstMessage);
        }
        if (clockMessage != null && player.transform.position.x > 20)
        {
            Destroy(clockMessage);
        }
        if (coinMessage != null && player.transform.position.x > 65)
        {
            Destroy(coinMessage);
        }
        if (bombMessage != null && player.transform.position.x > 115)
        {
            Destroy(bombMessage);
        }
        if (enemyMessage != null && player.transform.position.x > 165)
        {
            Destroy(enemyMessage);
        }
        if (controlMessage != null && player.transform.position.x > 205)
        {
            Destroy(controlMessage);
        }
    }
}
