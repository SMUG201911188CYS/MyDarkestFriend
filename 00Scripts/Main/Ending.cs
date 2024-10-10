using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
/*
public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject cutScene;
    
    // 매니저 스크립트들은 해당 매니저 이름.I.메서드로 호출 가능함. public static 해당 매니저 Instance 이런식으로 선언돠어있음.

    public async void EndingScene()
    {
        if (Player.playerStat[Enums.PlayerStat.Attention] == 100f || Player.playerStat[Enums.PlayerStat.Risk] == 100f) //엔딩 1
        {
            FindObjectOfType<CutsceneHandler>().SetSprite("1"); //컷씬

            await UniTask.Delay(2000);
            LogManager.I.GenerateLog("사이렌 소리가 시끄럽게 울린다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("집을 향하는 눈이 부신 헤드라이트들, 차량이 멈춰 서는 소리.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("사람들이 웅성이는 소리와 군홧발 소리.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("우리는 그들을 사냥했고, 이제는 그들이 우리를 사냥하고자 한다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("우리는 아직 인류에게 맞서기엔 너무나도 유약하며 무방비했다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("조금만 더 시간이 있었더라면.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("우리가 조금 더 성장할 수 있었더라면…");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("이렇게 허무하게 끝을 맺지는 않았을 텐데.");
            
            await UniTask.Delay(4000);
            
            Main.I.ActiveFadeInDimed();
            Main.I.GoToIntro();
        }
        else if (Player.playerStat[Enums.PlayerStat.Acquisition] == 0) //엔딩 2
        {
            FindObjectOfType<CutsceneHandler>().SetSprite("2"); //컷씬

            await UniTask.Delay(2000);
            LogManager.I.GenerateLog("W: 블랙?");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("그림자는 날이 갈수록 작아졌다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("위협적일 정도로 커다랗던 검은 몸은 눈사람이 녹듯 점점 사라져갔다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("화이트는 어느새 자신이 블랙을 내려다보고 있음을 깨달았다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("W: 블랙, 왜 그래?");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("무언가 잘못되었다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("블랙은 지금도 사라지고 있었다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("W: 블랙, 가지 마.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("손을 뻗어 친구를 껴안는 순간, 그림자는 지금까지의 일이 모두 꿈이었던 것처럼 흩어져 사라졌다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("소녀는 지금 이 순간, 오롯이 혼자가 되었다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("자신의 편이라고는 아무도 남지 않았다.");

            await UniTask.Delay(4000);
            
            Main.I.ActiveFadeInDimed();
            Main.I.GoToIntro();
        }
        /*else if () //엔딩 3
        {
            //FindObjectOfType<CutsceneHandler>().SetSprite(); //컷씬

            await UniTask.Delay(2000);
            LogManager.I.GenerateLog("W: 배고파.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("B: 화이트?");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("W: ...블랙, 배고파.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("인간 역시 결국에는 생명체다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("삶을 유지하기 위해서는 필수적인 것들이 있다.");

            await UniTask.Delay(1000);
            LogManager.I.GenerateLog("생명은 점점 더 흐려져가고, 결국에는 꺼지고 만다.");

            await UniTask.Delay(4000);
            
            Main.I.ActiveFadeInDimed();
            Main.I.GoToIntro();
        }
    }
}
*/