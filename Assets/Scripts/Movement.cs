using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rBody;
    // Movement code
    //test comment
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationForce;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem boostEngine;

    AudioSource engineAudio;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        engineAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

     void StopThrusting()
    {
        engineAudio.Stop();
        boostEngine.Stop();
    }

    void ProcessRotation()
    {
        StartRotating();
    }

    void StartThrusting()
    {
        rBody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!engineAudio.isPlaying) // Checking if the SFX is playing and playing it when pressing space
        {
            engineAudio.PlayOneShot(mainEngine);
        }
        if (!boostEngine.isPlaying)
        {
            boostEngine.Play();
        }
    }

    void StartRotating()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Rotate(-rotationForce); // rotate to the right
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Rotate(rotationForce);  // rotate to the left
        }
    }

    private void Rotate(float rotationThisFrame)
    {
        rBody.freezeRotation = true; //freeze rotations so we can manually move
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rBody.freezeRotation = false;
    }
}
