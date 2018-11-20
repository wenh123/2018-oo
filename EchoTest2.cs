using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
//<summary> //
// 雷達666  //
// 雷達777  //
// 雷達888  //
//</summary>//
/*
                                         大家的工作效率都變好了
    
*/
public class EchoTest : MonoBehaviour {
    public float
        SteeringWheel,
        ThrottlePedal,
        BrakePedal,
        HandBrake,
        Wipers,
        Headlights,
        Babas;
    public string Gear;
    public Text GearText;
    public GameObject SteeringWheelobj;
    private void Awake()
    {
        SteeringWheel = 540;
        ThrottlePedal = 0;
        BrakePedal = 0;
        HandBrake = 0;
        Wipers = 0;
        Headlights = 0;
        Babas = 0;

    }
    private void Update()
    {
        Quaternion Steering = Quaternion.Euler(0f, 0f, -(SteeringWheel - 540));
        SteeringWheelobj.transform.localRotation = Steering;
        GearText.text = Gear;
    }
    IEnumerator Start() {
        //	WebSocket w = new WebSocket(new Uri("ws://echo.websocket.org"));
        // WebSocket w = new WebSocket(new Uri("ws://127.0.0.1:8877"));
        //yield return StartCoroutine(w.Connect());

        sp = new SerialPort("COM3", 115200);

        sp.ReadTimeout = 10;
        sp.Open();
/* 
        receiveThread = new Thread(ReceiveThread);
        receiveThread.IsBackground = true;
        receiveThread.Start();

        private void ReceiveThread()
        {
            while (true)
            {
                if (this.sp != null && this.sp.IsOpen)
                {
                    try
                    {
                        String strRec = sp.ReadLine();            //SerialPort读取数据有多种方法，我这里根据需要使用了ReadLine()
                        Debug.Log("Receive From Serial: " + strRec);
                    }
                    catch
                    {
                        //continue;
                    }
                }
            }
        }
*/
        int a, b, c, d, e, f, g,h;
        string reply;
        while (true)
        {
         //   reply = w.RecvString();
         reply = sp.ReadLine();

            if (reply != null)
            {
                //切割json
                string []split = reply.Split(new Char[] {'\"',' ', '\'', '{', ':', ',', '.', ':', '\t', '}' });
		
		        //找出相對位置

                 a = Array.IndexOf(split, "SteeringWheel");
                 SteeringWheel = Convert.ToSingle(split[a + 3]);
                 b = Array.IndexOf(split, "ThrottlePedal");
                 ThrottlePedal = Convert.ToSingle(split[b + 3]);
                 c = Array.IndexOf(split, "BrakePedal");
                 BrakePedal = Convert.ToSingle(split[c + 3]);
                 d = Array.IndexOf(split, "HandBrake");
                 HandBrake = Convert.ToSingle(split[d + 3]);
                 e = Array.IndexOf(split, "Wipers");
                 Wipers = Convert.ToSingle(split[e + 3]);
                 f = Array.IndexOf(split, "Headlights");
                 Headlights = Convert.ToSingle(split[f + 3]);
                 g = Array.IndexOf(split, "Baba");
                 Babas = Convert.ToSingle(split[g + 3]);

                 h = Array.IndexOf(split, "Gear");
                 Gear =split[h + 4];         
                
                //檔位 split[34] : split[38]
                //方向0~1080
                //油門0-100
                // BrakePedal = Convert.ToSingle(split[25]);       //剎車0-100
                /* float HandBrake = Convert.ToSingle(split[31]);      //手剎車 0放下 1拉起
                 float Wipers = Convert.ToSingle(split[53]);         //雨刷
                 float Headlights = Convert.ToSingle(split[59]);     //大燈
                 float Babas = Convert.ToSingle(split[65]);          //喇叭
                 string Gear = Convert.ToString(split[38]);          //檔位*/
                /*
                 相對應位置
                 DeviceModel:ABCD    split[2]  : split[6]
                 SteeringWheel: 510  split[10] : split[13]
                 ThrottlePedal:40    split[16] : split[19]
                 BrakePedal: 55      split[22] : split[25]
                 HandBrake: 0        split[28] : split[31]
                 Gear: N             split[34] : split[38]
                 TurnSignal:Right    split[42] : split[46]
                 Wipers: 0           split[50] : split[53]
                 Headlights: 1       split[56] : split[59]
                 Baba: 1             split[62] : split[65]
                 */
               Debug.Log(reply);
            }
            if (w.error != null || reply == null)
            {
                Debug.Log("NULL");
                //yield return StartCoroutine(w.Connect());

            }
           yield return 0;
        }
      
       // w.Close();
       sp.Close();
    }

   
}
