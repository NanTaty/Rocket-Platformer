using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 2f;
    [SerializeField] AudioClip hitEffect;
    [SerializeField] AudioClip winEffect;
    
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource rocketAudio;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Start()
    {
        rocketAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        DebugKeys();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning || collisionDisable) { return; }

        switch(collision.gameObject.tag)
        {
            case "StartPoint":
                Debug.Log("You started");
                break;
            case "Finish":
                Debug.Log("You've made it until end!");
                StartNextLevelSequence();
                break;
            default:
                Debug.Log("You've hit the obstacle");
                StartCrashSequence();
                break;
        }

    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ReloadLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(hitEffect);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", levelDelay);
    }
    void StartNextLevelSequence()
    {
        isTransitioning = true;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(winEffect);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelDelay);
    }
    void ReloadLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = sceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
