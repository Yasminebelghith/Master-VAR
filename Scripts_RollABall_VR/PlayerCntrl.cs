using UnityEngine;
//using UnityEngine.InputSystem;
using TMPro;

public class PlayerCntrl : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject WinText;
    public bool isMobileBuild;
    public TextMeshProUGUI accText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinText.SetActive(false);

     //   if (isMobileBuild)
    //    {
         //   InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
      //  }
    }

  //  private void OnMove(InputValue movementValue)
  //  {
  //      Vector2 movementVector = movementValue.Get<Vector2>();
   //     movementX = movementVector.x;
   //     movementY = movementVector.y;

   // }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
       // if (isMobileBuild)
       // {
       //  Vector3 a = UnityEngine.InputSystem.Accelerometer.current.acceleration.ReadValue();
       //  accText.text = "Accelerometer: " + a.ToString("F6");
       //  movement = new Vector3(a.x, 0.0f, a.y);
       // }
       // else
       // {
         // movement = new Vector3(movementX, 0.0f, movementY);
     //   }

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count >= 20)
        {
            WinText.SetActive(true);
        }
    }
}
