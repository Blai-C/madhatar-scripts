using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIControllerScript : MonoBehaviour
{

    // AI INPUTS
    float InputTorque;
    float InputGir;


    // Collider Rodes
    public WheelCollider FrontR;
    public WheelCollider FrontL;
    public WheelCollider RearR;
    public WheelCollider RearL;

    // Mesh rodes
    public Transform FrontRMesh;
    public Transform FrontLMesh;
    public Transform RearRMesh;
    public Transform RearLMesh;

    // Forces
    public float maxTorque = 500f;
    public float brakeTorque = 1000f;

    //Angle rotació
    public float maxRotacio = 30f;




    // Centre de gravetat
    public Vector3 centreG = new Vector3(0f, 0f, 0f);

    //eulers
    public Vector3 eulers;

    // Inputs
    private float torquePower = 0f;
    private float steerAngle = 30f;


    // AUDIO
    AudioSource audioS;
    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;
    Rigidbody rb;
    public float carCurrentSpeed;
    public float maxSpeed = 80;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centreG;
        //AUDIO
        audioS = GetComponent<AudioSource>();
        minPitch = 0.5f;
        maxPitch = 2;
        rb = GetComponent<Rigidbody>();
        carCurrentSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //rotacio
        Vector3 temp = FrontLMesh.localEulerAngles;
        Vector3 temp1 = FrontRMesh.localEulerAngles;
        temp.y = FrontL.steerAngle - (FrontLMesh.localEulerAngles.z);
        FrontLMesh.localEulerAngles = temp;
        temp1.y = FrontR.steerAngle - (FrontRMesh.localEulerAngles.z);
        FrontRMesh.localEulerAngles = temp1;
        FrontLMesh.Rotate(FrontL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        FrontRMesh.Rotate(FrontR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        RearLMesh.Rotate(RearL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        RearRMesh.Rotate(RearR.rpm / 60 * 360 * Time.deltaTime, 0, 0);

        //AUDIO
        audioS.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(0, maxSpeed, carCurrentSpeed));

   
    }
    private void FixedUpdate()
    {
        
            // Control velocitat max
            if (rb.velocity.magnitude * 3.6f < maxSpeed)
            {
                torquePower = maxTorque * Mathf.Clamp(InputTorque, -1, 1);
                RearL.brakeTorque = 0f;
                RearR.brakeTorque = 0f;
            }
            else { torquePower = 0f; }

        // Aplicar força
        RearR.motorTorque = torquePower;
        RearL.motorTorque = torquePower;

        // Aplicar gir
        steerAngle = maxRotacio * InputGir;
        FrontL.steerAngle = steerAngle;
        FrontR.steerAngle = steerAngle;


        //audio
        carCurrentSpeed = (rb.velocity.magnitude * 3.6f);

    }

    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }
    public void SetInputs(float InputTorque, float InputGir)
    {
        this.InputTorque = InputTorque;
        this.InputGir = InputGir;
    }

}
