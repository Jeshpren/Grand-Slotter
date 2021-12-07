using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMouseClick : MonoBehaviour
{

    public ReelManager20 reelManager;

    [HideInInspector]
    public bool play = false;
    bool playPrev = false;

    bool mouseSpace = false;    // true for mouse, false for space

    Material playMat;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        playMat = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        //* shaderju morš nastau začetne vrednosti, ker si zapomne vrednosti iz prejšnga run-a
        playMat.SetFloat("_TimeAtClick", -10f);
        playMat.SetFloat("_Emission", 1f);
    }

    void Update()
    {
        HandleInput();

        
        if (play && playPrev)
            play = false;

        if (play)
        {
            //* nastau playButton shader property-je
            playMat.SetFloat("_TimeAtClick", Time.timeSinceLevelLoad);

            if (mouseSpace)
            {
                Ray ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
                Physics.Raycast(ray, out var hit);
                playMat.SetVector("_RippleCenter", hit.point);
                
            }
            else
            {
                playMat.SetVector("_RippleCenter", transform.position);
            }

        }


        playPrev = play;
    }

    void OnMouseDown() {
        if (!reelManager.winAnim && !reelManager.rolling)
        play = true;
        // mouseSpace = true;
    }

    public void EnableEmission()
    {
        //* nastau emission za play button
        playMat.SetFloat("_Emission", 1f);
    }
    public void DisableEmission()
    {
        // StartCoroutine(DisableEmissionDelay());
        //* ko začneš vrtet, sklop emission
        playMat.SetFloat("_Emission", 0f);
    }

    // IEnumerator DisableEmissionDelay()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     //* ko začneš vrtet, sklop emission
    //     playMat.SetFloat("_EmissionStrength", 0f);
    // }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !reelManager.winAnim && !reelManager.rolling)
        {
            play = true;
            // mouseSpace = false;
        }
    }
    
}
