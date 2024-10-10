using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager I;

    public List<Book> bookList;
    public List<Book> selectedBookList;
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(BookManager)) as BookManager;
        }
        
        bookList = new List<Book>();
        bookList.Add(new Book
        {
            growthQuarter = 1, type = "동화", title = "신데렐라", canRead = true,
            description = new []
            {
                "어릴 때부터 가족들에게 구박당한 신데렐라의 이야기이다.",
                "어느 날 찾아온 마법같은 기회로 유리구두를 신고 왕자와 행복한 결혼을 하는 신데렐라의 이야기가 적혀 있다."
                
            },
            conversation = new []
            {
                "W: 블랙은 요정일까 왕자님일까?"
            },
            stat = new Stat()
            {
                stat = Enums.PlayerStat.Humanity,
                value = 1f
            }
        });
        bookList.Add(new Book
        {
            growthQuarter = 1, type = "동화", title = "성냥팔이 소녀", canRead = true,
            description = new []
            {
                "사람들에게 성냥을 팔며 홀로 외롭게 살아가는 소녀의 이야기이다.",
                "성냥을 그을 때마다 보여지는 따뜻한 환영에 매료된 소녀는 그 안에서 행복한 죽음을 맞이했다."
                
            },
            conversation = new []
            {
                "W: 아무도 소녀를 도와주지 않았어."
            },
            stat = new Stat()
            {
                stat = Enums.PlayerStat.Dependency,
                value = 1f
            }
        });
        bookList.Add(new Book
        {
            growthQuarter = 1, type = "동화", title = "미운 오리 새끼", canRead = true,
            description = new []
            {
                "다르게 생겼다는 이유로 가족들에게 소외당한 아기 백조의 이야기이다.",
                "마지막엔 가장 아름다운 모습이 되어 날아올라 진짜 자신의 무리가 있는 곳으로 떠난다."
                
            },
            conversation = new []
            {
                "W: 블랙도 다른 사람들이랑은 다르게 생겼으니까 조심해야겠지."
            },
            stat = new Stat()
            {
                stat = Enums.PlayerStat.Dependency,
                value = 1f
            }
        });
        bookList.Add(new Book
        {
            growthQuarter = 1, type = "동화", title = "어린왕자", canRead = false,
            description = new []
            {
                "장미를 이해하기 위해 여행하는 어린 왕자를 만난 조종사의 이야기이다.",
                "수많은 사람들을 만나본 어린 왕자는 결국 자신이 사랑한 장미는 하나뿐임을 알고 자신의 별로 돌아간다."
                
            },
            conversation = new []
            {
                "B: 장미, 소중한, 하나.",
                "W: 블랙은 떠나지 않을 거야?"
            },
            stat = new Stat()
            {
                stat = Enums.PlayerStat.Trust,
                value = 1f
            }
        });
        bookList.Add(new Book
        {
            growthQuarter = 1, type = "동화", title = "눈의 여왕", canRead = false,
            description = new []
            {
                "마녀로부터 소년을 구하기 위해 나선 소녀의 이야기이다.",
                "얼어붙은 마음을 가지게 된 소년은 소녀의 따뜻한 눈물로 마음을 녹인다."
                
            },
            conversation = new []
            {
                "W: 소중한 친구를 구하기 위해서라면 저런 용기를 낼 수 있는 거구나."
            },
            stat = new Stat()
            {
                stat = Enums.PlayerStat.Humanity,
                value = 1f
            }
        });
        bookList.Add(new Book
        {
            growthQuarter = 1, type = "과학", title = "곤충 체험 백과", canRead = true,
            description = new []
            {
                "알에서 태어나 애벌레, 번데기를 거쳐 아름다운 날개를 펼치는 나비의 생애 삽화가 그려져 있다."

            },
            conversation = new []
            {
                "W: 인간은 언제 나비가 되는거야?"
            },
            stat = new Stat()
            {
                stat = Enums.PlayerStat.Knowledge,
                value = 1f
            }
        });
        bookList.Add(new Book
        {
            growthQuarter = 1, type = "과학", title = "우주와 외계인", canRead = true,
            description = new []
            {
                "지구 외의 행성에 생명체가 살고 있을 가능성에 대해 설명하고 있다."
            },
            conversation = new []
            {
                "W: 블랙도 지구 밖에서 온 거야? 외계인이랑 블랙은 달라?",
                "B: 모른다, 이곳, 처음.",
                "W: 그럼 다른 블랙은 없어?",
                "B: 만나다, 아니다. 없다.",
                "W: 블랙도 혼자구나."
            },
            stat = new Stat()
            {
                stat = Enums.PlayerStat.Knowledge,
                value = 1f
            }
        });
    }

    public void SelectBooks()
    {
        //selectedBookList = bookList.FindAll(x => x.growthQuarter == StatManager.I.growthQuarter && x.canRead);
        selectedBookList = bookList.FindAll(x => x.canRead);

        if (selectedBookList.Count == 0)
        {
            Debug.Log("해당 분기에 맞는 책이 없습니다.");
        }
        else if (selectedBookList.Count > 3)
        {
            for (var i = 0; i < selectedBookList.Count; i++)
            {
                var temp = selectedBookList[i];
                var randomIndex = UnityEngine.Random.Range(i, selectedBookList.Count);
                selectedBookList[i] = selectedBookList[randomIndex];
                selectedBookList[randomIndex] = temp;
            }
            
            selectedBookList = selectedBookList.GetRange(0, 3);
        }
    }

    public async void StartConversation( int index = 0, string[] conversation = null)
    {
        await Task.Delay(2000);

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
            
                await Task.Delay(4000);
            }
            
            StatManager.I.ChangePlayerStat(selectedBookList[index].stat.stat, selectedBookList[index].stat.value);
        }
        
        // 수치 변동 //
        Debug.LogWarning("수치 변동");
        StatManager.I.ChangePlayerStat(Enums.PlayerStat.Knowledge, 0.5f);
        
        FindObjectOfType<CutsceneHandler>().Hide();

        selectedBookList.Clear();
        
        
        // 대화 종료
        
        PlanManager.I.NextDay();
    }

    public void GetBook(string title)
    {
        bookList.Find(x => x.title == title).canRead = true;
    }
}


[Serializable]
public class Book
{
    public int growthQuarter; // 성장분기
    public string type; // 종류
    public string title; // 제목
    public string[] description; // 설명문
    public bool canRead; // 습득
    public Stat stat;
    // 대화
    public string[] conversation; 
}