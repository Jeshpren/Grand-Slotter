using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButtons : MonoBehaviour
{

    public ReelManager20 reelManager;
    public GameObject otherMode;

    Material matThis;
    Material matOther;

    void Start()
    {
        matThis = gameObject.GetComponent<MeshRenderer>().material;
        matOther = otherMode.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (!reelManager.winAnim && !reelManager.rolling)
        {
            if (Input.GetKeyDown("1") || Input.GetKeyDown(KeyCode.Keypad1))
            {

                if (transform.name == "ModeA Button")
                {
                    matThis.EnableKeyword("_EMISSION");
                    reelManager.easyMode = false;
                    // * These shouldn't be called like that, but they're called rearely so it doesn't really matter
                    reelManager.arrow1.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                    reelManager.arrow2.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                    reelManager.arrow3.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
                else
                {
                    matThis.DisableKeyword("_EMISSION");
                }
            }


            if (Input.GetKeyDown("2") || Input.GetKeyDown(KeyCode.Keypad2))
            {

                if (transform.name == "ModeA Button")
                {
                    matThis.DisableKeyword("_EMISSION");
                    reelManager.easyMode = true;
                    // * These shouldn't be called like that, but they're called rearely so it doesn't really matter
                    reelManager.arrow1.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                    reelManager.arrow2.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                    reelManager.arrow3.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                }
                else
                {
                    matThis.EnableKeyword("_EMISSION");
                }
            }


        }
    }



    void OnMouseDown()
    {
        if (!reelManager.winAnim && !reelManager.rolling)
        {
            matThis.EnableKeyword("_EMISSION");
            matOther.DisableKeyword("_EMISSION");

            if (transform.name == "ModeA Button")
            {
                reelManager.easyMode = false;
                // * These shouldn't be called like that, but they're called rearely so it doesn't really matter
                reelManager.arrow1.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                reelManager.arrow2.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                reelManager.arrow3.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
            else
            {
                reelManager.easyMode = true;
                reelManager.arrow1.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                reelManager.arrow2.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                reelManager.arrow3.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
                
        }
    }
}
