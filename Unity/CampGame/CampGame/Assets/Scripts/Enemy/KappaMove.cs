using UnityEngine;
using System.Collections;

public class KappaMove : MonoBehaviour {

	private float speed = 1f;
	private float rotationSmooth = 1f;
	private float num;
	private float num2;
	private Vector3 targetPosition;

	private float changeTargetSqrDistance = 10f;

	private Vector3 startPosition;

	private bool flag;

	// Use this for initialization
	void Start () {
		//startPosition = transform.localPosition;
	    //targetPosition = GetRandomPositionOnLevel();
		num = 3.0f;
		num2 = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		/*// 目標地点との距離が小さければ、次のランダムな目標地点を設定する
		float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.localPosition - targetPosition);
		if (sqrDistanceToTarget < changeTargetSqrDistance)
		{
		    targetPosition = GetRandomPositionOnLevel();
			//flag = false;
			//targetPosition = startPosition;
		}

		// 目標地点の方向を向く
		Quaternion targetRotation = Quaternion.LookRotation(startPosition - transform.localPosition);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

		// 前方に進む
		transform.Translate(Vector3.forward * speed * Time.deltaTime);*/
		this.transform.Translate (0.2f, 0, 0.2f);
		this.transform.Rotate(0, num, 0);
	}

	public Vector3 GetRandomPositionOnLevel()
	{
		float levelSize = 55f;
		return new Vector3(Random.Range(-levelSize, levelSize), startPosition.y, Random.Range(-levelSize, levelSize));
	}
}
