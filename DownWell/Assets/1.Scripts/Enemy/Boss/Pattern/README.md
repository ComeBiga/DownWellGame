# BossBrain

![bbE](https://user-images.githubusercontent.com/36800639/153003009-c66720bd-7474-434e-bc63-6229d4223396.PNG)

보스의 공격패턴을 구현하기 위한 코드입니다.

추가되는 보스의 공격 패턴에 상관없이, 보스의 기본 시스템을 유지할 수 있는 구조로 디자인했습니다.
## 구조
![BossBrain](https://user-images.githubusercontent.com/36800639/152999455-ad7fea9d-a29c-4f89-9572-a6a2a3fdc8a7.png)

보스의 공격 알고리즘에 대한 클래스가 BossAction입니다. TakeAction 함수가 템플릿 메서드 함수로 이루어지고, Take 함수가 추상 메서드로 선언됩니다.

하위 클래스들은 Take를 오버라이딩합니다. BossAction의 하위클래스만 추가, 변경함으로써, 보스의 기본 시스템은 유지 됩니다.

## [전체코드](https://github.com/ComeBiga/DownWellGame/tree/CatDown_README/DownWell/Assets/1.Scripts/Enemy/Boss/Pattern)

### BossAction.cs
```c#
public abstract class BossAction : MonoBehaviour
{
    public static bool ready = true;
    public static float interval;
    ...
    
    // 템플릿 메서드
    public void TakeAction()
    {
        BossAction.ready = false;

        // Take함수가 interval 시간 이후에 실행
        Invoke("Take", interval);
    }
    
    // 추상메서드
    public abstract void Take();
    
    ...
}
```
