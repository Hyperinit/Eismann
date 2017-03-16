using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {

    //passants LEGACY CODE
    public GameObject passant; //passant prefab. We need this to generate new ones. LEGACY CODE
    public Vector3 spawnValues; //gives a start position for passant. LEGACY CODE
    public Vector3 spawnRange; //Spawn area for passants. LEGACY CODE

    //passants
    public float startWait; //after this time first passant will spawn
    public float spawnWait; //after this time the next passant will spawn
    public int maxPassants; //only this amount of passants are allowed to wander simultaneously
    private int counterPassants; //current number of active passants
    private Vector3[] spawnPositionsPassants; //possible positions a passant could spawn
    private float[] spawnRotationPassants; //give the passant a rotation, so that they won't walke all parallel
    private int []laneAssignments; // saves the number of passants on each lane

    //GUI TV
    private int score; //points scored by a player in the current game
    public TextMesh scoreTextNumber; //interface to see the score in GUITV
    private float gameTime; //time left to play
    public TextMesh gameTimeTextNumber; //interface to see the time left in GUITV
    public TextMesh difficultyTextNumber; //interface to see current level in GUITV
    //scoreboard
    public ScoreManager scoreboard; //interface to administrate scoreboard

    //waffle
    public GameObject waffle; //waffle prefab. We need this to generate new ones
    private GameObject waffleClone; //our spawned waffle. This one will always spawn in the waffleholder on the right side. We need this variable to track our waffle.
    private WaffleBehavior waffleBehaviorScript; //better save waffleClones script for easier access
    private int[] order; //the current order. So which ice balls the waffle should contain

    //difficulty = difficulty level = level --- these variables are used to implement some kind of function to increase difficulty
    private int difficulty; //current difficulty level
    private int orderSize; //Size of the order. So how many ice balls the order should exist
    private int iceSorten; //So many different ice cream flavors I have for ordering.
    private float orderValue; //This is the order value. The more it is worth, the more points you get
    //use like this : variableName[difficulty]
    private float[] difficultyLevel; //Returns a factor that describes the current difficulty level.
    private int[] difficultyMaxTime; //after this time you only get the minium points for your correct order :(
    private int[] difficultyMinTime; //bevore this time you get the maxium points for your correct order :)
    private float[] difficultySpeechBubbledisappear; //after this time the speechbubble will disappear 
    private int[] difficultyIceSorten; //number of different ice cream flavors that are used
    private int[] diffcultyOrderSize; //number of scoops that are ordered
    private int[] succesfullOrdersTillNextLevel; //amount of succesfull orders needed till next level
    private int succesfullOrdersInThisLevel; // succesfull orders submitted in this level


    //Customer
    public GameObject customer; //customer prefab. We need this to generate new ones
    private GameObject customerClone; //our spawned customer. Keep this for easy tracking 
    private CustomerMovement customerMovementScript; // better save customers script for easier access

    //tutorial
    public GameObject tutorial; //GameObjects we need to show tutorial, actually we use this to deactivate the tutorial

    //gameOver
    private bool gameOver; //if true the game is over and no new customer, waffles and orders will spawn


    void Start()
    {
        //passants
        //These three arrays must have the same length
        spawnPositionsPassants = new Vector3[] { new Vector3(10f,1f,3.3f),new Vector3(10f,1f,5.3f), new Vector3(10f,1f,6.2f), new Vector3(10f, 1f, 7.4f) , new Vector3(10f, 1f, 8.6f) };
        spawnRotationPassants =new float[]{ 0f,0f,0f, 0f, 0f };
        laneAssignments = new int[] { 0, 0, 0, 0, 0 };

        //difficulty
        //These seven arrays must have the same length
        difficulty = 0;
        difficultyLevel = new float[] { 1, 1, 1, 1, 1, 1, 1 };
        difficultyMaxTime = new int[] { 40, 30, 20, 10, 8, 6, 4 };
        difficultyMinTime = new int[] { 10, 5, 2, 1, 1, 1, 1 };
        difficultySpeechBubbledisappear = new float [] { 10f, 8f, 6f, 5f, 4f, 3f, 2f }; ;
        difficultyIceSorten = new int[] { 1, 2, 2, 3, 4, 5, 5 };
        diffcultyOrderSize = new int[] { 2, 2, 3, 3, 3, 4, 5 };
        succesfullOrdersTillNextLevel = new int[] { 1, 2, 2, 2, 2, 2, 100 };
        succesfullOrdersInThisLevel = 0;


        score = 0;
        gameTime = 60;
        UpdateGUITV();
        gameOver = false;

        StartCoroutine(GameTimeContinuous());

        //Customer
        //StartCoroutine(SpawnPassants()); //use this if you prefer the legacy code instead of the following line
        StartCoroutine(PassantsTest());

        //waffle
        order = new int[0];

        SetRightDifficulty();

        createOrder();
        createWaffle();
        createCustomer();
    }

    void Update()
    {
        UpdateGUITV();
    }

    //finds a lane with as few as possible passants 
    int GiveFreeLane()
    {
        int freeLane = (int)(Random.value * spawnPositionsPassants.GetLength(0)); //start search position
        int searchedLanes = 0;
        int lanes = spawnPositionsPassants.GetLength(0);
        int run = 0;
        
        while (true)//risky risky
        {
            for(;searchedLanes<lanes;searchedLanes++)
            {
                if(freeLane==lanes) //we do not count starting at 0, so check if we are still in the valid range
                {
                    freeLane = 0;
                }

                if (laneAssignments[freeLane]==run)
                {
                    return freeLane;
                }
                else
                {
                    freeLane++;
                }
            }
            searchedLanes = 0;
            run++;//if there is no other way, then we put more than one passant on a lane
        }
        return 0;//this should not happen
    }

    //spawns passants over time. Uses GiveFreeLane() to find a free lane. This function spawns int maxPassants
    IEnumerator PassantsTest()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (maxPassants > counterPassants)
            {
                int freeLane = GiveFreeLane();//on which lane is little going on?

                if (Random.value > 0.5)//decide whether the passant starts on the left or right
                {
                    spawnPositionsPassants[freeLane].x *= -1;//whether the if query is true or false does not define whether the passant spawns left or right, but whether it spawns on the same side as the previous
                    spawnRotationPassants[freeLane] = 180 - spawnRotationPassants[freeLane];
                }

                var tempPassant = (GameObject)Instantiate(passant, spawnPositionsPassants[freeLane], Quaternion.identity);
                MovePassant MovePassantScript = (MovePassant)tempPassant.GetComponent(typeof(MovePassant));
                MovePassantScript.AddRotation(spawnRotationPassants[freeLane]);

                MovePassantScript.AddLaneDescription(freeLane);
                counterPassants++;
                laneAssignments[freeLane]++;//now one is more on the road
            }
            yield return new WaitForSeconds(spawnWait);
        }
    }

    //LEGACY CODE
    //This function works similar to PassantsTest, but is way quicker. It determines lanes to passants and spawns passants, but the collision avoidance is in PassantsTest() way better
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

    //Gets called by passant to tell GameController he disappeared
    public void ReduceCounterPassants(int lane)
    {
        counterPassants--;
        laneAssignments[lane]--;
        Debug.Log("Passantenanzahl: " + counterPassants);
    }

    //increase score by newScoreValue
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
    }

    //Updates all numbers in GUITC
    void UpdateGUITV()
    {
        difficultyTextNumber.text = difficulty.ToString("0");
        gameTimeTextNumber.text = gameTime.ToString("00");
        scoreTextNumber.text = score.ToString("0000");
    }

    //Creates a new Order. Max length is 5.
    void createOrder()
    {
        order = new int[0];//clear
        order = new int[5] { -1, -1, -1, -1, -1 };
        for (int i=0;i<orderSize;i++)
        {
            order[i] = (int)Random.Range(0, iceSorten);
        }
        System.Array.Sort(order);//we need a sorted array later in isIceReady()
        orderValue = ValueOfOrder();
    }

    //gets called by Speechbubble to determine when it has to delete itself
    public float SpeechbubbleDestroyTime()
    {
        return difficultySpeechBubbledisappear[difficulty];
    }

    //after some time the value of an order will get decreased.
    IEnumerator DecreaseValueOfOrder()
    {
        yield return new WaitForSeconds(difficultyMinTime[difficulty]);
        int decreaseWindow = difficultyMaxTime[difficulty] - difficultyMinTime[difficulty];
        float decreaseValue = orderValue / decreaseWindow;
        while (orderValue >= 10)
        {
            orderValue -= decreaseValue*Time.deltaTime;
            yield return null;//TODO
        }
    }

    //gets called by customer to start coroutine DecreaseValueOfOrder()
    public void StartPointDecay()
    {
        StartCoroutine("DecreaseValueOfOrder");
    }

    //gets called by customer to stop coroutine DecreaseValueOfOrder()
    public void StopPointDecay()
    {
        StopCoroutine("DecreaseValueOfOrder");
    }

    //determines difficulty and changes necessary variables
    public void SetRightDifficulty()
    {
        if(succesfullOrdersInThisLevel>=succesfullOrdersTillNextLevel[difficulty])
        {
            IncreaseDifficulty();
        }
        orderSize = diffcultyOrderSize[difficulty];
        iceSorten = difficultyIceSorten[difficulty];
    }

    //increase difficulty
    public void IncreaseDifficulty()
    {
        difficulty++;
        succesfullOrdersInThisLevel = 0;
    }

    //decrease difficulty
    public void DecreaseDifficulty()
    {
        difficulty--;
        succesfullOrdersInThisLevel = 0;
    }

    //Initializes the value of an order
    int ValueOfOrder()
    {
        return (int)difficultyLevel[difficulty] * ((orderSize*10) + (iceSorten*5));
    }

    //Reduces the remaining game time in each frame. Calls GameOver if no time is left
    IEnumerator GameTimeContinuous()
    {
        while(gameTime>0)
        {
            gameTime -= Time.deltaTime;
            yield return null;
        }
        GameOver();
        yield return null;
    }

    //Increase gameTime
    private void GameTimeIncrease(int plusTime)
    {
        gameTime += plusTime;
    }

    //Game is over. Clears the field, show scoreboard and stops all coroutines
    private void GameOver()
    {
        customerMovementScript.setServed();
        scoreboard.GameOver(score);
        StopAllCoroutines();
        gameOver = true;
    }

    //creates a new Waffle for player
    public void createWaffle()
    {
        Vector3 position = new Vector3(0.313f, 1.3859f, 0.4840664f);
        waffleClone = (GameObject)Instantiate(waffle, position, transform.rotation);
        waffleBehaviorScript = (WaffleBehavior)waffleClone.GetComponent(typeof(WaffleBehavior));
    }

    //creates a new Customer. Will approach the van
    void createCustomer()
    {
        Vector3 position = new Vector3(0.09f, 1.0f, 8.0f);
        customerClone = (GameObject)Instantiate(customer, position, transform.rotation);
        customerMovementScript = (CustomerMovement)customerClone.GetComponent(typeof(CustomerMovement));
    }

    //Checks whether the waffle contains the order
    bool isIceReady()
    {
        int[] iceBalls = waffleBehaviorScript.PullIceballs();
        if(iceBalls.GetLength(0)!=order.GetLength(0))
        {
            return false;
        }
        System.Array.Sort(iceBalls);
        bool areEqual = true;
        for (int i=0;i<order.GetLength(0);i++)
        {
            if(order[i]!=iceBalls[i])
            {
                areEqual = false;
            }
        }
        return areEqual;
    }

    //gets called by the droppoint, when the player tries to serve the customer. Checks if the waffle is correct with isIceReady(). Then restores a playable state. Bool is need for droppoint so it can play the appropriate sound
    public bool IceIsInDelivery()
    {
        Debug.Log("IceIsInDelivery()");
        if(isIceReady() && !gameOver)
        {
            succesfullOrdersInThisLevel++;
            SetRightDifficulty();
            tutorial.SetActive(false);
            score += getScoreValue();
            GameTimeIncrease(10-difficulty);
            waffleBehaviorScript.DestroyWaffle();
            customerMovementScript.setServed();
            createWaffle();
            createCustomer();
            createOrder();
            return true;
        }
        customerMovementScript.makeOhSound();
        return false;
    }

    //gets called if the trashcan is used. Destroys the waffle and creates a new one
    public void TrashcanReset()
    {
        waffleBehaviorScript.DestroyWaffle();
        createWaffle();
    }

    //returns the current score as an int
    private int getScoreValue()
    {
        return (int)orderValue;
    }

    //used by speechbubble to get current order
    public int[] getOrder()
    {
        return order;
    }
}
