using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin8 : MonoBehaviour
{
    // * Variables

    [Header("Main")]    //* Settings Main
    public float deceleration = 0.05f;
    public int minSignTurns = 12;  // obrne naj se za najmn 12 znakov.. torej za krog pa pou
    public int maxSignTurns = 24;  // največ 4.5 kroge

    [Header("Other Reels")] //* Settings Other Reels
    public Spin reelSpin1;
    public Spin reelSpin2;
    public Spin reelSpin3;
    public Spin reelSpin4;

    // [Header("Angles & Coresponding Signs")]
    // public string sign22_5 = "A";
    // public string sign67_5 = "B";
    // public string sign112_5 = "C";
    // public string sign157_5 = "D";
    // public string sign202_5 = "E";
    // public string sign247_5 = "F";
    // public string sign292_5 = "G";
    // public string sign337_5 = "H";

    public GameObject[] signObjects = new GameObject[8];
    [HideInInspector]
    public Material[] signMaterials = new Material[8];
    [HideInInspector]
    public string[] signNames = new string[8];

    [HideInInspector]
    public float angleAdjusted;
    
    [HideInInspector]
    public float spinSpeed;

    int signTurns;
    bool calculateSpin = false;
    bool calculateSpinPrev = false;
    float timeCounter = 0f;

    float initialRotation;
    float currentRotation;
    float endRotation;


    // * v = sqrt(2*a*d).... izračun končne hitrosti ob znanem pospešku in razdlaji *ali za moj primer* izračun začetne hitrosti ob znanem pojemku in številu obratov
    // * končna rotacija = začetna rotacija + signTurns*45°

    // [HideInInspector]
    // public string top;
    // public string middle;
    // public string bot;
    private void Awake() {

        //* iz vseh sign objectou pober materiale in ih shran v signMaterials
        for (int i = 0; i < signObjects.Length; i++)
        {
            signMaterials[i] = signObjects[i].GetComponent<MeshRenderer>().material;
            // signMaterials[i] = new Material(signMaterials[i]);   // ne rabš delat novih, ker očinu unity poskrbi da se aplicera na vsazga posebej
            // if (transform.name == "Reel Numbered 1")
            // {
            //     signMaterials[i].EnableKeyword("_EMISSION");
            // }
            //* shran imena znakov
            signNames[i] = signObjects[i].name;
        }



    }

    private void Start() 
    {
        float randomSign = Random.Range(1, 8);
        initialRotation = 22.5f + randomSign * 45;
        transform.rotation = Quaternion.Euler(0f, 90f, initialRotation);
        currentRotation = initialRotation;
    }

    private void Update() 
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        if (calculateSpin)
        {            
            SpinReel();
        }
        calculateSpinPrev = calculateSpin;
    }


    void HandleInput()
    {
        // *če so se vsi nehal vrtet lahku zaženš
        if (Input.GetKeyDown(KeyCode.Space) && spinSpeed == 0f && reelSpin1.spinSpeed == 0 && reelSpin2.spinSpeed == 0 && reelSpin3.spinSpeed == 0 && reelSpin4.spinSpeed == 0)
        {
            calculateSpin = true;
            timeCounter = 0;
        }
    }

    void SpinReel()
    {
        // if (transform.gameObject.name == "Reel Numbered 1")
        // {
        //     Debug.Log(Mathf.Round(timeCounter));
        // }

        // zračunej samu taprvič
        if (!calculateSpinPrev)
        {

            signTurns = Random.Range(minSignTurns, maxSignTurns);
            spinSpeed = -Mathf.Sqrt(2 * deceleration * (signTurns * 45));    // en signTurn je enak 45° (pri oktagonu)
            endRotation = initialRotation - signTurns * 45;
        }

        // štet morš tut trenutno rotacijo because circle
        currentRotation += spinSpeed;

        transform.Rotate(0, 0, spinSpeed);
        if (spinSpeed < 0f)
        {
            // spinSpeed -= deceleration;
            spinSpeed += deceleration;
        }

        // * če si se zavrtu do konca --> ponastau
        if ((endRotation - currentRotation >= 0f))
        {
            // lock the angle
            transform.localRotation = Quaternion.Euler(0, 90, endRotation);
            // Debug.Log("End rotation reached");
            spinSpeed = 0f;
            currentRotation = endRotation;
            initialRotation = endRotation;

            // TODO: zrihtej obično zmago
            // * poprau kot
            angleAdjusted = endRotation % 360f - 22.5f;   // "-" prva vrstica
            if (angleAdjusted < 0f)
                angleAdjusted += 360f;

            // * shran kote za ReelManagerja ****(ne dela, dela pa če zračunaš znotrej ReelManager.cs)
            // top = signs[(int)(((angleAdjusted) / 45f) % 8)];
            // middle = signs[(int)(((angleAdjusted + 45f) / 45f) % 8)];
            // bot = signs[(int)(((angleAdjusted + 90f) / 45f) % 8)];
            // Debug.Log("1: " + signs[(int)((angleAdjusted / 45f) % 8)] + ", 2: " + signs[(int)(((angleAdjusted + 45f) / 45f) % 8)] + ", 3: " + signs[(int)(((angleAdjusted + 90f) / 45f)) % 8]);

            calculateSpin = false;
        }

        timeCounter += Time.fixedDeltaTime;

    }

}
