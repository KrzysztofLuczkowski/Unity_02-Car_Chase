using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Parametry prêdkoœci
    public float baseSpeed = 12f;         // Bazowa (domyœlna) prêdkoœæ, do której auto wraca
    public float currentSpeed;            // Aktualna prêdkoœæ (publiczna, aby spawner móg³ j¹ odczytaæ)
    public float maxSpeed = 18f;          // Maksymalna prêdkoœæ
    public float minSpeed = 8f;           // Minimalna prêdkoœæ przy zwalnianiu (gracz mo¿e zwolniæ do tej wartoœci)

    public float acceleration = 5f;       // Przyspieszenie przy wciskaniu W
    public float deceleration = 5f;       // Zwalnianie przy wciskaniu S
    public float returnDeceleration = 2f; // Przyspieszenie powrotne do prêdkoœci bazowej, gdy nie wciskamy ¿adnego klawisza

    // Ruch boczny
    public float strafeSpeed = 5f;
    // Domyœlne granice trybu normalnego (np. symetryczne wzglêdem 0)
    public float defaultBoundary = 4.2f;
    public float leftBoundary;  // dolna granica (mniejsza wartoœæ)
    public float rightBoundary; // górna granica (wiêksza wartoœæ)


    // Obrót pojazdu przy skrêcie
    public float rotationSpeed = 5f;
    public float maxRotationY = 10f;

    // UI – TextMeshPro do wyœwietlania prêdkoœci
    public TextMeshProUGUI speedText;

    void Start()
    {
        currentSpeed = baseSpeed;
        leftBoundary = -defaultBoundary;
        rightBoundary = defaultBoundary;
        UpdateSpeedUI();
    }

    void Update()
    {
        // Sprawdzamy, czy gracz wciska klawisze przyspieszania lub zwalniania
        bool accelerating = Input.GetKey(KeyCode.W);
        bool decelerating = Input.GetKey(KeyCode.S);

        if (accelerating)
        {
            currentSpeed = Mathf.Min(maxSpeed, currentSpeed + acceleration * Time.deltaTime);
        }
        if (decelerating)
        {
            currentSpeed = Mathf.Max(minSpeed, currentSpeed - deceleration * Time.deltaTime);
        }
        // Gdy nie wciskamy ani W, ani S – auto stopniowo wraca do prêdkoœci bazowej
        if (!accelerating && !decelerating)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, returnDeceleration * Time.deltaTime);
        }

        // Ruch do przodu
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;

        // P³ynny ruch lewo/prawo
        float moveInput = Input.GetAxis("Horizontal");
        float moveAmount = moveInput * strafeSpeed * Time.deltaTime;
        float newX = Mathf.Clamp(transform.position.x + moveAmount, leftBoundary, rightBoundary);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Obrót pojazdu przy skrêcie (efekt przechylania)
        float targetRotationY = moveInput * maxRotationY;
        float smoothRotationY = Mathf.LerpAngle(transform.eulerAngles.y, targetRotationY, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, smoothRotationY, 0f);

        // Aktualizacja UI – prêdkoœæ (przeliczana jako currentSpeed * 10)
        UpdateSpeedUI();
    }

    private void UpdateSpeedUI()
    {
        if (speedText != null)
        {
            float displaySpeed = currentSpeed * 10f;
            speedText.text = displaySpeed.ToString("F0") + " km/h";
        }
    }
    public void SetHardcoreMode(bool hardcore)
    {
        if (hardcore)
        {
            // W trybie Hardcore granice wynosz¹: lewa: -14.2, prawa: -6
            leftBoundary = -14.2f;
            rightBoundary = -6f;
        }
        else
        {
            leftBoundary = -defaultBoundary;
            rightBoundary = defaultBoundary;
        }
    }
}
