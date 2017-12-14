using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform playerToFollow;
    public bool turnSmooth = false;
    public float smoothSpeed = 2.0f;

    private Transform _transform;

    private float _fireRate = 1.0f;
    private float _currentReloadTime = 0.0f;
    private bool _canShoot = false;
    private int _damage = 10;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(!_canShoot)
        {
            _currentReloadTime += Time.deltaTime;
            if(_currentReloadTime >= _fireRate)
            {
                _currentReloadTime = 0;
                _canShoot = true; 
            }
        }

        if (!turnSmooth)
            _transform.LookAt(playerToFollow);
        else
        {
            Quaternion rot = Quaternion.LookRotation(playerToFollow.transform.position - _transform.position);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, rot, Time.deltaTime * smoothSpeed);
        }

        if (_canShoot)
        {
            RaycastHit hit;
            if(Physics.Raycast(_transform.position, _transform.forward, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    PlayerManager.GetInstance().ChangeLife(-_damage);
                }
            }
            _canShoot = false;
        }
    }

}
