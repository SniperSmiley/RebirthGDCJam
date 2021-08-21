using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoFix : MonoBehaviour
{

    public VideoPlayer player;
    

    // Start is called before the first frame update
    void Start()
    {
         string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "11.mov");
        player.url = filePath;

        player.renderMode = VideoRenderMode.RenderTexture;
        player.targetCameraAlpha = 1.0f;
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
