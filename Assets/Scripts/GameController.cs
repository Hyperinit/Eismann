using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject passant;
    public Vector3 spawnValues;

    public float spawnWait;
    public float startWait;

    public GUIText scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnPassasnts());
    }

    IEnumerator SpawnPassasnts()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(passant, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(startWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
