using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Lamp _lamp;
    [SerializeField] private AudioClip _openingDoorAudio;
    [SerializeField] private AudioClip _closingDoorAudio;

    private readonly float _closedPosition = -.1f;
    private readonly float _openedPosition = 2.7f;
    private readonly Vector3 _moveDirection = Vector3.up;

    private Coroutine _moveJob;
    private AudioSource _audioSource;
    private Vector3 _startPosition;

    private void Start() 
    {
        _startPosition = transform.position;
        _audioSource = GetComponent<AudioSource>();
    }

    public void Toogle(bool isOpened)
    {
        if(isOpened)
            Open();
        else
            Close();
    }

    private void Open() 
    {
        StartJob(_openedPosition);
        PlayAudioClip(_openingDoorAudio);
        _lamp?.SetState(Lamp.State.Awaiting);
    }

    private void Close()
    {
        StartJob(_closedPosition);
        PlayAudioClip(_closingDoorAudio);
        _lamp?.SetState(Lamp.State.Releasing);
    }

    private void StartJob(float targetPosition)
    {
        if(_moveJob != null)
            StopCoroutine(_moveJob);

        _moveJob = StartCoroutine(ChangingPositionCoroutine(targetPosition));
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        _audioSource?.Stop();
        _audioSource?.PlayOneShot(audioClip);
    }    

    private IEnumerator ChangingPositionCoroutine(float _destinationPosition)
    {
        Vector3 destenation = _startPosition + _moveDirection * _destinationPosition;

        while(transform.position != destenation)
        {
            transform.position = Vector3.MoveTowards(transform.position, destenation, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
