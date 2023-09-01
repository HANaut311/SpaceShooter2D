using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float _rotateSpeed = 3f;
    [SerializeField] private GameObject _explosionPrefab;

    Spawn_Manager spawn_Manager;
    
    void Start()
    {
        spawn_Manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
    }

    // Update is called once per frame
    void Update()
    {   //forward: trục z, quay xung quanh
        transform.Rotate( Vector3.forward* _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.tag == "laser")
        {   

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collison.gameObject);

            spawn_Manager.StartSpawning();
            Destroy(this.gameObject, 0.25f);


        }



    }


}
