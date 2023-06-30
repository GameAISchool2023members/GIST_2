using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text livesText;
    public Text coinsText;
    public Slider decibelSlider;
    public RawImage left;
    public RawImage right;
    public RawImage scream;

    private Transform player;

    private void Awake()
    {
        if (Instance != null)
        {
             DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = GameManager.Instance.lives.ToString();
        coinsText.text = GameManager.Instance.coins.ToString();
    }

    public void UpdateDecibelUI(float value, float min, float max)
    {
        decibelSlider.value = Mathf.Clamp((value - min) / (max - min), 0f, 1f);
        scream.enabled = decibelSlider.value > 0.2;
    }
}
