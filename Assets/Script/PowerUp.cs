using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{   

    [SerializeField] private float _speed = 3f;
    [SerializeField] private int _powerUpID; //0 = tripleShot, 1= speed, 2= Shield 

    [SerializeField] private AudioClip _clip;
    

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.tag == "Player")
        {
            Player player = collison.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if(player != null)
            {   
                switch(_powerUpID)
                {   
                    
                    case 0:
                        player.TripleShotActice();

                        break;
                    case 1:
                        player.SpeedActive();

                        break;
                    case 2:
                        player.ShieldActive();

                        break;

                    default:
                        Debug.Log("Default value");
                        break;

                }

            }

            Destroy(this.gameObject);


        }


    }



}
