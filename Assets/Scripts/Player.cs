using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player speed
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private Vector3 _laserOffset = new Vector3(0, 1.05f, 0);
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private KeyCode _fireKey = KeyCode.Space;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    private float _nextFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2.5f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(_fireKey) && Time.time > _nextFire)
        {
            fireLaser();
        }
    }

    void CalculateMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        transform.Translate(_speed * direction * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x <= -11.3f || transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
    }
    
    void fireLaser()
    {
        _nextFire = Time.time + _fireRate;
        GameObject laserShot = Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
        //laserShot.transform.SetParent(_laserContainer.transform);
    }

    public void Damage(int val)
    {
        _lives -= val;

        if(_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public int getLives()
    {
        return _lives;
    }

}
