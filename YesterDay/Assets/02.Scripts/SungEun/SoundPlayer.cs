using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private GameDataInstance SV;    // soundVolume

    [SerializeField]
    private AudioSource BG;
    [SerializeField]
    private AudioSource sound;

    private void Awake()
    {
        SV = FindObjectOfType<GameDataInstance>();
    }

    /*public void SoundPlay()
    {
        sound.volume = SV.effectSV;     // 혹시몰라서 다시 설정
        
        sound.Play();
    }*/     // 스크립트보단 바로 클립으로 접근하기

    // 이펙트도 설정해줌
    public void SetSV(float volume)
    {
        sound.volume = volume;
        SV.effectSV = volume;
    }

    // BG 전용 BG는 계속해서 변해야 하기 때문에 이렇게 해줌.
    public void SetBG(float volume)
    {
        BG.volume = volume;
        SV.BGV = volume;    // 데이터 메니져 에 볼륨도 바꿔줌
    }
}
