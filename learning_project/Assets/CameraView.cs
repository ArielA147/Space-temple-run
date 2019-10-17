using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    // consts //
    private Vector3 CAMERA_TO_STATS_DELTA_LOCATION = new Vector3(8.0f, 10.0f, 10.0f);
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
        deltaPositionVector = new Vector3(-95.5f, 17.5f, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.position = player.transform.position + deltaPositionVector;
        stats.transform.position = player.transform.position + this.CAMERA_TO_STATS_DELTA_LOCATION;
    }
}
