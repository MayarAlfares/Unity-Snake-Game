using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  private Transform target;
  private float speed = 2f;
  private Vector3 speedRot = Vector3.right * 50f;
  
  void Start () {
      target = GameObject.FindGameObjectWithTag("Player").transform;
  }
 
  void Update () {
      transform.Rotate (speedRot * Time.deltaTime);
      transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
  }
}
