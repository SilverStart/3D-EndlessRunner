using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeatherType
{
    Sun, Rain, Snow
}

public class WeatherSystem : MonoBehaviour
{
    public WeatherType weatherType;

    [SerializeField] private GameObject[] weatherParticles;

    public static event Action<WeatherType> weatherChanged;

    IEnumerator Start()
    {
        while (true)
        {
            int weatherCount = weatherParticles.Length;
            int ranIndex = Random.Range(0, weatherCount); // 랜덤으로 선택된 날씨

            for (int i = 0; i < weatherCount; i++)
                weatherParticles[i].SetActive(ranIndex == i); // 선택된 날씨만 Active On 설정
            
            weatherType = (WeatherType)ranIndex;
            weatherChanged?.Invoke(weatherType);
            yield return new WaitForSeconds(10f);
        }
    }
}