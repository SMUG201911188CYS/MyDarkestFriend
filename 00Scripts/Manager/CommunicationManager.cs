using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CommunicationManager : MonoBehaviour
{
    public static CommunicationManager I;

    public List<Communication> communicationList;
    public Communication selectedCommunication;

    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(CommunicationManager)) as CommunicationManager;
        }

        communicationList = new List<Communication>();
        communicationList.Add(new Communication
        {
            growthQuarter = 1,
            weatherType = Enums.WeatherType.맑음,
            startConversation = new[] {
                "밖에 이웃 한 명이 집 근처를 돌아다니고 있다.",
                "아직 밝은 시간이지만, 주변에는 돌아다니는 사람이 없어 집으로 잘 유도한다면 블랙이 그를 잡아먹을 수 있을 것 같다.",
                "어떻게 해야 할까?"
                
            },
            answers = new List<Answer>
            {
                new Answer
                {
                    choice = "그를 집으로 초대한다.",
                    conversation = new[] { "그는 쉽게 미끼를 물었고, 블랙은 오늘도 만족스러운 식사를 마쳤다." },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                        stat = Enums.PlayerStat.Attention,
                        value = 2f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Nature,
                            value = 2f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Humanity,
                            value = -1f
                        }
                    }
                },
                new Answer
                {
                    choice = "그가 길을 떠나도록 둔다.",
                    conversation = new[] { "우리는 그를 내버려 두기로 했다." },
                    stats = new List<Stat>()
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
                    }
                }
            },
        });
        communicationList.Add(new Communication
        {
            growthQuarter = 1,
            weatherType = Enums.WeatherType.비,
            startConversation = new[]
            {
                "오늘은 비가 내리고 있다. 구름 때문에 낮인데도 상당히 어두워져, 블랙은 꽤 만족스러워 보인다",
                "W: 블랙은 비 오는 날이 좋은 거야?",
                "B: 밝음, 아니다. 좋다? 편하다."
            },
            answers = new List<Answer>
            {   
                new Answer
                {
                    choice = "비 오는 날이 좋다고 말한다.",
                    conversation = new[] 
                    {
                        "W: 나도 비 오는 날이 좋아, 블랙.", 
                        "블랙은 기쁜 것처럼 그르렁거렸다."
                    },
                    stats = new List<Stat>()
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
                    }
                },
                new Answer
                {
                    choice = "비 오는 날이 싫다고 말한다.",
                    conversation = new[]
                    {
                        "W: 나는 비 오는 날이 싫어.",
                        "블랙은 잘 이해하지 못한 것 같다."
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Humanity,
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
                            value = -1f
                        }
                    }
                }
            }
        });
        communicationList.Add(new Communication
        {
            growthQuarter = 1,
            weatherType = Enums.WeatherType.흐림,
            startConversation = new[]
            {
                "날씨가 흐려 하늘에 구름이 많이 떠 있다. 하얀 덩어리들이 제각각의 형태로 흘러간다.",
                "W: 저 구름은 토끼를 닮은 것 같아. 저쪽에 있는 건...",
                "B: 보다, 책. 곰.",
                "W: 책에 나오던 곰이랑 닮았다고? 그런 것 같아. 그럼 저기 있는 구름은 어때?"
            },
            answers = new List<Answer>
            {
                new Answer
                {
                    choice = "개",
                    conversation = new[]
                    {
                        "B: 개.",
                        "W: 블랙이 그렇게 말하니까 나도 그렇게 보여. 있지, 강아지들은 늑대가 착해진 거래."
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Humanity,
                            value = 1f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Nature,
                            value = -2f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Trust,
                            value = 3f
                        }
                    }
                },
                new Answer
                {
                    choice = "늑대",
                    conversation = new[]
                    {
                        "B: 늑대.",
                        "W: 블랙이 그렇게 말하니까 나도 그렇게 보여. 있지, 늑대는 개의 조상이래."
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Humanity,
                            value = -1f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Nature,
                            value = 2f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Trust,
                            value = 2f
                        }
                    }
                }
            }
        });
        communicationList.Add(new Communication
        {
            growthQuarter = 1,
            weatherType = Enums.WeatherType.흐림,
            startConversation = new[]
            {
                "겨울이 되자 눈이 내리기 시작했다. 세상이 새하얗게 변해가고 있다.",
                "B: 화이트, 닮았다. 눈.",
                "W: 온통 하얀 게?",
                "B: 그래."
            },
            answers = new List<Answer>
            {
                new Answer
                {
                    choice = "창밖을 바라본다.",
                    conversation = new[]
                    {
                        "블랙과 화이트는 함께 창가에 앉아 오랫동안 밖을 바라보았다.",
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Trust,
                            value = 1f
                        }
                    }
                },
                new Answer
                {
                    choice = "집 밖으로 나가본다.",
                    conversation = new[]
                    {
                        "하얀 눈을 따라 집 밖으로 나섰다. 쏟아지는 눈을 만끽하기도 전에 누군가가 지나가는 것이 보여 아쉽지만 다시 안으로 들어왔다."
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.None,
                            value = 0
                        }
                    }
                }
            }
        });
        communicationList.Add(new Communication
        {
            growthQuarter = 1,
            weatherType = Enums.WeatherType.None,
            startConversation = new[]
            { 
                "슬슬 식사 시간이다. 밥을 먹을 준비를 하고 있으니 블랙이 가까이 다가온다.",
                "B: 먹다. 음식?",
                "W: 블랙은 사람 말고 다른 것도 먹을 수 있어?",
                "블랙은 잘 모르겠다는 듯 그르렁거렸다. 일반적인 음식도 먹을 수 있는걸까?"
            },
            answers = new List<Answer>
            {
                new Answer
                {
                    choice = "블랙에게 고기를 준다.",
                    conversation = new[]
                    {
                        "블랙은 건네준 고기를 그림자 속으로 집어삼켰다. 어디로 사라진 걸까?.",
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Trust,
                            value = 2f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Nature,
                            value = 1f
                        },
                    }
                },
                new Answer
                {
                    choice = "블랙에게 채소를 준다.",
                    conversation = new[]
                    {
                        "블랙은 건네준 채소를 그림자 속으로 집어삼켰다. 어디로 사라진 걸까?"
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Trust,
                            value = 2f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Humanity,
                            value = 1f
                        },
                    }
                },
                new Answer
                {
                    choice = "블랙에게 아무것도 주지 않는다.",
                    conversation = new[]
                    {
                        "괴물에게는 괴물만의 먹이가 있는 법이다."
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Trust,
                            value = -1f
                        },
                    }
                }
            }
        });
        communicationList.Add(new Communication
        {
            growthQuarter = 1,
            weatherType = Enums.WeatherType.None,
            startConversation = new[]
            { 
                "블랙에 대해 더 알아보기로 했다.",
                "W: 블랙, 블랙이랑 같은 친구는 더 없어?",
                "B: 모른다. 처음, 화이트.",
                "W: 역시 모르는 거구나..."
            },
            answers = new List<Answer>
            {
                new Answer
                {
                    choice = "블랙이 계속 나랑만 있으면 좋을텐데.",
                    conversation = new[]
                    {
                        "블랙은 그르렁거리며 몸을 부비적거렸다.",
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Dependency,
                            value = 1f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Nature,
                            value = 1f
                        },
                    }
                },
                new Answer
                {
                    choice = "블랙도 다른 괴물 친구가 있으면 좋을텐데.",
                    conversation = new[]
                    {
                        "블랙은 잘 모르겠다는 듯 몸을 기울였다."
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Dependency,
                            value = -1f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Humanity,
                            value = 1f
                        },
                    }
                }
            }
        });
        communicationList.Add(new Communication
        {
            growthQuarter = 1,
            weatherType = Enums.WeatherType.None,
            startConversation = new[]
            { 
                "오늘따라 화이트는 기분이 좋지 않아 보인다. 슬픈 걸까? 소파 위에 앉아 몸을 웅크리고 있다.",
                "W: ...",
                "B: 조용하다, 화이트. 왜?",
                "W: 모르겠어. 기분이 안 좋아."
            },
            answers = new List<Answer>
            {
                new Answer
                {
                    choice = "화이트를 안아준다.",
                    conversation = new[]
                    {
                        "블랙은 화이트를 안아주었다.",
                        "W: 고마워, 블랙. 기분이 나아졌어."
                    },
                    stats = new List<Stat>()
                    {
                        new Stat
                        {
                            stat = Enums.PlayerStat.Humanity,
                            value = 1f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Acquisition,
                            value = 1f
                        },
                        new Stat
                        {
                            stat = Enums.PlayerStat.Trust,
                            value = 2f
                        }
                    }
                },
                new Answer
                {
                    choice = "화이트에게 장난을 친다.",
                    conversation = new[]
                    {
                        "블랙은 화이트의 머리카락 끄트머리를 잘근잘근 물며 가벼운 장난을 쳤다.",
                        "W: 블랙은 바보야~!",
                        "아까보다는 기분이 나아 보인다."
                    },
                    stats = new List<Stat>()
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
                    }
                },
                new Answer
                {
                choice = "화이트를 어둠 속에 둔다.",
                conversation = new[]
                {
                    "블랙은 그림자를 뻗어 방안을 어둠으로 뒤덮었다. 어둠 속은 편안한 곳이니, 화이트 역시 좋아할 것이다.",
                    "W: ...",
                    "화이트는 아무런 말도 하지 않았다."
                },
                stats = new List<Stat>()
                {
                    new Stat
                    {
                        stat = Enums.PlayerStat.Dependency,
                        value = -1f
                    },
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
            }
            }
        });
    }
    
    public void SelectCommunication()
    {
        //var selectedCommunicationList = communicationList.FindAll(x => x.growthQuarter == StatManager.I.growthQuarter && (x.weatherType == WeatherManager.I.weather || x.weatherType == Enums.WeatherType.None));
        var selectedCommunicationList = communicationList.FindAll(x => (x.weatherType == WeatherManager.I.weather || x.weatherType == Enums.WeatherType.None));
        
        foreach (var test in selectedCommunicationList)
        {
            Debug.Log(test.weatherType);
        }

        if (selectedCommunicationList.Count == 0)
        {
            Debug.Log("해당 분기에 맞는 대화가 없습니다.");
        }
        else
        {
            int choose = UnityEngine.Random.Range(0, selectedCommunicationList.Count);
            selectedCommunication = selectedCommunicationList[choose];
        }
    }
    
    public List<string> answerChoiceList = new List<string>();
    private int answerIndex;
    
    public async void StartConversation()
    {
        await Task.Delay(1000);
        
        var startConversation = selectedCommunication.startConversation;
        
        if (startConversation != null)
        {
            foreach (var line in startConversation)
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
            
                await Task.Delay(2000);
            }
        }
        
        var answers = selectedCommunication.answers;
        
        if(answers.Count == 3)
            LogManager.I.Button3TextChange(answers[0].choice, answers[1].choice, answers[2].choice);
        else
            LogManager.I.Button2TextChange(answers[0].choice, answers[1].choice);
        
        answerChoiceList.Clear();
        
        foreach (var answer in answers)
            answerChoiceList.Add(answer.choice);
        
    }

    public bool CheckAnswer(string answer)
    {
        if (!answerChoiceList.Contains(answer)) 
            return false;
        
        StartAnswerConversation(answer);
            
        return true;
    }

    private async void StartAnswerConversation(string answer)
    {
        var answers = selectedCommunication.answers;
        var selectedAnswer = answers.Find(x => x.choice == answer);
            
        var conversation = selectedAnswer.conversation;
            
        await Task.Delay(1000);

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

            await Task.Delay(2000);
        }
        
        // 수치 변동 //
        foreach (var stat in selectedAnswer.stats)
        {
            StatManager.I.ChangePlayerStat(stat.stat, stat.value);
        }
        // 대화 종료
            
        PlanManager.I.NextDay();
    }
}

[Serializable]
public class Communication
{
    public int growthQuarter; // 성장분기
    public Enums.WeatherType weatherType; // 날씨조건
    
    // 시작 대화
    public string[] startConversation;
    
    // 선택지
    public List<Answer> answers;
    
}

[Serializable]
public class Answer
{
    public string choice;
    public string[] conversation;
    // 스탯 변동
    public List<Stat> stats;
}

[Serializable]
public class Stat
{
    public Enums.PlayerStat stat;
    protected internal float value;
}