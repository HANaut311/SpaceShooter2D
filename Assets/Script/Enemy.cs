using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] SpriteRenderer sr2;
    private Animator _anim;
    private Player _player;

    private AudioSource  _audioSource;

    // [SerializeField] private GameObject _laserPrefab;
    // [SerializeField] private float _fireRite = 3f;
    // private float _canFire = -1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _anim = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        caculatedMovement();
        // if(Time.time > _canFire)
        // {
        //     _fireRite = Random.Range(3.0f, 7.0f);
        //     _canFire = Time.time + _fireRite;

        //     // GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        //     // Laser[] lasers =  enemyLaser.GetComponentsInChildren<Laser>();

        //     // for(int i = 0; i < lasers.Length; i++)
        //     // {
        //     //     lasers[i].AssignEnemyLaser();



        //     // }

        // }


    }

    private void caculatedMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <-6f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX,9f, transform.position.z);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {   

            Player player = collision.transform.GetComponent<Player>();
            
            if(player != null)
            {

                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");

            _speed =0;
            _audioSource.Play();
            sr.enabled = false;
            sr2.enabled = false;
            Destroy(this.gameObject, 2.3f);
        }
        else if(collision.tag=="laser")
        {   
            Destroy(collision.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);

            }

            _anim.SetTrigger("OnEnemyDeath");

            _speed =0;
            _audioSource.Play();
            Destroy(GetComponent<BoxCollider2D>());
            sr.enabled = false;
            sr2.enabled = false;
            Destroy(this.gameObject,2.3f);
        }

    }




}

