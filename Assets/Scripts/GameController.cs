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
    private int []laneAssignments;

    //GUI TV
    //public GUIText scoreText;
    private int score;
    public TextMesh scoreTextNumber;
    private float gameTime;
    public TextMesh gameTimeTextNumber;
    public TextMesh difficultyTextNumber;
    //scoreboard
    public ScoreManager scoreboard;

    //waffle
    public GameObject waffle;
    private GameObject waffleClone;
    private bool waffleIsComplete;
    private WaffleBehavior waffleBehaviorScript;

    //difficulty
    private float[] difficultyLevel; //gibt einen Faktor zurück, mit dem die aktuelle Schwierigkeitsstufe beschrieben wird.
    private int difficulty; //diesen Wert in difficultyLevel[] verwenden und man sieht in welcher Schwierigkeitsstufe gerade gespielt wird D:.
    private int[] order; //die aktuelle Order. Also welche Eiskugeln die Waffel enthalten soll.
    private int orderSize; //größe der Order. Also aus wievielen Eiskugeln die Order bestehen soll
    private int iceSorten; //So viele verschiedene Eissorten habe ich für die Order zur Verfügung.
    private float orderValue; //Das ist die Order Wert.
    private int maxOrderSize;//not used yet Maximale Anzahl an Eiskugeln, aus der eine Order bestehen darf.
    private int maxIceSorten;//not used yet Maximale Anzahl an Eissorten aus denen gewählt werden darf.
    private int[] difficultyMaxTime;
    private int[] difficultyMinTime;
    private float[] difficultySpeechBubbledisappear;
    private int[] difficultyIceSorten;
    private int[] diffcultyOrderSize;
    private int[] succesfullOrdersTillNextLevel;
    private int succesfullOrdersInThisLevel;

    //Customer
    public GameObject customer;
    private GameObject customerClone;
    private CustomerMovement customerMovementScript;

    //tutorial
    public GameObject tutorial;


    void Start()
    {
        //passants
        spawnPositionsPassants = new Vector3[] { new Vector3(10f,1f,3.3f),new Vector3(10f,1f,5.3f), new Vector3(10f,1f,6.2f), new Vector3(10f, 1f, 7.4f) , new Vector3(10f, 1f, 8.6f) };//diese drei Arrays müssen die !gleiche! Länge haben
        spawnRotationPassants =new float[]{ 0f,0f,0f, 0f, 0f };
        laneAssignments = new int[] { 0, 0, 0, 0, 0 };

        //difficulty
        difficulty = 0;
        difficultyLevel = new float[] { 1, 1, 1, 1, 1, 1, 1 };
        difficultyMaxTime = new int[] { 40, 30, 20, 10, 8, 6, 4 };
        difficultyMinTime = new int[] { 10, 5, 2, 1, 1, 1, 1 };
        difficultySpeechBubbledisappear = new float [] { 10f, 8f, 6f, 5f, 4f, 3f, 2f }; ;
        difficultyIceSorten = new int[] { 1, 2, 2, 3, 4, 5, 5 };
        diffcultyOrderSize = new int[] { 2, 2, 3, 3, 3, 4, 5 };
        succesfullOrdersTillNextLevel = new int[] { 1, 2, 3, 3, 3, 3, 100 };
        succesfullOrdersInThisLevel = 0;


        score = 0;
        gameTime = 60;
        UpdateGUITV();

        StartCoroutine(GameTimeContinuous());

        //Customer
        //StartCoroutine(SpawnPassants());
        StartCoroutine(PassantsTest());

        //waffle
        waffleIsComplete = false;
        //waffleBehaviorScript = waffleClone.GetComponent(WaffleBehavior);
        order = new int[0];
        //orderSize = 4;
        //iceSorten = 1;
        maxOrderSize = 5;
        maxIceSorten = 3;
        SetRightDifficulty();

        createOrder();
        createWaffle();
        createCustomer();


        //scoreboard
        //scoreboard.LoadScore();
    }

    void Update()
    {
        UpdateGUITV();//TODO braucht es das hier?
    }

    int GiveFreeLane()
    {
        int freeLane = (int)(Random.value * spawnPositionsPassants.GetLength(0)); //Startposition der Suche
        int searchedLanes = 0;
        int lanes = spawnPositionsPassants.GetLength(0);
        int run = 0;
        
        while (true)//risky risky
        {
            //Debug.Log("freeLane: " + freeLane + " searchedLanes: " + searchedLanes + " lanes: " + lanes + " run: " + run);
            //Debug.Log("GiveFreeLane while");
            for(;searchedLanes<lanes;searchedLanes++)
            {
                if(freeLane==lanes) //wir zählen nicht beginnend bei 0, daher überprüfen, ob wir noch im gültigen Bereich sind
                {
                    //Debug.Log("GiveFreeLane if 1");
                    freeLane = 0;
                }

                if (laneAssignments[freeLane]==run)
                {
                    //Debug.Log("GiveFreeLane if 2");
                    //Debug.Log("return " + freeLane);
                    return freeLane;
                }
                else
                {
                    freeLane++;
                }
            }
            searchedLanes = 0;
            run++;//wenn es nicht anders geht, dann setzen wir halt mehr als einen Passanten auf eine Lane
        }
        //Debug.Log("return -1");
        return 0;//this should not happen
    }

    IEnumerator PassantsTest()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (maxPassants > counterPassants)
            {
                int freeLane = GiveFreeLane();//Auf welcher Lane ist gerade wenig los?

                if (Random.value > 0.5)//Entscheidung, ob Passant links oder rechts startet
                {
                    spawnPositionsPassants[freeLane].x *= -1;//ob die if Abfrage true oder false ist, definiert nicht, ob der Passant links oder rechts spawnt, sondern nur ob er an der gleichen Stelle wie der vorherige oder auf der anderen Seite spawnt
                    spawnRotationPassants[freeLane] = 180 - spawnRotationPassants[freeLane];
                }

                var tempPassant = (GameObject)Instantiate(passant, spawnPositionsPassants[freeLane], Quaternion.identity);
                MovePassant MovePassantScript = (MovePassant)tempPassant.GetComponent(typeof(MovePassant));
                MovePassantScript.AddRotation(spawnRotationPassants[freeLane]);

                MovePassantScript.AddLaneDescription(freeLane);
                counterPassants++;
                laneAssignments[freeLane]++;//jetzt ist einer mehr unterwegs :)
            }
            yield return new WaitForSeconds(startWait);//TODO ersetzen durch spawnWait

        }

        /*for (int i = 0; i < spawnPositionsPassants.GetLength(0); i++)
        {
            if (Random.value > 0.5)
            {
                spawnPositionsPassants[i].x *= -1;
                spawnRotationPassants[i] = 180 - spawnRotationPassants[i];
            }
            Quaternion spawnRotation = Quaternion.identity;

            var tempPassant = (GameObject)Instantiate(passant, spawnPositionsPassants[i], Quaternion.identity);
            MovePassant MovePassantScript = (MovePassant)tempPassant.GetComponent(typeof(MovePassant));
            MovePassantScript.AddRotation(spawnRotationPassants[i]);
            //MovePassantScript.path = i;
            //yield return new WaitForSeconds(startWait);
            yield return new WaitForSeconds(0.5f);
        }
        //yield return null;*/
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
            yield return new WaitForSeconds(spawnWait);//TODO ersetzen durch spawnWait
        }
    }

    public void ReduceCounterPassants(int lane) //Der Passant kann dem GameController sagen, dass er "verschwunden" ist.
    {
        counterPassants--;
        laneAssignments[lane]--;
        Debug.Log("Passantenanzahl: " + counterPassants);
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateGUITV();
    }

    void UpdateGUITV()//Sammlung aller Zahlen, die im GUITV aktualisiert werden müssen
    {
        difficultyTextNumber.text = difficulty.ToString("0");
        gameTimeTextNumber.text = gameTime.ToString("00");
        //scoreTextNumber.text = orderValue.ToString("0000");//nur zum testen
        scoreTextNumber.text = score.ToString("0000");
        //scoreText.text = "Score: " + score;
    }

    void createOrder()
    {
        order = new int[0];//Array leeren
        order = new int[5] { -1, -1, -1, -1, -1 };//Array auf die aktuelle Autragsgroesse anpassen
        for (int i=0;i<orderSize;i++)
        {
            order[i] = (int)Random.Range(0, iceSorten);
        }
        //Debug.Log("order" + order[0] + " " + order[1] + " " + order[2] + " " + order[3] + " " + order[4]);
        System.Array.Sort(order);
        //Debug.Log("order" + order[0] + " " + order[1] + " " + order[2] + " " + order[3] + " " + order[4]);
        orderValue=ValueOfOrder();
    }

    public float SpeechbubbleDestroyTime()
    {
        return difficultySpeechBubbledisappear[difficulty];
    }

    IEnumerator DecreaseValueOfOrder()
    {
        yield return new WaitForSeconds(difficultyMinTime[difficulty]);
        int decreaseWindow = difficultyMaxTime[difficulty] - difficultyMinTime[difficulty];
        float decreaseValue = orderValue / decreaseWindow;
        while (orderValue >= 10)
        {
            orderValue -= decreaseValue*Time.deltaTime;
            //orderValue -= difficultyLevel[difficulty]*Time.deltaTime;//Faktor ist noch frei gewählt, vielleicht Time.time mitverwenden
            yield return null;//TODO
        }
    }

    public void StartPointDecay()
    {
        StartCoroutine("DecreaseValueOfOrder");
    }

    public void StopPointDecay()
    {
        StopCoroutine("DecreaseValueOfOrder");
    }

    public void SetRightDifficulty()
    {
        if(succesfullOrdersInThisLevel>=succesfullOrdersTillNextLevel[difficulty])
        {
            IncreseDifficulty();
        }
        orderSize = diffcultyOrderSize[difficulty];
        iceSorten = difficultyIceSorten[difficulty];
    }

    public void IncreseDifficulty()
    {
        difficulty++;
        succesfullOrdersInThisLevel = 0;
    }

    public void DecreaseDifficulty()
    {
        difficulty--;
        succesfullOrdersInThisLevel = 0;
    }

    int ValueOfOrder() //Initialisiert den Wert der Order.
    {
        //TODO
        return (int)difficultyLevel[difficulty] * ((orderSize*10) + (iceSorten*5));
        //return 0;
    }

    IEnumerator GameTimeContinuous()//TODO testen GameTimeContinuous,GameTimeIncrease und Aufruf in IceIsInDelivery
    {//verringert in jedem Frame die verbleibende Spielzeit
        while(gameTime>0)
        {
            gameTime -= Time.deltaTime;
            yield return null;
        }
        GameOver();
        yield return null;
    }

    private void GameTimeIncrease(int plusTime)//erhöht die Spielzeit
    {
        gameTime += plusTime;
    }

    private void GameOver()//leider ist das Spiel vorbei. Es wird eine Rangliste angezeigt
    {
        customerMovementScript.setServed();
        scoreboard.GameOver(score);
        StopAllCoroutines();
    }

    public void createWaffle()
    {
        Vector3 position = new Vector3(0.313f, 1.3859f, 0.4840664f);
        waffleClone = (GameObject)Instantiate(waffle, position, transform.rotation);
        waffleBehaviorScript = (WaffleBehavior)waffleClone.GetComponent(typeof(WaffleBehavior));
    }

    void createCustomer()
    {
        Vector3 position = new Vector3(0.09f, 1.0f, 8.0f);
        customerClone = (GameObject)Instantiate(customer, position, transform.rotation);
        customerMovementScript = (CustomerMovement)customerClone.GetComponent(typeof(CustomerMovement));
    }

    bool isIceReady()//TODO testen. Wird zusammen mit der Eiskugel an Waffel kleben Mechanik getestet
    {
        return true;
        int[] iceBalls = waffleBehaviorScript.PullIceballs();
        if(iceBalls.GetLength(0)!=order.GetLength(0)) //ist in der Waffel exakt die Anzahl der gewünschten Kugeln enthalten?
        {
            return false;
        }
        System.Array.Sort(iceBalls); //sortieren der Werte in iceBalls
        bool areEqual = true;
        for (int i=0;i<order.GetLength(0);i++)
        {
            if(order[i]!=iceBalls[i])
            {
                areEqual = false;
            }
        }
        //Debug.Log("iceBalls" + iceBalls[0] + " " + iceBalls[1] + " " + iceBalls[2] + " " + iceBalls[3]);
        //Debug.Log("order" + order[0] + " " + order[1] + " " + order[2] + " " + order[3]);
        return areEqual;
    }

    public bool IceIsInDelivery()
    {
        Debug.Log("IceIsInDelivery()");
        if(isIceReady())
        {
            succesfullOrdersInThisLevel++;
            SetRightDifficulty();
            tutorial.SetActive(false);
            score += getScoreValue();
            GameTimeIncrease(20);
            //waffleClone.DestroyWaffle();
            waffleBehaviorScript.DestroyWaffle();
            customerMovementScript.setServed();
            createWaffle();
            createCustomer();
            createOrder();//TODO diese Zeile noch testen
            return true;
        }
        customerMovementScript.makeOhSound();
        return false;
    }

    public void TrashcanReset()
    {
        waffleBehaviorScript.DestroyWaffle();
        createWaffle();
    }

    private int getScoreValue()//gibt den Wert der aktuellen Order zurück als int.
    {
        //return 123;//TODO diese Zeile entfernen
        return (int)orderValue;
    }

    public int[] getOrder()
    {
        return order;
    }
}
