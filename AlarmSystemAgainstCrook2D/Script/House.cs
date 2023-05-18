using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour
{
    [SerializeField] private UnityEvent _alarm; //�������
    [SerializeField] private float _increaseTimeVolume;  //����� ����������

    private AudioSource _audioSource;
    private float _pathRunningTime;  //����� ����������� ����
    private bool _isReached;  //�����������?

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void Update()
    {
        if(_isReached == true)
        {
            StartAlarm();
        }
        else
        {
            StopSound();
        }
    }

    private void StartAlarm()
    {
        _pathRunningTime += Time.deltaTime;
        _audioSource.volume = _pathRunningTime / _increaseTimeVolume;
    }

    private void StopSound()
    {
        _pathRunningTime -= Time.deltaTime;
        _audioSource.volume = _pathRunningTime / _increaseTimeVolume;
    }

    public event UnityAction Reached
    {
        add => _alarm.AddListener(value);
        remove => _alarm.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isReached = true;
            _alarm?.Invoke();
            Debug.Log("������ ���������. ���-�� ������ � ���!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         _isReached = false;
        Debug.Log("� ���� ��� �����������!");
    }
}
