using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Image _heartImage;
    private TextMeshProUGUI _coinText;
    private GameObject _menuPanel;
    private Slider _sfxSlider;
    private Slider _musicSlider;

    private int _coinCount = 0;
    private Audio _audio;

    // Start is called before the first frame update
    void Start()
    {
        _heartImage = GameObject.Find(Structs.UI.heartImage).GetComponent<Image>();
        _coinText = GameObject.Find(Structs.UI.coinText).GetComponent<TextMeshProUGUI>();
        _sfxSlider = GameObject.Find(Structs.UI.sfxSlider).GetComponent<Slider>();
        _musicSlider = GameObject.Find(Structs.UI.musicSlider).GetComponent<Slider>();
        _menuPanel = GameObject.Find(Structs.UI.panel);
        _coinCount = GameObject.Find(Structs.UI.coins).transform.childCount;

        _audio = GameObject.FindAnyObjectByType<Audio>();

        _menuPanel.SetActive(false);
        HeartImageUpdate(1);
        CoinTextUpdate(0);

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            if (_menuPanel.active)
            {
                _menuPanel.SetActive(false);
            }
            else
            {
                _menuPanel.SetActive(true);
            }
        }
    }

    public void BackButton()
    {
        _menuPanel.SetActive(false);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(Structs.Scenes.menu);
    }

    public void HeartImageUpdate(float newAmount)
    {
        _heartImage.fillAmount = newAmount;
    }

    public void CoinTextUpdate(int newAmount)
    {
        _coinText.text = newAmount + " / " + _coinCount;
    }

    public void UpdateSFXSlider() 
    {
        _audio.UpdateSFXGroup(_sfxSlider.value);
    }

    public void UpdateMusicSlider()
    {
        _audio.UpdateMusicGroup(_musicSlider.value);
    }
}
