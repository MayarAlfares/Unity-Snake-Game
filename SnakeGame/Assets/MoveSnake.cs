using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSnake : MonoBehaviour {
	

	public List<Transform> BodyParts = new List<Transform>();
	public int beginsize;
	public float minDistance = 0.25F; //so that parts stay sequential
	public float speed = 1;
	public float rotationSpeed = 50;
	public float timeFromLastRetry;

	public GameObject bodyprefab;
	public float curspeed = 1;
	

	public GameObject deathScreen;

	public Text curScore;

	public Text finalScore;

	public bool isAlive;

	private float dis;
	private Transform curBodyPart;
	private Transform PrevBodyPart;


	



	// Use this for initialization
	void Start () {
		StartLevel();
	}

	public void StartLevel(){

		timeFromLastRetry = Time.time;
		deathScreen.SetActive(false);



		for(int i = BodyParts.Count - 1; i>0 ; i-- ){ 
			Destroy(BodyParts[i].gameObject);
			BodyParts.Remove(BodyParts[i]);
		}


		BodyParts[0].position = new Vector3(0, 0.5f, 0);
		BodyParts[0].rotation = Quaternion.identity;

		curScore.gameObject.SetActive(true);
		curScore.text = "Score: 0";
		isAlive = true;

		for(int i = 0; i < beginsize - 1; i++){
			AddBodyPart();
		}

		BodyParts[0].position = new Vector3(2, 0.5f, 0);

		//SpawnWalls();

	}
	
	// Update is called once per frame
	void Update () {
		if(BodyParts.Count<3)
			Die();
		if(isAlive)
			Move();

		if(Input.GetKey(KeyCode.Q)){
			AddBodyPart();
		}

		
	}

	public void EndGame(){
		Application.Quit();
	}

	public void Pause(){
         if (Time.timeScale == 1)
         {
             Time.timeScale = 0;
         }
         else
         {
             Time.timeScale = 1;
         }
     }

	 public void changeSpeed(){
		 if(speed == 1)
			 speed =2;
		 else if (speed == 2)
			 speed = 4;
		 else
		 	speed = 1;
	 }

	public void Move(){

		curspeed = speed;

		if (Input.GetKey(KeyCode.W)){
			curspeed*=2;
		}

		BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

		//make sure every body part is at the exact same position
		if(Input.GetAxis("Horizontal") != 0)
			BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal") );

		for(int i=1; i< BodyParts.Count; i++){
			curBodyPart = BodyParts[i];
			PrevBodyPart = BodyParts[i-1];

			dis = Vector3.Distance(PrevBodyPart.position, curBodyPart.position);

			Vector3 newpos = PrevBodyPart.position;

			newpos.y = BodyParts[0].position.y;

			float T= Time.deltaTime *dis / minDistance * curspeed;  // if it is far it will move faster than if it is close

			if (T > 0.5f)
				T=0.5f;
			curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
			curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);

		}
	}



	public void AddBodyPart(){
		Transform newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count -1].position, BodyParts[BodyParts.Count -1].rotation) as GameObject).transform;
		newpart.SetParent(transform);
		BodyParts.Add(newpart);

		curScore.text = "Score: " + (BodyParts.Count - beginsize).ToString();
	}

	public void Die(){
		isAlive = false;
		finalScore.text = "Your Score is: " + (BodyParts.Count - beginsize).ToString();
		curScore.gameObject.SetActive(false);
		deathScreen.SetActive(true);
	}



	

}
