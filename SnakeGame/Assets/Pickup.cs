using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {


	public GameObject Foodprefab;
	public GameObject Wallprefab;
	public GameObject Breakprefab;


	public int randomIndex = 1;

	public Vector3 center;
	public Vector3 size;


	// Use this for initialization
	void Start () {
		SpawnFood();
		SpawnWalls();
		SpawnBreakable();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Q))
			SpawnFood();
			
		
	}

	public void SpawnFood(){
		Vector3 pos = center + new Vector3(Random.Range(-size.x/2 , size.x/2), Random.Range(-size.y/2 , size.y/2), Random.Range(-size.z/2 , size.z/2));
		
		randomIndex = (int)(Random.Range(1, 18));

		Color myColor = Color.green;
		
		switch (randomIndex)
      {
          case 1:
		  case 2:
		  case 3:
		  case 4:
		  case 5:
              myColor = Color.green;
              break;
          case 6:
		  case 7:
		  case 8:
		  case 9:
              myColor = Color.red;
              break;
		  case 10:
		  case 11:
		  case 12:
		  	  myColor = Color.white; // should be orange
              break;
		  case 13:
		  	  myColor = Color.yellow;
              break;
		  case 14:
		  case 15:
		  	  myColor = Color.magenta;
              break;
		  case 16:
		  case 17:
		  	  myColor = Color.blue;
              break;
          default:
               myColor = Color.black;
              break;
      }



		Foodprefab.GetComponent<Renderer> ().sharedMaterial.color = myColor;
		Instantiate(Foodprefab, pos, Quaternion.identity);
	}



	public void SpawnWalls(){ // assume only 1 wall
		Vector3 pos = center + new Vector3(Random.Range(-size.x/2 , size.x/2), Random.Range(-size.y/2 , size.y/2), Random.Range(-size.z/2 , size.z/2));
		Instantiate(Wallprefab, pos, Quaternion.identity);
	}



	public void SpawnBreakable(){ //assume only 1
		Vector3 pos = center + new Vector3(Random.Range(-size.x/2 , size.x/2), Random.Range(-size.y/2 , size.y/2), Random.Range(-size.z/2 , size.z/2));
		Instantiate(Breakprefab, pos, Quaternion.identity);
	}



	void OnDrawGizmosSelected(){

		Gizmos.color = new Color(1,0,0,0.5f); //red
		Gizmos.DrawCube(center, size);

	}


}
