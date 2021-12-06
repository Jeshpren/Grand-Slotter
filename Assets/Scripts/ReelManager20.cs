using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelManager20 : MonoBehaviour
{
    [Header("Main")]
    public bool eaysMode = true;

    [Header("Reel Transforms")]
    public Transform reel1;
    public Transform reel2;
    public Transform reel3;
    public Transform reel4;
    public Transform reel5;

    Spin20 spin1;
    Spin20 spin2;
    Spin20 spin3;
    Spin20 spin4;
    Spin20 spin5;

    Material[,] mats = new Material[5, 20];

    // float spinSpeed1;
    // float spinSpeed2;
    // float spinSpeed3;
    // float spinSpeed4;
    // float spinSpeed5;

    // bool won = false;
    bool rolling = false;
    bool rollingPrev = false;

    // Start is called before the first frame update
    void Start()
    {
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
            if (!eaysMode)
            {
                //* top row
                if (reel1_top == reel2_top && reel1_top == reel3_top)
                // if (spin1.top == spin2.top && spin1.top == spin3.top)
                {
                    Debug.Log("****** YOU HAVE WON; top row:\n" + reel1_top + " " + reel2_top + " " + reel3_top + "\no o o\no o o");
                    // won = true;
                }
                //* middle row
                if (reel1_middle == reel2_middle && reel1_middle == reel3_middle)
                // if (spin1.middle == spin2.middle && spin1.middle == spin3.middle)
                {
                    Debug.Log("****** YOU HAVE WON; middle row:\no o o" + reel1_middle + " " + reel2_middle + " " + reel3_middle + "\no o o");
                    // won = true;
                }
                //* bot row
                if (reel1_bot == reel2_bot && reel1_bot == reel3_bot)
                // if (spin1.bot == spin2.bot && spin1.bot == spin3.bot)
                {
                    Debug.Log("****** YOU HAVE WON; bot row:\no o o\no o o" + reel1_bot +" "+ reel2_bot + " " + reel3_bot);
                    // won = true;
                }
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
                                    

                                    //* primerjej vsak element v col4
                                    bool repeat4 = false;
                                    bool repeat4for5 = false;
                                    string col4for5 = "";
                                    for (int l = 0; l < col4.Length; l++)
                                    {
                                        if (col1[i] == col4[l])
                                        {
                                            // če si v 4tm reelu najdu dva/3 prava elementa, morš drugič/tretjič spet dodt vrstico
                                            if (repeat4)
                                            {
                                                winningCols.Add("123");
                                                winningRows.Add(idx1[i].ToString() + "," + idx2[j].ToString() + "," + idx3[k].ToString());
                                                winningStrs.Add(col1[i] + col2[j] + col3[k]);
                                                repeat4for5 = true;
                                            }

                                            // add elements to current lists
                                            winningCols[winningCols.Count-1] += "4";
                                            winningRows[winningRows.Count-1] += "," + idx4[l].ToString();
                                            col4for5 = idx4[l].ToString();  // * to vrstico rabš za col5, če se col5 ponovi
                                            winningStrs[winningStrs.Count-1] += col4[l];
                                            repeat4 = true;
                                        }
                                    }

                                    bool repeat5 = false;
                                    //* primerjej vsak element v col5
                                    for (int l = 0; l < col5.Length; l++)
                                    {
                                        if (col1[i] == col5[l])
                                        {
                                            if (repeat5)
                                            {   
                                                winningCols.Add("123");
                                                winningRows.Add(idx1[i].ToString() + "," + idx2[j].ToString() + "," + idx3[k].ToString());
                                                winningStrs.Add(col1[i] + col2[j] + col3[k]);
                                                // Debug.Log(winningCols[winningCols.Count - 2].Length);
                                                if (winningCols[winningCols.Count-2].Length == 5)
                                                {
                                                    winningCols[winningCols.Count - 1] += "4";
                                                    winningRows[winningRows.Count - 1] += "," + col4for5; 
                                                    winningStrs[winningStrs.Count - 1] += winningStrs[winningStrs.Count - 2][3];
                                                    
                                                }
                                            }
                                            
                                            // ! ne dela sm še v primeru, da se in col4 in col5 ponovita
                                            // če se je col4 ponovu, morš col5 zapisat tut v predzadnji rezultat
                                            if (repeat4for5)
                                            {
                                                // Debug.Log(winningCols.Count);
                                                winningCols[winningCols.Count - 2] += "5";
                                                winningRows[winningRows.Count - 2] += "," + idx5[l].ToString();
                                                winningStrs[winningStrs.Count - 2] += col5[l];
                                            }

                                            winningCols[winningCols.Count - 1] += "5";
                                            winningRows[winningRows.Count - 1] += "," + idx5[l].ToString();
                                            winningStrs[winningStrs.Count - 1] += col5[l];
                                            // Debug.Log("5");

                                            repeat5 = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // * WINNING ANIM
                StartCoroutine(WinningAnim(winningStrs, winningRows, winningCols));

            }


            rolling = false;
        }
        rollingPrev = rolling;
    }

    IEnumerator WinningAnim(List<string> winningStrs, List<string> winningRows, List<string> winningCols)
    {
        if (winningStrs.Count != 0)
        {

            // Debug.Log(winningStrs.Count);
            //* za vsak element winning columna (ki vsebuje vsaj 3 string indexe)
            for (int i = 0; i < winningStrs.Count; i++)
            {
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
                    // mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRows[i][j].ToString())].DisableKeyword("_EMISSION");
                    mats[int.Parse(winningCols[i][j].ToString()) - 1, int.Parse(winningRowSplit[j].ToString())].DisableKeyword("_EMISSION");

                }


            }
        }
    }

    void HandleInput()
    {
        // *če so se vsi nehal vrtet lahku zaženš
        if (Input.GetKeyDown(KeyCode.Space)  && spin1.spinSpeed == 0f && spin2.spinSpeed == 0f && spin3.spinSpeed == 0f && spin4.spinSpeed == 0f && spin5.spinSpeed == 0f)
        {
            foreach (var mat in mats)
            {
                mat.DisableKeyword("_EMISSION");
            }


            rolling = true;
        }
    }
}









