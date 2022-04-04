using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;



public class NetworkCon : MonoBehaviour
{
    // Start is called before the first frame update
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25002;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    Vector3 pos = Vector3.zero;
    int x = 0;
    public GameObject Fossa;
    MeshDeformer Deformer;
    public static Animator anim;
   public GameObject Heart_model;  
    static float beat_flag ;
    

    bool running;

    private void Update()
    {
        //Vector3 point = new Vector3((float)Xcord, (float)Zcord, (float)Ycord);
        //print(o);
        //  o++;
        //Vector3 point = new Vector3(peakX, peakY, peakZ);
        //Vector3 point = originalPos[o];
        transform.position= new Vector3(0,0,0);
        Camera.main.ScreenPointToRay(pos);
        //transform.position = pos;
        //       if (Input.anyKey)
        //     {
        Deformer = Fossa.GetComponent<MeshDeformer>();
         //MeshDeformer Deformer = hit.transform.GetComponent<MeshDeformer>(); //Look for examples grabbing mesh
        if (!Deformer)
        { //Debug.Log("Deformer is made"); 
        }

        if (Deformer)
        {
            //Debug.Log("THe code is exexuting till deformer");
            Deformer.Deform(pos);
        }

        if(beat_flag == 1)
        {
            GetComponent<Animator>().SetTrigger("Stated");
        }
    }

    private void Start()
    {
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
	    anim = GetComponent<Animator>();
        
        
	//GetComponent<Animation>().Stop();
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();


        running = true;
        while (running)
        {
            Connection();
        }
        listener.Stop();
    }

    void Connection()
    {
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        if (dataReceived != null)
        {
            if (dataReceived == "stop")
            {
                running = false;
            }
            else
            {
                pos = 1f * StringToVector3(dataReceived);
                //x = int.Parse(dataReceived);
                //print(x);
                nwStream.Write(buffer, 0, bytesRead);
            }
        }
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        
         
            string[] sArray = sVector.Split(' ');

            // store as a Vector3
            print(sVector);
            float x = float.Parse(sArray[0]);
            float y = float.Parse(sArray[1]);
            float z = float.Parse(sArray[2]);
	        beat_flag = float.Parse(sArray[3]);
            //Vector3 result = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]),float.Parse(sArray[2]));
            Vector3 result = new Vector3(x, z, y);

          // if beat_flag != 1 and beat_flag != 0
          //    beat_flag = 0

	    
        
	    	
		
            return result;
        
    }
}
