using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {
	public Boundary boundary;
	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

	private float currentSpeed;
	private float targetManeuver;

    private Rigidbody body;

	public void Start () {
        body = GetComponent<Rigidbody>();
		currentSpeed = body.velocity.z;
		StartCoroutine(Evade());
	}
	
	private IEnumerator Evade () {
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
		while (true) {
			targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
	
	public void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards(body.velocity.x, targetManeuver, smoothing * Time.deltaTime);
		body.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
		body.position = new Vector3(
			Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);
		
		body.rotation = Quaternion.Euler(0, 0, body.velocity.x * -tilt);
	}
}
