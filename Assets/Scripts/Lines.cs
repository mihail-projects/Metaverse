using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour {

    public Transform[] targets;
    Vector3[] points;
    LineRenderer line;

    void Start(){
        points = new Vector3[targets.Length];
    }

    void Update(){
        for(int i = 0; i < targets.Length; i++){
            points[i] = targets[i].position;
        }
        GetComponent<LineRenderer>().SetPositions(points);
    }

}
