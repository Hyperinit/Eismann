using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class ScoreManager : MonoBehaviour {

    private Dictionary<string,int> scoreDic; //our current scorerboard saved in as <timestampe,score>
    public GameObject Scoreboard; //scoreboard GameObject
    public Text player1; //timestamp alias the player name displayed on the left side at the scoreboard
    public Text score1; //score reached by player1. Dispayed on the right side at the scoreboard
    public Text player2; //timestamp alias the player name displayed on the left side at the scoreboard
    public Text score2; //score reached by player2. Dispayed on the right side at the scoreboard
    public Text player3; //timestamp alias the player name displayed on the left side at the scoreboard
    public Text score3; //score reached by player3. Dispayed on the right side at the scoreboard
    public Text player4;//timestamp alias the player name displayed on the left side at the scoreboard
    public Text score4; //score reached by player4. Dispayed on the right side at the scoreboard

    private string[] playerNameArray; //array with all stored highscore names + current player
    private int[] scoreArray; //array with all stored highscore scores + current score


    // Use this for initialization
    void Start () {
        LoadScore();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //call this function when the game is over. It will build a scoreboard by adding the current score and then transform the dictionary to arrays for easier access to the max values. It will then proceed to put the scores in the right order and delete the lowest score in the dictionary. Displayes the scoreboard in the world. Saves the top 4 scores.
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

    //load saved scores
    void LoadScore()
    {
        scoreDic = GetDict("dict.txt");
    }

    //save scores
    void SaveScore()
    {
        ConvertDictionary(scoreDic);
    }
    
    // adds a new score in the dictionary. Format is <timestamp,score>
    public void NewScore(int scorePoints)
    {
        scoreDic.Add(DateTime.Now.ToString("yyyy/MM/dd-HH.mm"), scorePoints);
    }

    // transforms the dictionary<string,int> to two arrays. Will be used to find the max value
    private void TransDicInArrays()
    {
        playerNameArray = scoreDic.Keys.ToArray();
        scoreArray = scoreDic.Values.ToArray();
    }


    //source https://www.dotnetperls.com/convert-dictionary-string further explanation is there
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