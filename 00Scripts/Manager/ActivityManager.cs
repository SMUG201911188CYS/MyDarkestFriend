using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActivityManager : MonoBehaviour
{
    public static ActivityManager I;

    public List<Activity> activityList;

    public Animator cutSceneAnimator;

    private ActivityChoice selectActivityChoice;
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(ActivityManager)) as ActivityManager;
        }

        cutSceneAnimator.enabled = false;
        
        activityList = new List<Activity>
        {
            new()
            {
                name = "공놀이", conversation = new [] { "공놀이를 하기로 하고 밖으로 나왔다.", "공을 어떻게 던져볼까?" },
                choices = new List<ActivityChoice>
                {
                    new()
                    {
                        choice = "세게 던진다.",
                        successRate = 40,
                        successLog = "블랙은 세게 던진 공을 쉽게 잡아왔다. 즐거운 시간이었던 것 같다.",
                        failLog = "화이트와 공놀이를 하기에는 너무 세게 던졌던 것 같다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Acquisition,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Dependency,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Acquisition,
                                value = -1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -1f
                            }
                        }
                    },
                    new()
                    {
                        choice = "살살 던진다.",
                        successRate = 60,
                        successLog = "공을 여러 차례 주고받았다. 즐거운 시간이었던 것 같다.",
                        failLog = "블랙에게는 공을 너무 살살 던졌던 것 같다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Acquisition,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Dependency,
                                value = -1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Dependency,
                                value = -1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -1f
                            }
                        }
                    },
                }
            },
            new()
            {
                name = "낙서", conversation = new [] { "종이에 낙서를 하기로 했다.", "무엇을 그려볼까?" },
                choices = new List<ActivityChoice>
                {
                    new()
                    {
                        choice = "거칠게",
                        successRate = 40,
                        successLog = "인상적인 그림을 완성한 것 같다.",
                        failLog = "그림을 너무 거칠게 그린 듯하다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = -2f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Nature,
                                value = 2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 2f
                            }
                        }
                    },
                    new()
                    {
                        choice = "기분 따라",
                        successRate = 50,
                        successLog = "마음을 담은 그림을 완성한 것 같다.",
                        failLog = "너무 멋대로 그린 듯하다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Nature,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -2f
                            }
                        }
                    },
                    new()
                    {
                        choice = "열심히",
                        successRate = 60,
                        successLog = "노력한 만큼의 결과를 완성해냈다.",
                        failLog = "노력이 언제나 성공을 보장해주지는 않는 것 같다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = 2f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Nature,
                                value = -2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -1f
                            }
                        }
                    },
                    
                    
                }
            },
            new()
            {
                name = "낮잠", conversation = new [] { "함께 낮잠을 자기로 했다." },
                choices = new List<ActivityChoice>
                {
                    new()
                    {
                        choice = "",
                        successRate = 100,
                        successLog = "한숨 자고 일어나니 몸이 가볍다.",
                        failLog = "제대로 쉬지는 못한 것 같다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Nature,
                                value = -1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 1f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Nature,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -1f
                            }
                        }
                    }
                }
            },
            new()
            {
                name = "산책", conversation = new [] { "인적이 드문 골목을 찾아 산책을 나왔다.", "어떻게 걸어볼까?" },
                choices = new List<ActivityChoice>
                {
                    new()
                    {
                        choice = "천천히 걷는다.",
                        successRate = 40,
                        successLog = "여유로운 시간을 보내고 돌아왔다.",
                        failLog = "너무 여유를 부리다 하마터면 들킬 뻔 했다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Acquisition,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Dependency,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 1f
                            }
                        }
                    },
                    new()
                    {
                        choice = "빨리 걷는다.",
                        successRate = 60,
                        successLog = "적당히 운동이 된 것 같다.",
                        failLog = "화이트가 쫓아오지 못한 것 같다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Acquisition,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Dependency,
                                value = -1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 1f 
                            }
                        }
                    }
                }
            },
            new()
            {
                name = "영화", conversation = new [] { "거실에 앉아 TV를 켰다.", "영화를 골라볼까?" },
                choices = new List<ActivityChoice>
                {
                    new()
                    {
                        choice = "로맨스",
                        successRate = 50,
                        successLog = "낭만적인 이야기였다.",
                        failLog = "알기 어려운 이야기다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = 3f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Nature,
                                value = -1f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Nature,
                                value = 1f
                            },
                            new Stat
                            {
                            stat = Enums.PlayerStat.Trust,
                            value = -1f
                        }
                        }
                    },
                    new()
                    {
                        choice = "액션",
                        successRate = 60,
                        successLog = "화려한 액션이 재미있는 영화였다.",
                        failLog = "화려하기만 한 B급 영화였다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = 2f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Dependency,
                                value = 1f 
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -2f 
                            }
                        }
                    },
                    new()
                    {
                        choice = "공포",
                        successRate = 70,
                        successLog = "블랙의 취향에 맞는 영화였던 듯하다.",
                        failLog = "화이트는 악몽을 꿀 것 같다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = -1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Nature,
                                value = 3f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Humanity,
                                value = 1f 
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -2f 
                            }
                        }
                    }
                }
            },
            new()
            {
                name = "게임", conversation = new [] { "비디오 게임을 연결했다.", "어떤 플레이를 해볼까?" },
                choices = new List<ActivityChoice>
                {
                    new()
                    {
                        choice = "이지 모드",
                        successRate = 70,
                        successLog = "적당히 재미있는 게임이었다.",
                        failLog = "너무 쉬워서 별로 재미가 없었다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Acquisition,
                                value = 2f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Dependency,
                                value = 1f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 2f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -2f
                            }
                        }
                    },
                    new()
                    {
                        choice = "하드 모드",
                        successRate = 30,
                        successLog = "어려웠음에도 즐겁게 엔딩을 봤다.",
                        failLog = "아직 이 난이도는 무리였던 것 같다.",
                        successStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Acquisition,
                                value = 3f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Dependency,
                                value = -2f
                            },
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = 3f
                            }
                        },
                        failStats = new List<Stat>()
                        {
                            new Stat
                            {
                                stat = Enums.PlayerStat.Trust,
                                value = -2f 
                            }
                        }
                    }
                }
            }
        };
    }

    public async void StartActivity(string activityName)
    {
        LogManager.I.Button3Inactive();
        LogManager.I.Button2Inactive();

        var activity = activityList.Find(x => x.name == activityName);
        var conversation = activity.conversation;
        
        await Task.Delay(1000);

        if (conversation != null)
        {
            // 대화 시작 (1초마다 한 줄씩 출력)
            foreach (var line in conversation)
            {
                var speaker = line.Split(':')[0];
            
                if (speaker == "W")
                {
                    LogManager.I.portraitHandler.ChangePortrait(Enums.PlayerType.White);
                }
                else if (speaker == "B")
                {
                    LogManager.I.portraitHandler.ChangePortrait(Enums.PlayerType.Black);
                }
            
                LogManager.I.GenerateLog(line);
            
                await Task.Delay(1000);
            }
        }
        
        // 선택지 출력
        if (activity.choices.Count == 1)
        {
            bool isSuccess = true;
            isSuccess = Random.Range(0, 100) < activity.choices[0].successRate;
            
            LogManager.I.GenerateLog(isSuccess ? activity.choices[0].successLog : activity.choices[0].failLog);
            
            // 수치 변동 //
            Debug.LogWarning("수치 변동");

            if (isSuccess)
            {
                foreach (var stat in activity.choices[0].successStats)
                {
                    StatManager.I.ChangePlayerStat(stat.stat, stat.value);
                }
            }
            else
            {
                foreach (var stat in activity.choices[0].failStats)
                {
                    StatManager.I.ChangePlayerStat(stat.stat, stat.value);
                }
            }
        
            // 대화 종료
        
            await Task.Delay(2000);
            FindObjectOfType<CutsceneHandler>().Hide();
            
            PlanManager.I.NextDay();
        }
        else if (activity.choices.Count == 2)
        {
            LogManager.I.Button2TextChange(activity.choices[0].choice, activity.choices[1].choice);
        }
        else if (activity.choices.Count == 3)
        {
            LogManager.I.Button3TextChange(activity.choices[0].choice, activity.choices[1].choice, activity.choices[2].choice);
        }
    }

    public async void CheckChoice(string choice)
    {
        LogManager.I.Button2Inactive();
        LogManager.I.Button3Inactive();

        bool isSuccess = true;
        
        if (choice == "세게 던진다.")
        {
            Debug.Log("세게 던진다.");
            FindObjectOfType<CutsceneHandler>().PlayAnim(choice);
            var ballActivity = activityList.Find(x => x.name == "공놀이");
            selectActivityChoice = ballActivity.choices[0];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;
            
            await Task.Delay(1000);
            
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "살살 던진다.")
        {
            Debug.Log("살살 던진다.");
            FindObjectOfType<CutsceneHandler>().PlayAnim(choice);
            
            var ballActivity = activityList.Find(x => x.name == "공놀이");
            selectActivityChoice = ballActivity.choices[1];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "거칠게")
        {
            Debug.LogWarning("거칠게");
            
            
            var ballActivity = activityList.Find(x => x.name == "낙서");
            selectActivityChoice = ballActivity.choices[0];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            //성공, 실패에 따른 컷씬 변화
            FindObjectOfType<CutsceneHandler>().SetSprite(isSuccess ? "거칠게_성공" : "거칠게_실패");
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "기분 따라")
        {
            Debug.LogWarning("기분 따라");
            
            var ballActivity = activityList.Find(x => x.name == "낙서");
            selectActivityChoice = ballActivity.choices[1];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            //성공, 실패에 따른 컷씬 변화
            FindObjectOfType<CutsceneHandler>().SetSprite(isSuccess ? "기분 따라_성공" : "기분 따라_실패");
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "열심히")
        {
            Debug.LogWarning("열심히");
            
            var ballActivity = activityList.Find(x => x.name == "낙서");
            selectActivityChoice = ballActivity.choices[2];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            //성공, 실패에 따른 컷씬 변화
            FindObjectOfType<CutsceneHandler>().SetSprite(isSuccess ? "열심히_성공" : "열심히_실패");
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "천천히 걷는다.")
        {
            Debug.Log("천천히 걷는다.");
            FindObjectOfType<CutsceneHandler>().PlayAnim(choice);
            var ballActivity = activityList.Find(x => x.name == "산책");
            selectActivityChoice = ballActivity.choices[0];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;
            
            await Task.Delay(1000);
            
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "빨리 걷는다.")
        {
            Debug.Log("빨리 걷는다.");
            FindObjectOfType<CutsceneHandler>().PlayAnim(choice);
            var ballActivity = activityList.Find(x => x.name == "산책");
            selectActivityChoice = ballActivity.choices[1];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;
            
            await Task.Delay(1000);
            
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "로맨스")
        {
            Debug.LogWarning("로맨스");
            
            var ballActivity = activityList.Find(x => x.name == "영화");
            selectActivityChoice = ballActivity.choices[0];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "액션")
        {
            Debug.LogWarning("액션");
            
            var ballActivity = activityList.Find(x => x.name == "영화");
            selectActivityChoice = ballActivity.choices[1];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "공포")
        {
            Debug.LogWarning("공포");
            
            var ballActivity = activityList.Find(x => x.name == "영화");
            selectActivityChoice = ballActivity.choices[2];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "이지 모드")
        {
            Debug.LogWarning("이지 모드");
            
            var ballActivity = activityList.Find(x => x.name == "게임");
            selectActivityChoice = ballActivity.choices[0];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }
        else if (choice == "하드 모드")
        {
            Debug.LogWarning("하드 모드");
            
            var ballActivity = activityList.Find(x => x.name == "게임");
            selectActivityChoice = ballActivity.choices[1];
            isSuccess = Random.Range(0, 100) < selectActivityChoice.successRate;

            await Task.Delay(1000);
            LogManager.I.GenerateLog(isSuccess ? selectActivityChoice.successLog : selectActivityChoice.failLog);
        }


        // 수치 변동 //
        Debug.LogWarning("수치 변동");

        if (isSuccess)
        {
            foreach (var stat in selectActivityChoice.successStats)
            {
                StatManager.I.ChangePlayerStat(stat.stat, stat.value);
            }
        }
        else
        {
            foreach (var stat in selectActivityChoice.failStats)
            {
                StatManager.I.ChangePlayerStat(stat.stat, stat.value);
            }
        }
        await Task.Delay(3000);
        
        FindObjectOfType<CutsceneHandler>().Hide();
        // 대화 종료
        PlanManager.I.NextDay();
    }
}

[Serializable]
public class Activity
{
    public string name;
    public string[] conversation;
    public List<ActivityChoice> choices;
}

[Serializable]
public class ActivityChoice
{
    public string choice;
    public int successRate;
    public string successLog;
    public string failLog;
    public List<Stat> successStats;
    public List<Stat> failStats;
}