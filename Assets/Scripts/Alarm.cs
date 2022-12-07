using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarm;

    private float _startVolume = 0;
    private float _maxVolume = 1;
    private float _duration = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        _alarm.PlayOneShot(_alarm.clip);
        StartIncreaseSound();     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartTurnDownSound();
    }

    private void StartTurnDownSound()
    {
        StartCoroutine(TurnDownSound());
    }

    private IEnumerator TurnDownSound()
    {
        float numberOfChanges = FindNumberOfChanges(_duration);

        for(int i = 0; i < numberOfChanges; i++) 
        {
            _alarm.volume = _maxVolume - (_maxVolume / numberOfChanges * i);

            yield return null;
        }

        _alarm.Stop();
    }

    private void StartIncreaseSound()
    {
        StartCoroutine(IncreaseSound());
    }

    private IEnumerator IncreaseSound()
    {
        float numberOfChanges = FindNumberOfChanges(_duration);

        for(int i = 0; i < numberOfChanges; i++) 
        {
            _alarm.volume = _startVolume + (_maxVolume / numberOfChanges * i);

            yield return null;
        }
    }

    private float FindNumberOfChanges(float Duration) 
    {
        float numberOfChanges = Duration / Time.deltaTime;
        return numberOfChanges;
    }
}
