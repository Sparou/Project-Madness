using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hints : MonoBehaviour
{
    public static Hints Instance;

    public GameObject BordHint;
    public GameObject BordWarning;
    public TMP_Text HintText;
    public TMP_Text WarningText;

    public bool IsActiveHint = false;
    public bool IsActiveWarning = false;

    public float blinkSpeed = 1f;

    private float time;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        BordHint.SetActive(false);
        BordWarning.SetActive(false);

        // TurnOnHint("Привет, у меня не все дома");
    }

    void Update()
    {
        if (IsActiveWarning)
        {
            time += Time.deltaTime * blinkSpeed;

            float alpha = Mathf.Lerp(0f, 1f, Mathf.PingPong(time, 1f));

            Color color = WarningText.color;
            color.a = alpha;
            WarningText.color = color;
        }
    }

    public void TurnOnWarning(string  warningText, float TimeWait)
    {
        if (!IsActiveWarning)
        {
            IsActiveWarning = true;
            WarningText.text = warningText;
            BordWarning.SetActive(true);
            StartCoroutine(TimeWarning(TimeWait));
        }
    }


    private IEnumerator TimeWarning(float delay)
    {
        yield return new WaitForSeconds(delay);
        BordWarning.SetActive(false);
        IsActiveWarning = false;
    }

    public void TurnOnHint(string hintText)
    {
        if (!IsActiveHint)
        {
            IsActiveHint = true;
            HintText.text = hintText;
            Time.timeScale = 0.0f;
            BordHint.SetActive(true);
            while (!Input.GetKeyUp(KeyCode.Space) )
            {
                Wait(0.1f);
            }
            Time.timeScale = 1.0f;
            BordHint.SetActive(false);
            IsActiveHint = false;
        }
    }

    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
