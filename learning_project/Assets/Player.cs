using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    // consts //
    public float moveSpeed = 7.5f;
    public float turnSpeed = 50f;
    public float smooth = 5.0f;
    public float tiltAngle = 60.0f;
    // end - consts //

    // members //
    // this objects
    public Renderer renderer;
    public Rigidbody rb;
    public Vector3 lastPosition = Vector3.zero;

    // mechanics
    public long hitTime;

    // stats
    private long _lastHitTime;
    public GameObject backWall;
    private float startPos;
    public float timeLeftInSeconds;
    public int score;
    private float distance;
    private float boostDeltaTimeMiliseconds = 500;

    // sounds 
    public AudioSource coinSound;
    public AudioSource clockSound;
    public AudioSource bombSound;
    public AudioSource enemySound;

    // final message
    public GameObject finalMessagePanael;

    // game minupulated by player
    public UpdateStats gameBoard;
    public GameObject FloatingDamagePrefab;

    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        // init player components
        rb = gameObject.GetComponent<Rigidbody>();
        renderer = gameObject.GetComponent<Renderer>();

        // init mechanisem members
        hitTime = DateTime.Now.Ticks;
        _lastHitTime = DateTime.Now.Ticks;

        // init stats
        gameBoard = GameObject.Find("stats").GetComponent<UpdateStats>();
        distance = 0;
        timeLeftInSeconds = 45;
        score = 0;
        startPos = backWall.transform.position.x;

        // init sounds 
        coinSound = GameObject.Find("coin_sound").GetComponent<AudioSource>();
        clockSound = GameObject.Find("clock_sound").GetComponent<AudioSource>();
        bombSound = GameObject.Find("bomb_sound").GetComponent<AudioSource>();
        enemySound = GameObject.Find("enemy_sound").GetComponent<AudioSource>();

        // init final message
        finalMessagePanael = GameObject.Find("final_message");
        
        finalMessagePanael.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // update time
        timeLeftInSeconds -= Time.deltaTime;

        // manage first frame speed
        rb.velocity = new Vector3(rb.velocity.x + 0.5f, rb.velocity.y, rb.velocity.z);

        // stablize rotation
        Quaternion target = Quaternion.Euler(0, 90, Input.GetAxis("Horizontal") * tiltAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        /* manage player actions */
        // boost 
        if (Input.GetKey(KeyCode.Space) && new TimeSpan(DateTime.Now.Ticks - hitTime).Milliseconds > boostDeltaTimeMiliseconds)
        {
            rb.velocity = new Vector3(rb.velocity.x + 10f, rb.velocity.y + 0.2f, rb.velocity.z);
            hitTime = DateTime.Now.Ticks;
        }

        // reset game
        if (Input.GetKey(KeyCode.R))
        {
            RestartGame();
        }
        
        // move right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // change speed
            rb.velocity = new Vector3(rb.velocity.x - 0.1f, rb.velocity.y, rb.velocity.z - moveSpeed * Time.deltaTime);
        }

        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // change speed
            rb.velocity = new Vector3(rb.velocity.x - 0.1f, rb.velocity.y, rb.velocity.z + moveSpeed * Time.deltaTime);
        }

        /* manage player actions' properties */ 

        // make sure the player is not too fast
        if (rb.velocity.magnitude > 50)
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.95f, 
                rb.velocity.y * 0.95f, 
                rb.velocity.z * 0.95f);
        }

        // stop game if not time 
        if (timeLeftInSeconds <= 0 || score < 0)
        {
            rb.velocity = Vector3.zero;
            timeLeftInSeconds = 0;
            // TODO: show finish screen
            gameOverMessage(true);
            return;
        }
        else
        {
            gameOverMessage(false);
        }

        this.distance = (float)Math.Truncate((transform.position.x - this.startPos) * 100 / 100);
    }

    public float GetDistance() {
        return this.distance;
    }

    public void gameOverMessage(bool show_message)
    {
        Vector3 offset = new Vector3(0, 7, 0);
        finalMessagePanael.transform.position = gameObject.transform.position + offset;
        if (show_message)
        {
            finalMessagePanael.gameObject.SetActive(true);
            StartCoroutine(GameOverReset());
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    public IEnumerator GameOverReset()
    {
        yield return new WaitForSeconds(5);
        RestartGame();
    }

    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // if toach the ends then stops
        if (collision.gameObject.name == "wall")
        {
            rb.velocity *= 0.5f;
        }

        // hit a ball is a good thing
        if (collision.gameObject.name == "coin")
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            coinSound.Play();
            Destroy(collision.gameObject);
            // TODO: change score incresment using the coin's inner score member
            score += 1;
        }

        // hit a bomb is a bad thing
        if (collision.gameObject.name == "bomb")
        {
            System.Random random = new System.Random();
            Vector3 hitVector = new Vector3(random.Next(10, 39) / 10, random.Next(10, 39) / 10, random.Next(10, 39) / 10);
            rb.velocity = new Vector3(rb.velocity.x + hitVector.x, 
                rb.velocity.y + hitVector.y, 
                rb.velocity.z + hitVector.z);
            bombSound.Play();
            Destroy(collision.gameObject);
            // TODO: change time decrease using the bomb's inner score member
            timeLeftInSeconds -= 5;
        }

        // hit a ball is a good thing
        if (collision.gameObject.name == "clock")
        {
            rb.velocity = new Vector3(rb.velocity.x + 3, rb.velocity.y, rb.velocity.z);
            clockSound.Play();
            Destroy(collision.gameObject);
            // TODO: change time improve in using the clock's inner score member
            timeLeftInSeconds += 5;
        }

        // hit a ball is a good thing
        if (collision.gameObject.name == "arrow")
        {
            rb.velocity = new Vector3(rb.velocity.x + 120f, rb.velocity.y, rb.velocity.z);
            clockSound.Play();
            Destroy(collision.gameObject);
        }

        // hit a ball is a good thing
        if (collision.gameObject.name == "enemy")
        {
            float reduceSpeedFactor = 0.8f;
            rb.velocity *= reduceSpeedFactor;
            // TODO: change reduce in score using the enemy's inner score member
            enemySound.Play();
            Destroy(collision.gameObject);
            score -= 3;
        }
    }

    public void OnCollisionExit(Collision collision)
    {

    }
}
