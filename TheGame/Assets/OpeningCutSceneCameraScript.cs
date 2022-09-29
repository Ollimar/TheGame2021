using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class OpeningCutSceneCameraScript : MonoBehaviour
{

    public float speed;
    public Vector3 direction;

    // PostProcessEffects
    public PostProcessVolume postProcess;
    public DepthOfField depthOfField;

    public float dofAmount = 13f;

    // Start is called before the first frame update
    void Start()
    {
        postProcess.profile.TryGetSettings(out depthOfField);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void MovingCamera()
    {
        depthOfField.focalLength.value = 9f;
    }
}
