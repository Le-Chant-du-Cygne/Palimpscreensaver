using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float minTimeToEnd;
    public float maxTimeToEnd;

    private Generator generator;
    private Transform corridors;

    private Vector3 lastPosition;
    private float currentSpeed;
    private float currentMaxSpeed;
    private float currentTimeToEnd;
    private float endTimer;

	void Start ()
    {
        generator = GameObject.FindObjectOfType<Generator>();
        corridors = GameObject.Find("Corridors").transform;

        lastPosition = Vector3.zero;
        currentSpeed = 0f;
        currentMaxSpeed = Random.Range(minSpeed, maxSpeed);
        currentTimeToEnd = Random.Range(minTimeToEnd, maxTimeToEnd);
        endTimer = 0f;
    }
	
	void Update ()
    {
        // Movement
        float currentSpeed = Interpolate.Ease(Interpolate.EaseType.EaseOutQuad)(minSpeed, currentMaxSpeed, endTimer, currentTimeToEnd);
        transform.Translate(new Vector3(0f, 0f, currentSpeed) * Time.deltaTime);
        endTimer += Time.deltaTime;

        // Reset parameters
        if (endTimer >= currentTimeToEnd)
        {
            currentMaxSpeed = Random.Range(minSpeed, maxSpeed);
            currentTimeToEnd = Random.Range(minTimeToEnd, maxTimeToEnd);
            endTimer = 0f;
        }

        // Destroy old corridors
        if (transform.position.z - lastPosition.z > 4f * generator.maxScale)
        {
            Destroy(corridors.GetChild(0).gameObject);
            lastPosition = transform.position;
        }
	}
}
