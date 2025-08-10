using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using Unity.VisualScripting;
using Unity.PlasticSCM.Editor.WebApi;

public class UdpReceiver : MonoBehaviour
{
    private int pointer = 0;
    private UdpClient udpClient;
    private Thread receiveThread;
    private int listenPort = 8899;

    private float targetXPos = 0f; // مقدار نگاشت شده
    private float targetZPos = 0f; // مقدار نگاشت شده
    private object lockObject = new object();

    public GameObject surface;

    private int index;

    // Rigidbody rb; 
    Vector3 previousPos;
    float speed;
    bool soundPlayed = false;
    public AudioSource[] materialSound;
    public Material[] materialView;

    public int NumberOfTrials = 10; 
    public bool actionComplted = false;

    void Start()
    {
        udpClient = new UdpClient(listenPort);
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        Debug.Log("UDP Receiver started on port " + listenPort);

        StartCoroutine(ExperimentSequence()); 
    }

    void Update()
    {

        speed = (transform.position - previousPos).magnitude/Time.deltaTime;
        previousPos = transform.position;

        // if (speed > 0.4 & !soundPlayed)
        // {
        //     index = Random.Range(0, 3);
        //     materialSound[index].Play();
        //     soundPlayed = true;
        //     Invoke("ResetSound", 2);
        // }



        // Debug.LogWarning("Speed: " + speed.ToString()); 
        // اعمال تغییر به پوزیشن شیء
        lock (lockObject)
        {
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(targetXPos, currentPos.y, targetZPos);
        }
    }

    IEnumerator ExperimentSequence()
    {

        Debug.LogWarning("Started Experiment");
        // Set the parameters of the experiment for this particular participant/condition 
        int CurrentCondition = 2; // This you have to randomly compute *** 


        PlayerPrefs.SetInt("CurrentCondition", CurrentCondition);


        // Run through the conditions of the experiment (e.g. audio-only, visual-only, tactile-only audio-visual, audio-visual-tactile) <-randomised order of blocks of trial 

        // Run through the trials of the experiment (e.g. trial 0 to NumberOfTrials)     
        for (int i = 0; i < NumberOfTrials; i++)
        {
            Debug.LogWarning("Started Experiment Loop");

            // Start of the trial (reset positions and timer etc)
            float startTime = Time.time;



            for (i = 0; i < 7; i++)
            {
                for (i = 0; i < 32; i++)
                {

                    // Wait until user acts on instruction (1)
                    while (!actionComplted)
                    {
                        if (speed > 0.4 & !soundPlayed)
                        {
                            Debug.LogWarning("material changed");
                            index = Random.Range(0,4); // This needs to be chosen/set once outside the loop ***
                            materialSound[index].Play();
                            Renderer renderer = surface.GetComponent<Renderer>();
                            renderer.material = materialView[index];
                            soundPlayed = true;
                            Invoke("ResetSound", 2.5f);
                        }
                        yield return null;
                    }

                    yield return new WaitForSeconds(0.1f);
                    actionComplted = false;

                }
            }

            // Wait until user acts on instruciton (2) e.g. user response 


        }


        yield return null; 
    }

    void ResetSound()
    {
        soundPlayed = false; 
    }

    void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
        while (true)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data); // مثلا: "100,123.45,67.89,0.5"
                Debug.Log("Received: " + message);

                string[] parts = message.Split(',');
                if (parts.Length >= 2)
                {
                    if (float.TryParse(parts[2], out float rawX))
                    {
                        float mappedX = MapValue(rawX, 0f, 130f, 1.64f, 1.90f);

                        lock (lockObject)
                        {
                            targetXPos = mappedX;
                        }
                    }
                    if (float.TryParse(parts[1], out float rawZ))
                    {
                        float mappedZ = MapValue(rawZ, 0f, 240f, 1.50f, 1.98f);

                        lock (lockObject)
                        {
                            targetZPos = mappedZ;
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                Debug.LogError("Socket exception: " + ex.Message);
            }
        }
    }

    float MapValue(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return outMin + (value - inMin) * (outMax - outMin) / (inMax - inMin);
    }

    void OnApplicationQuit()
    {
        if (receiveThread != null)
            receiveThread.Abort();
        if (udpClient != null)
            udpClient.Close();
    }
}
