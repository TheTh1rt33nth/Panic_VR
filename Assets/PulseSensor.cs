
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
//using System.IO.Ports;
using System.Threading;
public class PulseSensor : MonoBehaviour
{
    public int val;
    public Staship Staship; 
   // public SerialPort serial;

    public float changeAcceleration;
    [HideInInspector]
    public float changeSpeed;
    float curVal;
    // Start is called before the first frame update
    void Start()
    {
        curVal = 100;
        changeSpeed = 0;
        //serial = new SerialPort("COM3", 115200);
        //serial.Open();
       StartCoroutine(GenerateValues());
    }

    IEnumerator GenerateValues ()
    {
        while (true)
        {
            //string inputData = serial.ReadLine();
            try
            {
                changeSpeed += UnityEngine.Random.Range(-changeAcceleration, changeAcceleration);
                curVal += changeSpeed;
                curVal = Mathf.Round(  Mathf.Clamp(curVal,75, 140));
                

                //val = Convert.ToInt32(inputData.Split('.')[0]);
                Staship.pulse = curVal;
                //Debug.Log("PulseSensorSays: " + val);
            }
            catch (System.Exception ex)
            {
                Debug.Log("We fucked up for a reason: " + ex.Message);
            }
            yield return new WaitForSeconds(0.33f);
        }
    }

}
