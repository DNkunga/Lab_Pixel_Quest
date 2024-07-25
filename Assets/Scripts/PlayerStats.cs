using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Audio _audio;
    private UIController uIController;


    public Transform _respawnPoint;

    public int _playerLife = 3;
    private float _maxHealth = 3;
    public int _playerCoin = 0;



    // Start is called before the first frame update
    void Start()
    {
       _rigidbody2D = GetComponent<Rigidbody2D>();
       _audio = GameObject.FindAnyObjectByType<Audio>();
       uIController = GameObject.FindAnyObjectByType<UIController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        string colTag = collision.tag;


        switch (colTag) 
        {
            case Structs.Tags.deathTag: 
                {
                    _rigidbody2D.velocity = Vector2.zero;
                    transform.position = _respawnPoint.position;
                    _playerLife--;
                    uIController.HeartImageUpdate(_playerLife / _maxHealth);
                    _audio.PlaySFX(Structs.SoundEffects.death);
                    if (_playerLife <= 0)
                    { 
                        string SceneName = SceneManager.GetActiveScene().name;
                        SceneManager.LoadScene(SceneName);
                    }
                    return;
                }
            case Structs.Tags.healthTag:
                {
                    if (_playerLife == 3) { return; }
                    _playerLife++;
                    uIController.HeartImageUpdate(_playerLife / _maxHealth);
                    Destroy(collision.gameObject);
                    _audio.PlaySFX(Structs.SoundEffects.heart);
                    return;
                }
            case Structs.Tags.coinTag:
                {
                    _playerCoin++;
                    uIController.CoinTextUpdate(_playerCoin);
                    _audio.PlaySFX(Structs.SoundEffects.coin);
                    Destroy(collision.gameObject);
                    return;
                }
            case Structs.Tags.respawnTag:
                {
                    _audio.PlaySFX(Structs.SoundEffects.checkpoint);
                    _respawnPoint = collision.gameObject.transform.Find(Structs.Tags.respawnColPoint).transform;
                    return;
                }
            case Structs.Tags.finishTag:
                {
                    string nextLevel = collision.GetComponent<EndLevel>().nextLevel;
                    SceneManager.LoadScene(nextLevel);
                    return;
                }
        }
    }
}
