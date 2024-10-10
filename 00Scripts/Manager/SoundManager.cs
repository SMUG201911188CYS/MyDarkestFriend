using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource sfx;

    [SerializeField] private AudioClip bgm_Title; // 타이틀 화면
    [SerializeField] private AudioClip bgm_Main; // 메인 화면
    [SerializeField] private AudioClip bgm_Exploration; // 탐색

    [SerializeField] private AudioClip sfx_Grrr; // 특정 대화 시 출력
    [SerializeField] private AudioClip sfx_Roar; // 포효(R)
    [SerializeField] private AudioClip sfx_Click; // 클릭 시
    [SerializeField] private AudioClip sfx_Crack1; // 조명 파괴 시 1
    [SerializeField] private AudioClip sfx_Crack2; // 조명 파괴 시 2
    [SerializeField] private AudioClip sfx_Error; // 일정 배분 오류 시
    [SerializeField] private AudioClip sfx_PlanConfirm; // 일정 배분 시 (칸에 들어갔을 시)
    [SerializeField] private AudioClip sfx_PlanSelect; // 일정 클릭 시 (칸 들어가기 전)
    [SerializeField] private AudioClip sfx_Switch; // 캐릭터 전환(Tab)
    [SerializeField] private AudioClip sfx_Text; // 텍스트 출력 시
    [SerializeField] private AudioClip sfx_WhiteWalk; // 화이트 이동 시

    [SerializeField] private AudioClip sfx_Knock; // 노크 소리
    [SerializeField] private AudioClip sfx_DoorBig_Close; // 아빠 문 닫을 때
    [SerializeField] private AudioClip sfx_DoorBig_Open; // 아빠 문 열 때
    [SerializeField] private AudioClip sfx_DoorSmall_Close; // 화이트 문 닫을 때
    [SerializeField] private AudioClip sfx_DoorSmall_Open; // 화이트 문 열 때
    [SerializeField] private AudioClip sfx_SuccessEat; // 블랙 포식 시
    [SerializeField] private AudioClip sfx_Gun; // 블랙 포식 시

    [SerializeField] private AudioClip sfx_Pick;
    [SerializeField] private AudioClip sfx_Caught; // NPC 줄에 걸릴 시
    [SerializeField] private AudioClip sfx_Lock; // 문 잠김 시
    [SerializeField] private AudioClip sfx_LockOp; // 문 잠김 해제 시
    [SerializeField] private AudioClip sfx_Buzzer; // 방범 벨 울릴 시
    [SerializeField] private AudioClip sfx_AlarmIng; // 알람 대기 시
    [SerializeField] private AudioClip sfx_AlarmDone; // 알람 울릴 시



    
    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // SFX
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    // BGM
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    // Mute and Unmute
    public void MuteSFX(bool isMute)
    {
        if (isMute)
        {
            audioMixer.SetFloat("SFX", 1);
        }
        else
        {
            audioMixer.SetFloat("SFX", 0);
        }

        audioMixer.GetFloat("SFX", out float value);
        Debug.Log($"MuteSFX : {isMute} / {value}");
    }

    public void MuteBGM(bool isMute)
    {
        if (isMute)
        {
            audioMixer.SetFloat("BGM", 1);
        }
        else
        {
            audioMixer.SetFloat("BGM", 0);
        }
    }



    // BGM
    public void PlayBGM_Title()
    {
        bgm.clip = bgm_Title;
        bgm.Play();
    }

    public void PlayBGM_Main()
    {
        bgm.clip = bgm_Main;
        bgm.Play();
    }

    public void PlayBGM_Exploration()
    {
        bgm.clip = bgm_Exploration;
        bgm.Play();
    }

    // SFX
    public void PlaySFX_Grrr()
    {
        sfx.PlayOneShot(sfx_Grrr);
    }

    public void PlaySFX_Roar()
    {
        sfx.clip = sfx_Roar;
        sfx.Play();
    }
    
    public void PlaySFX_Click()
    {
        sfx.clip = sfx_Click;
        sfx.Play();
    }
    
    public void PlaySFX_Crack()
    {
        sfx.clip = Random.Range(0, 2) == 0 ? sfx_Crack1 : sfx_Crack2;
        sfx.Play();
    }
    
    public void PlaySFX_Error()
    {
        sfx.clip = sfx_Error;
        sfx.Play();
    }
    
    public void PlaySFX_PlanConfirm()
    {
        sfx.clip = sfx_PlanConfirm;
        sfx.Play();
    }
    
    public void PlaySFX_PlanSelect()
    {
        if (sfx.clip == sfx_PlanSelect && sfx.isPlaying)
            return;
        sfx.clip = sfx_PlanSelect;
        sfx.Play();
    }
    
    public void PlaySFX_Switch()
    {
        sfx.clip = sfx_Switch;
        sfx.Play();
    }
    
    public void PlaySFX_Text()
    {
        //sfx.clip = sfx_Text;
        if (sfx.clip == sfx_Text && sfx.isPlaying)
            return;
        sfx.clip = sfx_Text;
        sfx.Play();
    }
    
    public void PlaySFX_WhiteWalk()
    {
        if (sfx.clip == sfx_WhiteWalk && sfx.isPlaying)
            return;
        
        sfx.clip = sfx_WhiteWalk;
        sfx.Play();
    }
    
    public void StopWhiteWalk()
    {
        if (sfx.clip == sfx_WhiteWalk)
        {
            sfx.Stop();
            sfx.clip = null;
        }
    }
    
    public void PlaySFX_Knock()
    {
        sfx.PlayOneShot(sfx_Knock);
    }
    
    public void PlaySFX_DoorBig_Open()
    {
        sfx.PlayOneShot(sfx_DoorBig_Open);
    }

    public void PlaySFX_DoorBig_Close()
    {
        sfx.PlayOneShot(sfx_DoorBig_Close);
    }
    
    public void PlaySFX_DoorSmall_Open()
    {
        sfx.PlayOneShot(sfx_DoorSmall_Open);
    }
    
    public void PlaySFX_DoorSmall_Close()
    {
        sfx.PlayOneShot(sfx_DoorSmall_Close);
    }
    
    public void PlaySFX_SuccessEat()
    {
        sfx.PlayOneShot(sfx_SuccessEat);
    }
    
    public void PlaySFX_Gun()
    {
        sfx.PlayOneShot(sfx_Gun);
    }
    
    public void PlaySFX_Pick()
    {
        sfx.PlayOneShot(sfx_Pick);
    }
    public void PlaySFX_Caught()
    {
        sfx.PlayOneShot(sfx_Caught);
    }
    public async void PlaySFX_Buzzer()
    {
        sfx.PlayOneShot(sfx_Buzzer);
        await UniTask.Delay(4000);
        sfx.Stop();
    }
    public void PlaySFX_Lock()
    {
        sfx.PlayOneShot(sfx_Lock);
    }
    public void PlaySFX_LockOp()
    {
        sfx.PlayOneShot(sfx_LockOp);
    }
    public void PlaySFX_AlarmIng()
    {
        sfx.PlayOneShot(sfx_AlarmIng);
    }
    public async void PlaySFX_AlarmDone()
    {
        sfx.PlayOneShot(sfx_AlarmDone);
        await UniTask.Delay(4000);
        sfx.Stop();
    }
}
