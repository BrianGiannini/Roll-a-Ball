using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text counText;
	public Text winText;

	private Rigidbody rb;
	private int count;
	private float timeFinish;

	Vector3 zeroAc;
	Vector3 curAc;
	float sensH = 5;
	float sensV = 5;
	float smooth = 0.5f;
	float GetAxisH = 0;
	float GetAxisV = 0;
	Vector3 movement;

	void ResetAxes(){
		zeroAc = Input.acceleration;
		curAc = Vector3.zero;
	}

	void Start()
	{
		ResetAxes();
		rb = GetComponent < Rigidbody> ();
		count = 0;
		timeFinish = 0f;
		setCountText ();
		winText.text = "";
	}

	void FixedUpdate()
	{
		if (Application.platform == RuntimePlatform.Android) {
			curAc = Vector3.Lerp(curAc, Input.acceleration-zeroAc, Time.deltaTime/smooth);
			GetAxisV = Mathf.Clamp(curAc.y * sensV, -1, 1);
			GetAxisH = Mathf.Clamp(curAc.x * sensH, -1, 1);
			movement = new Vector3 (GetAxisH, 0.0f, GetAxisV);
		} else {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		}

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject.CompareTag("Pickup"))
		{
			other.gameObject.SetActive(false);
			count=count+1;
			setCountText();
		}
	}

	void setCountText()
	{
		counText.text = "Count: " + count.ToString ();
		if (count >= 9) 
		{
			timeFinish = Time.timeSinceLevelLoad;
			winText.text="You Win in : " + timeFinish.ToString()+" sec";
		}
	}
}