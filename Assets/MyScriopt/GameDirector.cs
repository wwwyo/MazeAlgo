using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace MyScript
{
    public class GameDirector : MonoBehaviour
    {
        GameObject mainCamera;
        GameObject subCamera;
        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main.gameObject;
            subCamera = GameObject.Find("SubCamera");
            subCamera.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            Destroy(other.GetComponent<PlayerBfs>());
            mainCamera.SetActive(false);
            subCamera.SetActive(true);
            transform.Find("GoalParticle").GetComponent<ParticleSystem>().Play();
        }

    }
}

