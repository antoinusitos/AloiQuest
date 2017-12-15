using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : BaseManager
{
    public PlayerInventory inventory;
    public PlayerMovement playerMovement;

    public enum PlayerState
    {
        None,
        Alive,
        Dead,
        Unconscious,
        Stuck,
        //...
    }

    private PlayerState _currentPlayerState = PlayerState.None;

    private int _currentLife = 0;
    private int _maxLife = 100;

    private float _timeToRespawn = 2f;
    private float _currentTimeToRespawn = 0f;

    public Slider lifeBar;

    private void Start()
    {
        _currentLife = _maxLife;
        _currentPlayerState = PlayerState.Alive;
    }

    private void Update()
    {
        if(lifeBar != null)
            lifeBar.value = (float)_currentLife / (float)_maxLife;
        if (_currentPlayerState == PlayerState.Dead)
        {
            _currentTimeToRespawn += Time.deltaTime;
            if(_currentTimeToRespawn >= _timeToRespawn)
            {
                _currentTimeToRespawn = 0;
                _currentPlayerState = PlayerState.Alive;
                ChangeLife(_maxLife);
                playerMovement.SetCanMove(true);
            }
        }
    }

    public void ChangeLife(int Amount)
    {
        _currentLife += Amount;
        _currentLife = Mathf.Clamp(_currentLife, 0, _maxLife);
        if(_currentLife == 0)
        {
            playerMovement.SetCanMove(false);
            _currentPlayerState = PlayerState.Dead;
        }
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static PlayerManager _instance;

    public static PlayerManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
