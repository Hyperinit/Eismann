using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class ScoreManager : MonoBehaviour {

    private Dictionary<string,int> scoreDic;
    public GameObject Scoreboard;
    public Text player1;
    public Text score1;
    public Text player2;
    public Text score2;
    public Text player3;
    public Text score3;
    public Text player4;
    public Text score4;

    //private List<Tuple<int,string>> tupleList;
    private string[] playerNameArray;
    private int[] scoreArray;


    // Use this for initialization
    void Start () {
        LoadScore();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void GameOver(int score)
    {
        NewScore(score);
        TransDicInArrays();
        int scorePos = scoreArray.ToList().IndexOf(scoreArray.Max());
        player1.text = playerNameArray[scorePos];
        score1.text = scoreArray[scorePos].ToString();
        scoreArray[scorePos] = -1;

        scorePos = scoreArray.ToList().IndexOf(scoreArray.Max());
        player2.text = playerNameArray[scorePos];
        score2.text = scoreArray[scorePos].ToString();
        scoreArray[scorePos] = -1;

        scorePos = scoreArray.ToList().IndexOf(scoreArray.Max());
        player3.text = playerNameArray[scorePos];
        score3.text = scoreArray[scorePos].ToString();
        scoreArray[scorePos] = -1;

        scorePos = scoreArray.ToList().IndexOf(scoreArray.Max());
        player4.text = playerNameArray[scorePos];
        score4.text = scoreArray[scorePos].ToString();
        scoreArray[scorePos] = -1;

        scorePos = scoreArray.ToList().IndexOf(scoreArray.Max());
        scoreDic.Remove(playerNameArray[scorePos]);

        Scoreboard.SetActive(true);

        SaveScore();
    }
    void LoadScore()
    {
        scoreDic = GetDict("dict.txt");
    }

    void SaveScore()
    {
        ConvertDictionary(scoreDic);
    }

    public void NewScore(int scorePoints)
    {
        scoreDic.Add(DateTime.Now.ToString("yyyy/MM/dd-HH.mm"), scorePoints);
    }

    public Dictionary<string, int> GiveDic()
    {
        return scoreDic;
    }

    private string NewDate()
    {
        //return System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        return System.DateTime.Now.ToString("HH:mm:ss");
    }

    private void TransDicInArrays()
    {
        Debug.Log("What is in the Dic?:");
        playerNameArray = scoreDic.Keys.ToArray();
        scoreArray = scoreDic.Values.ToArray();
        //for (int i = 0; i < playerNameArray.GetLength(0); i++) //Debug
        //    Debug.Log(playerNameArray[i]+":"+scoreArray[i]);
    }


    //ab hier https://www.dotnetperls.com/convert-dictionary-string
    Dictionary<string, int> _dictionary = new Dictionary<string, int>()
    {
        {"salmon", 5},
        {"tuna", 6},
        {"clam", 2},
        {"asparagus", 3}
    };

    public void ConvertDictionary(Dictionary<string, int> m_dictionary)
    {
        // Convert dictionary to string and save
        string s = GetLine(m_dictionary);
        File.WriteAllText("dict.txt", s);
        // Get dictionary from that file
        Dictionary<string, int> d = GetDict("dict.txt");
    }
    string GetLine(Dictionary<string, int> d)
    {
        // Build up each line one-by-one and then trim the end
        StringBuilder builder = new StringBuilder();
        foreach (KeyValuePair<string, int> pair in d)
        {
            builder.Append(pair.Key).Append(":").Append(pair.Value).Append(',');
        }
        string result = builder.ToString();
        // Remove the final delimiter
        result = result.TrimEnd(',');
        return result;
    }
    Dictionary<string, int> GetDict(string f)
    {
        Dictionary<string, int> d = new Dictionary<string, int>();
        string s = File.ReadAllText(f);
        // Divide all pairs (remove empty strings)
        string[] tokens = s.Split(new char[] { ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
        // Walk through each item
        for (int i = 0; i < tokens.Length; i += 2)
        {
            string name = tokens[i];
            string freq = tokens[i + 1];

            // Parse the int (this can throw)
            int count = int.Parse(freq);
            // Fill the value in the sorted dictionary
            if (d.ContainsKey(name))
            {
                d[name] += count;
            }
            else
            {
                d.Add(name, count);
            }
        }
        return d;
    }

}