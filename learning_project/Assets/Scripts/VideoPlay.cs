using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlay : MonoBehaviour
{
    public UnityEngine.Video.VideoClip videoClip;

    // Start is called before the first frame update
    void Start()
    {
        var videoPlayer = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        // var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        // var audioSource = gameObject.AddComponent<AudioSource>();

        // videoPlayer.playOnAwake = false;
        // videoPlayer.clip = videoClip;
        // videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        // videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        // videoPlayer.targetMaterialProperty = "_MainTex";
        // videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
        // videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"Guardians of the Galaxy Prison Fight.mp4"); 
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {

     if (Input.GetButtonDown("Jump"))
        {
            var vp = GetComponent<UnityEngine.Video.VideoPlayer>();

            if (vp.isPlaying)
            {
                vp.Pause();
            }
            else
            {
                vp.Play();
            }

        }
        
    if(Input.GetKeyDown(KeyCode.Escape) ){
            SceneManager.LoadScene("Story");
        }

    // if(Input.GetKeyDown(KeyCode.LeftArrow)){
    //         SceneManager.LoadScene("DisplayingCharacters");
    //     }

    if(Input.GetKeyDown(KeyCode.RightArrow)){
            SceneManager.LoadScene("Story");
        }
    }
}
