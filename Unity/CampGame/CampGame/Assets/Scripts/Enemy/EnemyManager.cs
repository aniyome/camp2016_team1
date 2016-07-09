using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	// make enemy object setting
	public GameObject AddEnemyObject;

	// Boss Enemy Object
	public GameObject BossEnemyObject;

	// minimum enemy count
	public float MinimumCount = 10;

	// enemy count
	private int EnemyCount;

	// enemy object
	private GameObject[] EnemyObject;

	// Use this for initialization
	void Start () {
		// FindGameObject
		EnemyObject = GameObject.FindGameObjectsWithTag("Enemy");

		EnemyCount = EnemyObject.Length;
	}

	// Update is called once per frame
	void Update () {
		// FindGameObject
		EnemyObject = GameObject.FindGameObjectsWithTag("Enemy");
		EnemyCount = EnemyObject.Length;

		if (EnemyCount < MinimumCount) {
			GameObject.Instantiate(AddEnemyObject, new Vector3(0, 0, 0), Quaternion.identity);
		}
	}

	// Boss Appear
	public void AppearBossEnemy() {
		BossEnemyObject.SetActive (true);
	}
}
