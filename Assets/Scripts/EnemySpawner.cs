using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;                // enemy object
    private float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    private float time;                     //keeping time
    public string level;                    // Level being played
    System.Random random = new System.Random(); //Used for random number generation
    int rand;
    private bool one = false;               // Used for level check
    private bool two = false;               // Used for level check
    private bool three = false;             // Used for level check

    // Use this for initialization
    void Start () {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        time = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        //After certain time periods shorten spawn time
        if (time > 20)
        {
            spawnTime = 2f;
        }
        else if (time > 30)
        {
            spawnTime = 1f;
        }
        // Stop spawning once game over
        if (GameControl.instance.gameOver == true)
        {
            CancelInvoke("Spawn");
        }
    }

  
    void Spawn()
    {
        //Check level is equal to find out which one is being played
        one = string.Equals(level, "One");
        two = string.Equals(level, "Two");
        three = string.Equals(level, "Three");

        if (one == true)
        {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, (spawnPoints.Length - 1));


            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            Instantiate(enemy, spawnPoints[spawnPointIndex + 1].position, spawnPoints[spawnPointIndex + 1].rotation);
        }
        else if (two == true)
        {
            //get a random number either 1 or 2
             rand = random.Next(1, 2);

            // Create an instance of the enemy prefab at the selected spawn point's position and rotation.
            Instantiate(enemy, spawnPoints[0].position, spawnPoints[0].rotation);
            Instantiate(enemy, spawnPoints[3].position, spawnPoints[3].rotation);
            Instantiate(enemy, spawnPoints[rand].position, spawnPoints[rand].rotation);

        }
        else if (three == true)
        {
            for (int i = 0; i < 4; i++)
            {
                // Create an instance of the enemy prefab at every spawn point position and rotation.
                Instantiate(enemy, spawnPoints[i].position, spawnPoints[i].rotation);
            }
        }
    }
}
