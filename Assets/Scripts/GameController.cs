using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject passant;
    public Vector3 spawnValues;
    public Vector3 spawnRange;

    public float spawnWait;
    public float startWait;
    public int maxPassants;
    private int counterPassants;

    public GUIText scoreText;
    private int score;

    //waffle
    public GameObject waffle;
    private GameObject waffleClone;
    private bool waffleIsComplete;
    private WaffleBehavior waffleBehaviorScript;

    private ArrayList auftrag;

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnPassasnts());

        waffleIsComplete = false;
        //waffleBehaviorScript = waffleClone.GetComponent(WaffleBehavior);
        auftrag = new ArrayList();
        createWaffle();
    }

    IEnumerator SpawnPassasnts()
    {
        yield return new WaitForSeconds(startWait);
        while (maxPassants>counterPassants)
        {
            Vector3 spawnPosition = new Vector3(spawnValues.x+Random.Range(-spawnRange.x, spawnRange.x), spawnValues.y, spawnValues.z + Random.Range(-spawnRange.z, spawnRange.z));
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(passant, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(startWait);
            counterPassants++;
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
    void ReduceCounterPassants()
    {
        --counterPassants;
    }

    void createWaffle()
    {
        Vector3 position = new Vector3(0, 0.77f, 0.34f);
        waffleClone = (GameObject)Instantiate(waffle, position, transform.rotation);
        waffleBehaviorScript = (WaffleBehavior)waffleClone.GetComponent(typeof(WaffleBehavior));
    }

    public void IceIsInDelivery()
    {
        Debug.Log("IceIsInDelivery()");
        if(true)
        {
            score += getScoreValue();
            //waffleClone.DestroyWaffle();
            waffleBehaviorScript.DestroyWaffle();
            createWaffle();
        }
    }

    private int getScoreValue()
    {
        return 0;
    }
}
