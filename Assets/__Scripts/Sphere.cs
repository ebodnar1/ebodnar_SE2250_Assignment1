using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sphere : MonoBehaviour
{

    //Stores the different items that have been collected
    public List<string> collectables;

    //Tracks the score
    private int count;

    //Text to display the score and time
    public Text score;
    public Text currentTime;

    //Tracks the current time
    private float timer;
    private bool timerIsRunning;

    //Create a private RigidBody object
    private Rigidbody rigidBody;

    //Allow for more precise movement with new Input System
    public float speed = 0.0f;

    /*
     * Initialize collectables as an empty list, rigidBody as a RigidBody, 
     * the initial count as 0, and calls the incScore function to
     * get the scoreboard on the screen upon starting
    */
    private void Start()
    {
        collectables = new List<string>();
        rigidBody = GetComponent<Rigidbody>();
        count = 0;
        incScore();
        timer = 0.0f;
        timerIsRunning = true;
        displayTime();
    }

    /*
     * Adds a force to the ball based on the user input, and increments
     * the elapsed time every FixedUpdate (more frequent than Update),
     * and calls the displayTime function to ensure the
     * user always sees the correct time that has elapsed,
     * stopping the timer when the user wins so they can see their final time
    */
    private Vector3 oldVelocity;
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidBody.AddForce(move * speed * Time.deltaTime);
        oldVelocity = rigidBody.GetPointVelocity(rigidBody.position);

        if (timerIsRunning)
        {
            timer += Time.deltaTime;
            displayTime();
        }
    }

    /*
     * Function to handle item pickups via trigger collisions:
     * If the user collides with an object of tag "Collectable", 
     * get the itemType (as defined in CollectableItems) upon each
     * trigger collision, then add that to the list of collected items.
     * Then, set the collected item to inactive so it dissapears, and determine
     * the number of points earned by collecting that item, then increment the 
     * displayed score by calling incScore function
    */
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            string itemType = collision.gameObject.GetComponent<CollectableItems>().itemType;
            collectables.Add(itemType);
            Destroy(collision.gameObject);
            switch (itemType)
            {
                case "cube":
                    count ++;
                    break;
                case "cylinder":
                    count += 2;
                    break;
                case "star":
                    count += 3;
                    break;
                case "compound":
                    count += 10;
                    break;
                default:
                    count += 0;
                    break;
            }
            incScore();
        }
    }

    /*
     * Displays the following:
     *      Current Score: score (number)
     * If the score reaches 35 (max score), then "You Win!"
     * is displayed onscreen, and we start a coroutine
     * with a 2 second delay, as well as turn the timer off
    */
   void incScore()
    {
        score.text = "Current Score: " + count.ToString();
        if(count == 35)
        {
            score.text = "You win!";
            StartCoroutine(ExecuteAfterTime(2));
            timerIsRunning = false;
        }
    }

    /*
     * Shows the time in the form hours:minutes, with 0.0s being 00:00
     * Displays this in the top left corner of the screen
    */
    void displayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        currentTime.text = string.Format("Time Elapsed: {0:00}:{1:00}", minutes, seconds);
    }

    /*
     * When the sphere (player) collides with a wall, it will be reflected
     * (as light does off a mirror) at half the velocity it had before contact
     * and rotates the ball on collision too
    */
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.contacts[0];
        Vector3 reflectedVelocity = Vector3.Reflect(0.50f * oldVelocity, cp.normal);
        rigidBody.velocity = reflectedVelocity;

        Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
        transform.rotation = rotation * transform.rotation;
    }

    /*
     * This is the coroutine that was called earlier, waiting for 2 seconds
     * before restarting the scene - and thus the entire game
    */
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("SampleScene");
    }
}