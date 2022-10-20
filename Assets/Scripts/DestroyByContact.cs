using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

    private int BOUNDARY_LAYER = 10;

	public void Start () {
		gameController = SceneContext.Instance.GameController;
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	public void OnTriggerEnter (Collider other) {
        if (other.gameObject.layer == BOUNDARY_LAYER) {
            // No destroying the boundary due to contact.
            return;
        }

		if (explosion != null) {
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.gameObject.GetComponent<PlayerController>() != null) {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}

		EventManager.Instance.ScoreEarned.Invoke(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}