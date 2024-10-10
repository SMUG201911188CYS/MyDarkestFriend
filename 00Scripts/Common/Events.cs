using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Events : MonoBehaviour
{
    public async void DelaySeconds(float seconds)
    {
        // seconds 뒤에 실행
        await Task.Delay((int)(seconds * 1000));
        
        Debug.Log($"{seconds}초 뒤에 실행됩니다.");
    }
    
}
