public class Enums
{
    public enum SceneType
    {
        None = 0,
        Intro = 1,
        Main = 2,
        Map = 3,
        Tutorial = 4
    }
    
    public enum FadeType
    {
        None = 0,
        In = 1 << 0, // 1
        Out = 1 << 1, // 2
        InOut = 1 << 2, // 4
        OutIn = 1 << 3 // 8
    }
    
    public enum SoundToggleType
    {
        None = 0,
        SFX = 1 << 0, // 1
        BGM = 1 << 1 // 2
    }
    
    public enum PlanType
    {
        None = 0,
        Exploration = 1 << 0, // 1
        Communication = 1 << 1, // 2
        Daily = 1 << 2 // 4
    }
    
    public enum ObjectType
    {
        None = 0,
        Door,
        Item,
        TutorialEvent,
        TutorialRoomDoor,
        A,
        B1,
        B2,
        C,
        D,
        E,
        F,
        G,
    }
    public enum ObjectState
    {
        None = 0,
        Active,
        Inactive,
        Destroy
    }
    
    public enum PlayerType
    {
        None = 0,
        White,
        Black,
        WhiteToBlack,
        BlackToWhite
    }
    
    public enum MapType
    {
        None = 0,
        Bed,
        Bookshelf,
        Home,
        Yard,
        Walk,
        Explore,
        Tv,
        TvNone,
        Mart_1,
        Mart_2,
        Mart_3,
        Mart_4,
        Mart_5,
        School_1,
        School_2,
        School_3,
        School_4,
        School_5,
    }
    
    public enum WeatherType
    {
        None = 0,
        맑음,
        흐림,
        비,
        눈,
        안개,
        천둥번개,
        폭풍,
        눈보라
    }
    
    public enum InteractionType
    {
        None,
        Select,
        Destroy,
        Move
    }
    
    public enum EnemyState
    {
        None,
        Idle,
        Move,
        Following,
        Caught,
        Warning,
        Run,
        Hide,
        Dead
    }
    
    public enum MapSelectType
    {
        Base,
        Mart,
        AmusementPark,
        School,
        Hospital,
        DepartmentStore,
    }
    
    public enum PlayerStat
    {
        Humanity,
        Dependency,
        Knowledge,
        Trust,
        Nature,
        Acquisition,
        Attention,
        Risk,
        DoNotEatNpc,
        Hunger,
        SerialHunger,
        None
    }
    
    public enum ItemType
    {
        Food,
        Book,
        JumpRope,
        Alarm,
        SelfDefenceBell,
        Lock,
        Flower,
        Bouquet,
        Cabinet,
        None
    }
}
