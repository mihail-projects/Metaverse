using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class test : MonoBehaviour
{

    public Transform[] spheresLeft, spheresRight;
    Rigidbody[] rbsLeft, rbsRight;
    bool flag;
    Process[] processes;
    public int speed;

    void Start()
    {
        //Process.Start("detector.exe");
        /*rbsLeft = new Rigidbody[spheresLeft.Length];
        for (int i = 0; i < spheresLeft.Length; i++) rbsLeft[i] = spheresLeft[i].GetComponent<Rigidbody>();
        rbsRight = new Rigidbody[spheresRight.Length];
        for (int i = 0; i < spheresRight.Length; i++) rbsRight[i] = spheresRight[i].GetComponent<Rigidbody>();*/
    }

    void Update()
    {
        if (!flag)
        {
            processes = Process.GetProcessesByName("Python");
        }
        if (processes.Length > 0) set();
    }

    void set(){

        if (!flag){
            flag = true;
        }

        if(File.Exists("C:/Metaverse/full0.csv")){

            //Hand0
            var lines = File.ReadAllLines("C:/Metaverse/full0.csv");

            var columns = lines[0].Split(',');
            var fColumns = new List<float>(columns.Length);

            foreach (var column in columns) fColumns.Add(float.Parse(column));
            
            var j = 1;

            for (int i = 0; i < 21; i++){
                spheresLeft[i].localPosition = Vector3.MoveTowards(spheresLeft[i].localPosition, new Vector3(fColumns[j - 1], fColumns[j], fColumns[j + 1]) * -1, Time.deltaTime * speed);
                //rbsLeft[i].MovePosition(spheresLeft[i].TransformPoint(new Vector3(fColumns[j-1], fColumns[j], fColumns[j+1]*-1.0f)));
                j += 3;
            }

            /*GameObject.Find("Left").transform.Find("Origin").transform.localPosition = Vector3.MoveTowards(GameObject.Find("Left").transform.Find("Origin").transform.localPosition,
                                                                                                        new Vector3(GameObject.Find("Left").transform.Find("Origin").transform.localPosition.x,
                                                                                                                    GameObject.Find("Left").transform.Find("Origin").transform.localPosition.y,
                                                                                                                    remap(Vector2.Distance(new Vector2(fColumns[0], fColumns[1]), new Vector2(fColumns[13], fColumns[14])), .8f, 1.3f, -.5f, .5f)),
                                                                                                        Time.deltaTime * speed);*/

            FileStream fileStream = new FileStream("C:/Metaverse/full0.csv", FileMode.Truncate);
            fileStream.SetLength(0);
            fileStream.Close();

            System.IO.File.Move("C:/Metaverse/full0.csv", "C:/Metaverse/empty0.csv");

        }

        if(File.Exists("C:/Metaverse/full1.csv")){

            //Hand1
            var lines = File.ReadAllLines("C:/Metaverse/full1.csv");

            var columns = lines[0].Split(',');
            var fColumns = new List<float>(columns.Length);

            foreach (var column in columns) fColumns.Add(float.Parse(column));

            var j = 1;

            for (int i = 0; i < 21; i++){
                spheresRight[i].localPosition = Vector3.MoveTowards(spheresRight[i].localPosition, new Vector3(fColumns[j - 1], fColumns[j], fColumns[j + 1]) * -1, Time.deltaTime * speed);
                //rbsRight[i].MovePosition(spheresRight[i].TransformPoint(new Vector3(fColumns[j-1], fColumns[j], fColumns[j+1]*-1.0f)));
                j += 3;
            }

            /*GameObject.Find("Right").transform.Find("Origin").transform.localPosition = Vector3.MoveTowards(GameObject.Find("Right").transform.Find("Origin").transform.localPosition,
                                                                                                        new Vector3(GameObject.Find("Right").transform.Find("Origin").transform.localPosition.x,
                                                                                                                    GameObject.Find("Right").transform.Find("Origin").transform.localPosition.y,
                                                                                                                    remap(Vector2.Distance(new Vector2(fColumns[0], fColumns[1]), new Vector2(fColumns[13], fColumns[14])), .8f, 1.3f, -.5f, .5f)),
                                                                                                        Time.deltaTime * speed);*/

            FileStream fileStream = new FileStream("C:/Metaverse/full1.csv", FileMode.Truncate);
            fileStream.SetLength(0);
            fileStream.Close();

            System.IO.File.Move("C:/Metaverse/full1.csv", "C:/Metaverse/empty1.csv");
        
        }
        /*Vector3[] pos = new Vector3[3];
        pos[0] = Vector3.MoveTowards(pos[0], spheresRight[7].position, Time.deltaTime*10);
        pos[1] = Vector3.MoveTowards(pos[1], spheresRight[8].position, Time.deltaTime*10);
        pos[2] = pos[0] + (pos[1] - pos[0]).normalized * 10;
        GameObject.Find("Line").GetComponent<LineRenderer>().SetPositions(pos);*/

    }

    float remap(float value, float aLow, float aHigh, float bLow, float bHigh){
        float normal = Mathf.InverseLerp(aLow, aHigh, value);
        float bValue = Mathf.Lerp(bLow, bHigh, normal);
        return bValue;
    }

}


