using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        this.offset = transform.position - this.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.player.transform.position + this.offset;
    }
}
