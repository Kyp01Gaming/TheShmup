using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    // Reference to bullet
    public GameObject bullet;
    
    // Private variables
    private float shotTimer = 0;
    private float speed;
    private float nextShot = 1;

    // Shooting time
    public float minShotTime = 1.5f;
    public float maxShotTime = 3.5f;

    // Reference to own Transform
    private Transform myTransform;

	// Use this for initialization
	void Awake () {
        // Sets reference to transform
        myTransform = gameObject.transform;
        // Set speed to a random range to add diversity
        speed = Random.Range(2, 5);
	}
	
	// Update is called once per frame
	void Update () {
        // Shooting Logic
        shotTimer += Time.deltaTime;
        if (shotTimer > nextShot)
        {
            GameObject reff = Instantiate(bullet, myTransform.position + (myTransform.forward * 1.25f), myTransform.rotation) as GameObject;
            reff.tag = "Enemy";
            shotTimer = 0;
            nextShot = Random.Range(1.5f, 3.5f);
        }

        // Movement
        myTransform.position += myTransform.forward * speed * Time.deltaTime;
    }

    public void Hurt()
    {
        GameManager.refer.IncreaseScore(10);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("Hurt");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag != "Enemy")
            Destroy(gameObject);
    }
}
