using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    // consts //
    public Vector3 CAMERA_TO_STATS_DELTA_LOCATION = new Vector3(25.5f, -75.5f, 10);
    // end - consts //

    // members //
    public GameObject player;
    public GameObject stats;
    public Vector3 deltaPositionVector;
    private Vector3 offset;
    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        stats = GameObject.Find("stats");
        offset = new Vector3(-88, 17, 0);
        deltaPositionVector = new Vector3(-40.5f, 86.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position + deltaPositionVector;
        stats.transform.position = gameObject.transform.position + CAMERA_TO_STATS_DELTA_LOCATION;
    }

    // Taken from: https://learn.unity.com/tutorial/movement-basics?projectId=5c514956edbc2a002069467c&signup=true#
    private void LateUpdate()
	{
        transform.position = player.transform.position + offset;
    }
}
