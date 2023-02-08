using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class csvAnimator : MonoBehaviour
{
    private GameObject objToMove;
    [SerializeField] TextAsset csvFile;
    [SerializeField] Animator animator;
    [SerializeField] bool ikActive;
    [SerializeField] Transform head;
    [SerializeField] Transform rightHand;

    Vector3 rotateVec;
    Vector3 moveVec;
    Vector3 posVec;
    Vector3 wristVecR;
    string[] records;
    int i = 2; //2 is Human controller, 3 is AI controller
    int x = 4;

    //24-26 is head pos

    private void Start() {
        animator = GetComponent<Animator>();
        objToMove = this.gameObject;
        records = csvFile.text.Split('\n');
        //readCSV();
    }

    void Update()
    {
        if(x < records.Length-1 && Time.timeScale != 0){
            string[] fields = records[x].Split(',');
            rotateVec = new Vector3(0, float.Parse(fields[6]) * (180/Mathf.PI), 0); 
            posVec = new Vector3(float.Parse(fields[24]), float.Parse(fields[26]), float.Parse(fields[25]) + 2);
            wristVecR = new Vector3(float.Parse(fields[36]), float.Parse(fields[37]), float.Parse(fields[38]));
            //print("Time: " + fields[0] + " angle: " + float.Parse(fields[6]) * (180/Mathf.PI));
            //objToMove.transform.Rotate(rotateVec * Time.deltaTime * .9f, Space.Self);
            //objToMove.transform.rotation = Quaternion.Euler(0f, -rotateVec.y, 0f);
            objToMove.transform.localPosition = posVec;

            ////
            //code below relies on animator 36-38
            //if(animator){
                if(ikActive){
                    if(head != null){
                        //rightHand.localPosition = wristVecR;
                        head.rotation = Quaternion.Euler(-rotateVec);
                        if(Mathf.Abs((head.rotation.y * (180/Mathf.PI))) > objToMove.transform.rotation.y + 25f){
                            print("ROTATE");
                            objToMove.transform.rotation = Quaternion.Euler(0f, -(rotateVec.y-25f), 0f);
                        }
                        //animator.SetIKPosition(AvatarIKGoal.RightHand, rotateVec);
                    }
                }
            //}
            ////

            i+=2;
            x+=3;
        }
        else if(Time.timeScale != 0){
            print("FINISH ANIM");
        }
        
       
    }

    void readCSV(){
        for(int i = 1; i < records.Length - 1; i++){
            string[] fields = records[i].Split(',');

            //right now its only rotating on the X with the origin being itself
            rotateVec = new Vector3(0, float.Parse(fields[2]), 0); 
            
            moveVec = new Vector3(float.Parse(fields[4]), float.Parse(fields[5]), float.Parse(fields[6]));

            

            //objToMove.transform.Rotate(rotateVec * Time.deltaTime * .9f, Space.Self);
            //objToMove.transform.Translate(moveVec * Time.deltaTime * .9f);
        }
        rotateVec = new Vector3(0,0,0);
    }
}
