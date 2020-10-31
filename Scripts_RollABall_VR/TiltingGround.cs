using UnityEngine;
//using UnityEngine.InputSystem;
using TMPro;

public class TiltingGround : MonoBehaviour
{
    public bool isUseOldInput;
    public TextMeshProUGUI gyroText;
    private Quaternion initialQuaternion;
    // Start is called before the first frame update
    void Start()
    {
        if (isUseOldInput)
        {
            UnityEngine.Input.gyro.enabled = true;
            initialQuaternion = UnityEngine.Input.gyro.attitude;
        }
      //  else
       // {
          //  InputSystem.EnableDevice(UnityEngine.InputSystem.AttitudeSensor.current);
          //  initialQuaternion = UnityEngine.InputSystem.AttitudeSensor.current.attitude.ReadValue();
       // }
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(-q.x, -q.z, -q.y, -q.w);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q_sensor = Quaternion.identity;
        if (isUseOldInput)
        {
            q_sensor = UnityEngine.Input.gyro.attitude;
        }
     //   else
     // {
         //q_sensor = UnityEngine.InputSystem.AttitudeSensor.current.attitude.ReadValue();

       // }
        this.transform.rotation = GyroToUnity(Quaternion.Inverse(initialQuaternion) * q_sensor);
        gyroText.text = (this.transform.rotation).eulerAngles.ToString("F6");
        
    }

    
}
