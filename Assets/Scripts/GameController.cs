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

    private int[] auftrag;
    private int auftragGroesse;
    private int iceSorten;

    //Customer
    public GameObject customer;
    private GameObject customerClone;
    private CustomerMovement customerMovementScript;

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnPassasnts());

        waffleIsComplete = false;
        //waffleBehaviorScript = waffleClone.GetComponent(WaffleBehavior);
        auftrag = new int[0];
        auftragGroesse = 1;
        iceSorten = 1;

        createAuftrag();
        createWaffle();
        createCustomer();

    }

    IEnumerator SpawnPassasnts()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            if (maxPassants > counterPassants)//workaround to enable respawn
            {
                Vector3 spawnPosition = new Vector3(spawnValues.x + Random.Range(-spawnRange.x, spawnRange.x), spawnValues.y, spawnValues.z + Random.Range(-spawnRange.z, spawnRange.z));
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(passant, spawnPosition, spawnRotation);
                counterPassants++;
                Debug.Log("Passantenanzahl: "+counterPassants);
            }
            yield return new WaitForSeconds(startWait);
        }
    }

    public void ReduceCounterPassants() //Der Passant kann dem GameController sagen, dass er "verschwunden" ist.
    {
        --counterPassants;
        Debug.Log("Passantenanzahl: " + counterPassants);
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

    void createAuftrag()
    {
        auftrag = new int[0];//Array leeren
        auftrag = new int[auftragGroesse];//Array auf die aktuelle Autragsgroesse anpassen
        for (int i=0;i<auftragGroesse;i++)
        {
            auftrag[i] = (int)Random.Range(0, iceSorten);
        }
    }

    void createWaffle()
    {
        Vector3 position = new Vector3(0, 0.77f, 0.43f);
        waffleClone = (GameObject)Instantiate(waffle, position, transform.rotation);
        waffleBehaviorScript = (WaffleBehavior)waffleClone.GetComponent(typeof(WaffleBehavior));
    }

    void createCustomer()
    {
        Vector3 position = new Vector3(0.09f, 1.0f, 10.0f);
        customerClone = (GameObject)Instantiate(customer, position, transform.rotation);
        customerMovementScript = (CustomerMovement)customerClone.GetComponent(typeof(CustomerMovement));
    }

    public void IceIsInDelivery()
    {
        Debug.Log("IceIsInDelivery()");
        if(true)
        {
            score += getScoreValue();
            //waffleClone.DestroyWaffle();
            waffleBehaviorScript.DestroyWaffle();
            customerMovementScript.setServed();
            createWaffle();
            createCustomer();
        }
    }

    private int getScoreValue()
    {
        return 0;
    }
}
