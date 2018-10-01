using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour {


	public MoveSnake movement;
	public Pickup PU;

	public bool EnemyShield = false;
	public bool NoCollisions = false;
	public bool blackFlag = false;
	public bool BodyShield = false;

	public GameObject blackScreen;

	public Text Power;


	void OnCollisionEnter(Collision col){
		if(!NoCollisions){
		
			if (col.gameObject.tag == "Food"){
				if(PU.randomIndex<6) //green
				movement.AddBodyPart();

				else if(PU.randomIndex<10){ // red
					for(int i =0; i<2; i++){
						Transform lastPart = movement.BodyParts[movement.BodyParts.Count-1];
						movement.BodyParts.Remove(lastPart);
						Destroy(lastPart.gameObject);
					}
				}else if (PU.randomIndex<13){  //orange
					//Transform headPart = movement.BodyParts[0];
					//headPart.Translate(movement.BodyParts[movement.BodyParts.Count -1].forward * movement.curspeed * Time.smoothDeltaTime, Space.World);


				}else if (PU.randomIndex<14){ //yellow
					StartCoroutine(StartKillEnemy());

				}else if (PU.randomIndex<16){ //purple
					StartCoroutine(StartBodyPass());
					
				}else if (PU.randomIndex<18){ //blue
					StartCoroutine(StartNoCollisions());

				}else if (PU.randomIndex<19){ //black
					StartCoroutine(StartBlack());

				}

				Destroy(col.gameObject);

				//spawn new food
				PU.SpawnFood();
			}
			else if (col.gameObject.tag == "Breakable"){
				Destroy(col.gameObject);
				if (movement.BodyParts.Count >8){
					for(int i=0; i<8; i++){
						Transform lastPart = movement.BodyParts[movement.BodyParts.Count-1];
						movement.BodyParts.Remove(lastPart);
						Destroy(lastPart.gameObject);
					}
					PU.SpawnBreakable();
				}
				else
					movement.Die();

			}
			else if (col.gameObject.tag == "Enemy" ){ // assume 1 enemy
				Destroy(col.gameObject);
				if(!EnemyShield){
					if (movement.BodyParts.Count >6){
						for(int i=0; i<6; i++){
							Transform lastPart = movement.BodyParts[movement.BodyParts.Count-1];
							movement.BodyParts.Remove(lastPart);
							Destroy(lastPart.gameObject);
						}
					}
					else
						movement.Die();
				}
			}else if (col.gameObject.tag == "Death" ){ //out of boundaries
					movement.Die();

			}else{
				if(col.transform != movement.BodyParts[1] && movement.isAlive ){ 
					if(Time.time - movement.timeFromLastRetry >5 )
						if(!BodyShield){
							if (movement.BodyParts.Count >10) //own body
								for(int i=0; i<10; i++){
									Transform lastPart = movement.BodyParts[movement.BodyParts.Count-1];
									movement.BodyParts.Remove(lastPart);
									Destroy(lastPart.gameObject);
								}

							else
								movement.Die();
						}
				}

			}
		}
	}




	////////////////////// special traits coroutines /////////////////////////////////////////

	IEnumerator StartKillEnemy(){
		const float MAX_TIME = 10.0f; // time to wait before resetting the flag
		EnemyShield = true;
		float timeNow = 0.0f;
		Power.gameObject.SetActive(true);
		Power.text = "KILL THE ENEMY";
		while ( timeNow < MAX_TIME && EnemyShield )
		{
		timeNow += Time.deltaTime;
		yield return new WaitForEndOfFrame();
		}
		EnemyShield = false;
		Power.gameObject.SetActive(false);

	}

	IEnumerator StartNoCollisions(){
		const float MAX_TIME = 10.0f; // time to wait before resetting the flag
		NoCollisions = true;
		float timeNow = 0.0f;
		Power.gameObject.SetActive(true);
		Power.text = "NO COLLISION";
		while ( timeNow < MAX_TIME && NoCollisions )
		{
		timeNow += Time.deltaTime;
		yield return new WaitForEndOfFrame();
		}
		NoCollisions = false;
		Power.gameObject.SetActive(false);

	}


	IEnumerator StartBodyPass(){
		const float MAX_TIME = 5.0f; // time to wait before resetting the flag
		BodyShield = true;
		float timeNow = 0.0f;
		Power.gameObject.SetActive(true);
		Power.text = "PASS THROUGH BODY ";
		while ( timeNow < MAX_TIME && BodyShield )
		{
		timeNow += Time.deltaTime;
		yield return new WaitForEndOfFrame();
		}
		BodyShield = false;
	}

	IEnumerator StartBlack(){
		const float MAX_TIME = 5.0f; // time to wait before resetting the flag
		blackScreen.SetActive(true);
		blackFlag = true;
		float timeNow = 0.0f;
		while ( timeNow < MAX_TIME && blackFlag )
		{
		timeNow += Time.deltaTime;
		yield return new WaitForEndOfFrame();
		}
		blackScreen.SetActive(false);
		blackFlag = false;
	}


}
