using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelManager20 : MonoBehaviour
{
    [Header("Main")]
    public bool easyMode = true;

    [Header("Reel Transforms")]
    public Transform reel1;
    public Transform reel2;
    public Transform reel3;
    public Transform reel4;
    public Transform reel5;

    [Header("Play Button")]
    public DetectMouseClick detectMouseClick;

    [Header("Winning UI")]
    public GameObject winningUiA;
    public GameObject winningUiB;
    TMPro.TextMeshProUGUI winningUiTmpA;
    TMPro.TextMeshProUGUI winningUiTmpB;

    [Header("Arrows")]
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;

    [Header("Mode Buttons")]
    public GameObject modeA;
    public GameObject modeB;
    Material modeAMat;
    Material modeBMat;

    Spin20 spin1;
    Spin20 spin2;
    Spin20 spin3;
    Spin20 spin4;
    Spin20 spin5;

    Material[,] mats = new Material[5, 20];



    // bool won = false;
    [HideInInspector]
    public bool rolling = false;
    bool rollingPrev = false;

    [HideInInspector]
    public bool winAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        winningUiTmpA = winningUiA.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        winningUiTmpB = winningUiB.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        spin1 = reel1.GetComponent<Spin20>();
        spin2 = reel2.GetComponent<Spin20>();
        spin3 = reel3.GetComponent<Spin20>();
        spin4 = reel4.GetComponent<Spin20>();
        spin5 = reel5.GetComponent<Spin20>();

        //* shran vse materiale v en 2d array
        for (int i = 0; i < mats.GetLength(1); i++)
        {
            mats[0,i] = spin1.signMaterials[i];
            mats[1,i] = spin2.signMaterials[i];
            mats[2,i] = spin3.signMaterials[i];
            mats[3,i] = spin4.signMaterials[i];
            mats[4,i] = spin5.signMaterials[i];
        }

        modeAMat = modeA.GetComponent<MeshRenderer>().material;
        modeBMat = modeB.GetComponent<MeshRenderer>().material;
        // * nastau leve puščiče in gumba glede na mode
        if (easyMode)
        {
            modeAMat.DisableKeyword("_EMISSION");
            modeBMat.EnableKeyword("_EMISSION");
            arrow1.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            arrow2.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            arrow3.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }
        else
        {
            modeAMat.EnableKeyword("_EMISSION");
            modeBMat.DisableKeyword("_EMISSION");
            arrow1.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            arrow2.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            arrow3.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate() {

        // float reelRot1 = reel1.rotation.eulerAngles.z;
        // float reelRot2 = reel2.rotation.eulerAngles.z;
        // float reelRot3 = reel3.rotation.eulerAngles.z;
        // float reelRot4 = reel4.rotation.eulerAngles.z;
        // float reelRot5 = reel5.rotation.eulerAngles.z;

        if (rolling && rollingPrev && spin1.spinSpeed == 0f && spin2.spinSpeed == 0f && spin3.spinSpeed == 0f && spin4.spinSpeed == 0f && spin5.spinSpeed == 0f)
        {
            //* sklop rolling zvok
            FindObjectOfType<AudioManager>().Stop("Rolling");
            // FindObjectOfType<AudioManager>().Play("StopRolling", 0f);

            // * shran indexe vidnih znakov
            int idx1_top = (int)(((spin1.angleAdjusted) / 18f) % 20);
            int idx1_middle = (int)(((spin1.angleAdjusted + 18f) / 18f) % 20);
            int idx1_bot = (int)(((spin1.angleAdjusted + 36f) / 18f) % 20);
            int idx2_top = (int)(((spin2.angleAdjusted) / 18f) % 20);
            int idx2_middle = (int)(((spin2.angleAdjusted + 18f) / 18f) % 20);
            int idx2_bot = (int)(((spin2.angleAdjusted + 36f) / 18f) % 20);
            int idx3_top = (int)(((spin3.angleAdjusted) / 18f) % 20);
            int idx3_middle = (int)(((spin3.angleAdjusted + 18f) / 18f) % 20);
            int idx3_bot = (int)(((spin3.angleAdjusted + 36f) / 18f) % 20);
            int idx4_top = (int)(((spin4.angleAdjusted) / 18f) % 20);
            int idx4_middle = (int)(((spin4.angleAdjusted + 18f) / 18f) % 20);
            int idx4_bot = (int)(((spin4.angleAdjusted + 36f) / 18f) % 20);
            int idx5_top = (int)(((spin5.angleAdjusted) / 18f) % 20);
            int idx5_middle = (int)(((spin5.angleAdjusted + 18f) / 18f) % 20);
            int idx5_bot = (int)(((spin5.angleAdjusted + 36f) / 18f) % 20);
            // * fetchej 3 vidne znake vsazga reela
            string reel1_top = spin1.signNames[idx1_top];
            string reel1_middle = spin1.signNames[idx1_middle];
            string reel1_bot = spin1.signNames[idx1_bot];
            string reel2_top = spin2.signNames[idx2_top];
            string reel2_middle = spin2.signNames[idx2_middle];
            string reel2_bot = spin2.signNames[idx2_bot];
            string reel3_top = spin3.signNames[idx3_top];
            string reel3_middle = spin3.signNames[idx3_middle];
            string reel3_bot = spin3.signNames[idx3_bot];
            string reel4_top = spin4.signNames[idx4_top];
            string reel4_middle = spin4.signNames[idx4_middle];
            string reel4_bot = spin4.signNames[idx4_bot];
            string reel5_top = spin5.signNames[idx5_top];
            string reel5_middle = spin5.signNames[idx5_middle];
            string reel5_bot = spin5.signNames[idx5_bot];


            // * Winning if middle row is aligned
            //*
            if (!easyMode)
            {
                // //* top row
                // if (reel1_top == reel2_top && reel1_top == reel3_top)
                // // if (spin1.top == spin2.top && spin1.top == spin3.top)
                // {
                //     Debug.Log("****** YOU HAVE WON; top row:\n" + reel1_top + " " + reel2_top + " " + reel3_top + "\no o o\no o o");
                //     // won = true;
                // }
                //* middle row
                if (reel1_middle == reel2_middle && reel1_middle == reel3_middle)
                // if (spin1.middle == spin2.middle && spin1.middle == spin3.middle)
                {
                    int winCase = 0;
                    // Debug.Log("****** YOU HAVE WON; middle row:\no o o" + reel1_middle + " " + reel2_middle + " " + reel3_middle + "\no o o");

                    if (reel1_middle == reel4_middle && reel1_middle == reel5_middle)
                    {
                        //* YOU DA MAN (1 in 10.000 chance)
                        Debug.Log("HOLY JESUS, YOU JUST WON THE GRAND PRIZE (1 in 10.000 chance)");
                        winCase = 6;

                    }
                    else if (reel1_middle == reel4_middle)
                    {
                        //* (1 in 1.000 chance)
                        Debug.Log("Won with 4 signs (1 in 1.000 chance)");
                        winCase = 4;
                    }
                    else if (reel1_middle == reel5_middle)
                    {
                        //* (1 in 1.000 chance)
                        Debug.Log("Won with 4 signs (1 in 1.000 chance)");
                        winCase = 5;
                    }
                    else
                    {
                        //* (1 in 100 chance)
                        Debug.Log("Won with 3 signs (1 in 100 chance)");
                        winCase = 3;
                    }

                    StartCoroutine(WinningAnimBasic(winCase, idx1_middle, idx2_middle, idx3_middle, idx4_middle, idx5_middle));

                }
                else
                {
                    //* ko se ustau, sklop emission (če nis zmagou)
                    detectMouseClick.EnableEmission();
                }
                // //* bot row
                // if (reel1_bot == reel2_bot && reel1_bot == reel3_bot)
                // // if (spin1.bot == spin2.bot && spin1.bot == spin3.bot)
                // {
                //     Debug.Log("****** YOU HAVE WON; bot row:\no o o\no o o" + reel1_bot +" "+ reel2_bot + " " + reel3_bot);
                //     // won = true;
                // }
            }
            // * Winning if there is a match of numbers in any row of first three reels
            //*
            // * case 1: reel1, row1
            else
            {
                // za vsak reel nared column vidnih znakou
                string[] col1 = new string[] { reel1_top, reel1_middle, reel1_bot };
                string[] col2 = new string[] { reel2_top, reel2_middle, reel2_bot };
                string[] col3 = new string[] { reel3_top, reel3_middle, reel3_bot };
                string[] col4 = new string[] { reel4_top, reel4_middle, reel4_bot };
                string[] col5 = new string[] { reel5_top, reel5_middle, reel5_bot };

                int[] idx1 = new int[] { idx1_top, idx1_middle, idx1_bot }; // row 1
                int[] idx2 = new int[] { idx2_top, idx2_middle, idx2_bot }; // row 2
                int[] idx3 = new int[] { idx3_top, idx3_middle, idx3_bot }; // row 3
                int[] idx4 = new int[] { idx4_top, idx4_middle, idx4_bot }; // row 4
                int[] idx5 = new int[] { idx5_top, idx5_middle, idx5_bot }; // row 5

                List<string> winningRows = new List<string>();
                List<string> winningCols = new List<string>();
                List<string> winningStrs = new List<string>();

                // string winString = "";

                //* za vsak element v col1
                for (int i = 0; i < col1.Length; i++)
                {
                    //* primerjej vsak element v col2
                    for (int j = 0; j < col2.Length; j++)
                    {
                        if (col1[i] == col2[j]) // če sta elementa  enaka, poglej še tretjo vrstico
                        {
                            //* primerjej vsak element v col3
                            for (int k = 0; k < col3.Length; k++)
                            {
                                if (col1[i] == col3[k]) // če so torej vsi trije elementi enaki, dodej indexe
                                {
                                    // shran win string v končne Liste
                                    winningCols.Add("123");
                                    winningRows.Add(idx1[i].ToString() + "," + idx2[j].ToString() + "," + idx3[k].ToString());
                                    winningStrs.Add(col1[i] + col2[j] + col3[k]);
                                    
                                    bool repeat4 = false;
                                    for (int l = 0; l < col4.Length; l++)
                                    {

                                        if (col1[i] == col4[l])
                                        {
                                            // če je v col4 spet isti znak...
                                            if (repeat4)
                                            {
                                                // ...še enkrat shran col1, col2 & col3 iz prejšnje vrstice
                                                winningCols.Add("123");
                                                winningRows.Add(idx1[i].ToString() + "," + idx2[j].ToString() + "," + idx3[k].ToString());
                                                winningStrs.Add(col1[i] + col2[j] + col3[k]);

                                            }
                                            // add elements to current lists
                                            winningCols[winningCols.Count-1] += "4";
                                            winningRows[winningRows.Count-1] += "," + idx4[l].ToString();
                                            winningStrs[winningStrs.Count-1] += col4[l];

                                            repeat4 = true;


                                            bool repeat5 = false;
                                            for (int m = 0; m < col5.Length; m++)
                                            {

                                                if (col1[i] == col5[m])
                                                {
                                                    // če je v col5 spet isti znak...
                                                    if (repeat5)
                                                    {
                                                        // ...še enkrat shran col1, col2, col3 & col4 iz prejšnje vrstice
                                                        winningCols.Add("123");
                                                        winningRows.Add(idx1[i].ToString() + "," + idx2[j].ToString() + "," + idx3[k].ToString());
                                                        winningStrs.Add(col1[i] + col2[j] + col3[k]);
                                                        winningCols[winningCols.Count - 1] += "4";
                                                        winningRows[winningRows.Count - 1] += "," + idx4[l].ToString();
                                                        winningStrs[winningStrs.Count - 1] += col4[l];

                                                    }
                                                    // add elements to current lists
                                                    winningCols[winningCols.Count - 1] += "5";
                                                    winningRows[winningRows.Count - 1] += "," + idx5[m].ToString();
                                                    winningStrs[winningStrs.Count - 1] += col5[m];

                                                    repeat5 = true;



                                                }
                                            }

                                        }
                                    }
                                    
                                    //* če v col4 ni blu istga znaka, naj se col5 prever posebej
                                    if (!repeat4)
                                    {
                                        bool repeat5 = false;
                                        for (int m = 0; m < col5.Length; m++)
                                        {

                                            if (col1[i] == col5[m])
                                            {
                                                // če je v col5 spet isti znak...
                                                if (repeat5)
                                                {
                                                    // ...še enkrat shran col1, col2, col3 & col4 iz prejšnje vrstice
                                                    winningCols.Add("123");
                                                    winningRows.Add(idx1[i].ToString() + "," + idx2[j].ToString() + "," + idx3[k].ToString());
                                                    winningStrs.Add(col1[i] + col2[j] + col3[k]);

                                                }
                                                // add elements to current lists
                                                winningCols[winningCols.Count - 1] += "5";
                                                winningRows[winningRows.Count - 1] += "," + idx5[m].ToString();
                                                winningStrs[winningStrs.Count - 1] += col5[m];

                                                repeat5 = true;



                                            }
                                        }
                                        
                                    }

                                }
                            }
                        }
                    }
                }

                // * WINNING ANIM
                StartCoroutine(WinningAnimEasy(winningStrs, winningRows, winningCols));

            }


            rolling = false;
        }
        rollingPrev = rolling;
    }

    IEnumerator WinningAnimEasy(List<string> winningStrs, List<string> winningRows, List<string> winningCols)
    {

        if (winningStrs.Count != 0)
        {

            winAnim = true;

            // Debug.Log(winningStrs.Count);
            //* za vsak element winning columna (ki vsebuje vsaj 3 string indexe)
            for (int i = 0; i < winningStrs.Count; i++)
            {

                //* animacije, ki se stopnjujejo z vsakim winning stringom
                winningUiB.SetActive(true);
                if (i == 0)
                    winningUiTmpB.text = "WIN";
                else
                    winningUiTmpB.text = (i+1) + "X WIN";

                //* play winBsmall sound for each string    (za vsako dodatno kombinacijo naj se ptich zveča)
                FindObjectOfType<AudioManager>().PlayDelayed("WinBsmall", 0.3f, 1f + (0.125f*i));

                // Change winningRows into a suitable format (vrednosti so shranjene v string, zatu nemorš več prepoznt cifre, ker je lahku dvomestna)
                string[] winningRowSplit = winningRows[i].Split(',');



                // Debug.Log("EasyMode Win " + i + ":\n" + winningRows[i] + "\n" + winningStrs[i] + "\n" + winningCols[i]);
                // matCol1[0].EnableKeyword("_EMISSION");
                
                // * WAIT FOR SECONDS
                yield return new WaitForSeconds(0.25f);

                //* za vsak char v i-tem elementu columna vklop emission
                for (int j = 0; j < winningCols[i].Length; j++)
                {
                    // mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRows[i][j].ToString())].EnableKeyword("_EMISSION");
                    mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRowSplit[j].ToString())].EnableKeyword("_EMISSION");

                    yield return new WaitForSeconds(0.05f);
                }

                yield return new WaitForSeconds(0.5f);


                //* za vsak char v i-tem elementu columna izklop emission
                for (int j = 0; j < winningCols[i].Length; j++)
                {
                    mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRowSplit[j].ToString())].DisableKeyword("_EMISSION");
                }

                if (i == winningStrs.Count - 1)
                {
                    yield return new WaitForSeconds(0.4f);
                    for (int j = 0; j < winningCols[i].Length; j++)
                        mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRowSplit[j].ToString())].EnableKeyword("_EMISSION");
                    yield return new WaitForSeconds(0.4f);
                    for (int j = 0; j < winningCols[i].Length; j++)
                        mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRowSplit[j].ToString())].DisableKeyword("_EMISSION");
                    yield return new WaitForSeconds(0.4f);
                    for (int j = 0; j < winningCols[i].Length; j++)
                        mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRowSplit[j].ToString())].EnableKeyword("_EMISSION");
                    yield return new WaitForSeconds(0.4f);
                    for (int j = 0; j < winningCols[i].Length; j++)
                        mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRowSplit[j].ToString())].DisableKeyword("_EMISSION");
                }

                // sklop winning ui
                if (i == winningStrs.Count - 1)
                {
                    // yield return new WaitForSeconds(1.9f);
                    yield return new WaitForSeconds(0.3f);
                    winningUiB.SetActive(false);
                }
                else
                    winningUiB.SetActive(false);

            }
            //* ko je konc win animacije, uklop play button emission
            detectMouseClick.EnableEmission();
            winAnim = false;
        }
        else
            detectMouseClick.EnableEmission();

    }

    IEnumerator WinningAnimBasic(int winCase, int idx1, int idx2, int idx3, int idx4, int idx5)
    {
        winAnim = true;
        winningUiA.SetActive(true);
        float delay = 0.05f;
        float onOfPeriod = 0.3125f;
        FindObjectOfType<AudioManager>().PlayDelayed("WinA", 0f, 1f);

        switch (winCase)
        {
            //* matched 123
            case 3:
                for (int i = 0; i < 5; i++)
                {
                    winningUiTmpA.text = "3 MATCH WIN!";
                    if (i == 0)
                        yield return new WaitForSeconds(delay);
                    mats[0, idx1].EnableKeyword("_EMISSION");
                    mats[1, idx2].EnableKeyword("_EMISSION");
                    mats[2, idx3].EnableKeyword("_EMISSION");
                    yield return new WaitForSeconds(onOfPeriod);
                    mats[0, idx1].DisableKeyword("_EMISSION");
                    mats[1, idx2].DisableKeyword("_EMISSION");
                    mats[2, idx3].DisableKeyword("_EMISSION");
                    yield return new WaitForSeconds(onOfPeriod);
                }
                break;
            //* matched 1234
            case 4:
                for (int i = 0; i < 5; i++)
                {
                    winningUiTmpA.text = "4 MATCH WIN!";
                    if (i == 0)
                        yield return new WaitForSeconds(delay);
                    mats[0, idx1].EnableKeyword("_EMISSION");
                    mats[1, idx2].EnableKeyword("_EMISSION");
                    mats[2, idx3].EnableKeyword("_EMISSION");
                    mats[3, idx4].EnableKeyword("_EMISSION");
                    yield return new WaitForSeconds(onOfPeriod);
                    mats[0, idx1].DisableKeyword("_EMISSION");
                    mats[1, idx2].DisableKeyword("_EMISSION");
                    mats[2, idx3].DisableKeyword("_EMISSION");
                    mats[3, idx4].DisableKeyword("_EMISSION");
                    yield return new WaitForSeconds(onOfPeriod);
                }
                break;
            //* matched 1235
            case 5:
                for (int i = 0; i < 5; i++)
                {
                    winningUiTmpA.text = "4 MATCH WIN!";
                    if (i == 0)
                        yield return new WaitForSeconds(delay);
                    mats[0, idx1].EnableKeyword("_EMISSION");
                    mats[1, idx2].EnableKeyword("_EMISSION");
                    mats[2, idx3].EnableKeyword("_EMISSION");
                    mats[4, idx5].EnableKeyword("_EMISSION");
                    yield return new WaitForSeconds(onOfPeriod);
                    mats[0, idx1].DisableKeyword("_EMISSION");
                    mats[1, idx2].DisableKeyword("_EMISSION");
                    mats[2, idx3].DisableKeyword("_EMISSION");
                    mats[4, idx5].DisableKeyword("_EMISSION");
                    yield return new WaitForSeconds(onOfPeriod);
                }
                break;
            //* matched 12345
            case 6:
                for (int i = 0; i < 5; i++)
                {
                    winningUiTmpA.text = "5 MATCH WIN!";
                    if (i == 0)
                        yield return new WaitForSeconds(delay);
                    mats[0, idx1].EnableKeyword("_EMISSION");
                    mats[1, idx2].EnableKeyword("_EMISSION");
                    mats[2, idx3].EnableKeyword("_EMISSION");
                    mats[3, idx4].EnableKeyword("_EMISSION");
                    mats[4, idx5].EnableKeyword("_EMISSION");
                    yield return new WaitForSeconds(onOfPeriod);
                    mats[0, idx1].DisableKeyword("_EMISSION");
                    mats[1, idx2].DisableKeyword("_EMISSION");
                    mats[2, idx3].DisableKeyword("_EMISSION");
                    mats[3, idx4].DisableKeyword("_EMISSION");
                    mats[4, idx5].DisableKeyword("_EMISSION");
                    yield return new WaitForSeconds(onOfPeriod);
                }
                break;
            default:
                Debug.Log("Something must have gone wrong");
                break;
        }
        //* ko je konc win animacije, uklop play button emission
        detectMouseClick.EnableEmission();
        winningUiA.SetActive(false);
        winAnim = false;
    }

    void HandleInput()
    {
        // *če so se vsi nehal vrtet lahku zaženš
        if ((Input.GetKeyDown(KeyCode.Space) || detectMouseClick.play) && !winAnim && spin1.spinSpeed == 0f && spin2.spinSpeed == 0f && spin3.spinSpeed == 0f && spin4.spinSpeed == 0f && spin5.spinSpeed == 0f)
        {
            //* začni rolling zvok
            FindObjectOfType<AudioManager>().Play("Rolling", 0f);

            spin1.calculateSpin = true;
            spin1.timeCounter = 0f;
            spin2.calculateSpin = true;
            spin2.timeCounter = 0f;
            spin3.calculateSpin = true;
            spin3.timeCounter = 0f;
            spin4.calculateSpin = true;
            spin4.timeCounter = 0f;
            spin5.calculateSpin = true;
            spin5.timeCounter = 0f;

            // disablej emission za vse materiale
            foreach (var mat in mats)
            {
                mat.DisableKeyword("_EMISSION");
            }

            // enable play button emission
            detectMouseClick.DisableEmission();


            rolling = true;
        }
    }
}









