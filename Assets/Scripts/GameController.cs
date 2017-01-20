using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject passant;
    public Vector3 spawnValues;
    public Vector3 spawnRange;

    //passants
    public float spawnWait;
    public float startWait;
    public int maxPassants;
    private int counterPassants;
    private Vector3[] spawnPositionsPassants;
    private float[] spawnRotationPassants;

    public GUIText scoreText;
    private int score;

    //waffle
    public GameObject waffle;
    private GameObject waffleClone;
    private bool waffleIsComplete;
    private WaffleBehavior waffleBehaviorScript;

    private int[] order;
    private int orderSize;
    private int iceSorten;
    private int maxOrderSize;//not used yet
    private int maxIceSorten;//not used yet

    //Customer
    public GameObject customer;
    private GameObject customerClone;
    private CustomerMovement customerMovementScript;

    void Start()
    {
        spawnPositionsPassants = new Vector3[] { new Vector3(10f,1f,3.3f),new Vector3(10f,1f,5.3f), new Vector3(10f,1f,6.2f), new Vector3(10f, 1f, 7.4f) , new Vector3(10f, 1f, 8.6f) };
        spawnRotationPassants =new float[]{ 0f,-2.862f,0f, 0f, 0f };
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnPassants());
        //PassantsTest();

        waffleIsComplete = false;
        //waffleBehaviorScript = waffleClone.GetComponent(WaffleBehavior);
        order = new int[0];
        orderSize = 5;
        iceSorten = 3;
        

        createOrder();
        createWaffle();
        createCustomer();

    }

    void PassantsTest()
    {
        for (int i = 0; i < spawnPositionsPassants.GetLength(0); i++)
        {
            if(Random.value>0.5)
            {
                spawnPositionsPassants[i].x *= -1;
                spawnRotationPassants[i] = 180-spawnRotationPassants[i];
            }
            Quaternion spawnRotation = Quaternion.identity;

            var tempPassant = (GameObject)Instantiate(passant, spawnPositionsPassants[i], Quaternion.identity);
            MovePassant MovePassantScript = (MovePassant)tempPassant.GetComponent(typeof(MovePassant));
            MovePassantScript.AddRotation(spawnRotationPassants[i]);
           //MovePassantScript.path = i;
        }
    }

    IEnumerator SpawnPassants()
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

    void createOrder()
    {
        order = new int[0];//Array leeren
        order = new int[orderSize];//Array auf die aktuelle Autragsgroesse anpassen
        for (int i=0;i<orderSize;i++)
        {
            order[i] = (int)Random.Range(0, iceSorten);
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
            createOrder();//TODO diese Zeile noch testen
        }
    }

    private int getScoreValue()
    {
        return 0;
    }

    public int[] getOrder()
    {
        return order;
    }
}
