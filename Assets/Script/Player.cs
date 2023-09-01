using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed =5f;
    [SerializeField] private float _speedMultiplier = 2;
    [SerializeField] private Transform _laserPrefab;
    [SerializeField] private Transform _tripleShotPrefab;
    [SerializeField] private float _fireRite = 0.5f;
    private  float _canFire= -1f;
    [SerializeField] private int _life = 3;
    private Spawn_Manager _spawnManager;//tham chiếu đến 1 đối tượng
    private bool _isTripleShotActive = false;
    private bool _isSpeedActive = false;
    private bool _isShieldActive = false;

    [SerializeField] private GameObject shieldVisualized;
    private int _score;
    private int _bestScore;
    private UIManager _uiManager;

    [SerializeField] private GameObject _rightEngine;
    [SerializeField] private GameObject _leftEngine;

    [SerializeField] private AudioClip _laserSoundClip;
    private AudioSource  _audioSource;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _laserSoundClip;
        transform.position = new Vector3(0,-1,0);
        _bestScore =PlayerPrefs.GetInt("HighScore",0);
        _uiManager.CheckForBestScore(_bestScore);
        

    }

    // Update is called once per frame
    void Update()
    {

        caculatedMovement();

        //Time.time: Bắt đầu từ khi bắt đầu trò chơi, chạy với thời gian thực.
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    private void caculatedMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        
        //Mathf.Clamp: Kẹp giữa 2 giá trị
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 6f),0);

        if(transform.position.x >11.3f)
        {
            transform.position = new Vector3( -11.3f, transform.position.y, 0);
        }
        else if(transform.position.x <-11.3f)
        {
            transform.position = new Vector3(11.3f,  transform.position.y, 0);

        }


    }

    void FireLaser()
    {
            _canFire = Time.time + _fireRite;//cooldown, kiểm tra điều kiện, khi thỏa mãn sẽ thực hiện.
            if(_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

            }
            else
            {
                //Quaternion.identity: Phép quay mặc định và  99% thời gian đó là những gì sẽ sử dụng.
                Instantiate(_laserPrefab,new Vector3(transform.position.x,transform.position.y +1.05f,transform.position.z), Quaternion.identity);

            }

            _audioSource.Play();

    }

    public void Damage()
    {   

        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            shieldVisualized.SetActive(false);
            return;
        }

        _life--;

        if(_life == 2  )
        {
            _rightEngine.SetActive(true);
        }
        else if (_life ==1)
        {
            _leftEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_life);

        if(_life <1)
        {
            _spawnManager.OnPlayerDeath();//tham chiếu đối tượng
            AddBestScore();
            Destroy(this.gameObject);

        }

    }

    public void TripleShotActice()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;


    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedActiveRoutine());

    }

    IEnumerator SpeedActiveRoutine()
    {
        yield return new WaitForSeconds(5);
        _isSpeedActive = false;
        _speed /= _speedMultiplier;

    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        shieldVisualized.SetActive(true);

    }

    public void AddScore(int Points)
    {
        _score += Points;
        _uiManager.UpdateScore(_score);

    }
    
    public void AddBestScore()
    {   
        if(_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("HighScore", _bestScore);
            _uiManager.CheckForBestScore(_score);
        }

    }

    









}   


