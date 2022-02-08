# EnemyBrain
![eaE](https://user-images.githubusercontent.com/36800639/153006817-a898281b-903d-4a63-9915-f821584a1b8e.PNG)

적의 AI를 구현하기 위한 코드입니다. 비슷한 움직임이 많은 적들이기 때문에, 비슷한 움직임의 코드를 재사용하기 유용하게 구조를 디자인했습니다.

BossBrain과 유사하지만 적 AI는 공격패턴이 아니고, 단순한 움직임이기 때문에 Decision이라는 AI의 의사결정에 대한 코드가 추가되었습니다.
이 의사결정에 따라서 적의 Act가 바뀝니다.

## 구조
![EnemyBrainDiagram](https://user-images.githubusercontent.com/36800639/153007277-7290d309-e4e0-45a9-8a34-5d4db4c1cf9c.png)

EnemyAct 클래스의 UpdateAct 함수가 템플릿 메서드 함수로 구현되었습니다.

EnemyActionState의 CheckTransition 함수를 통해서 ActState를 바꾸는 것을 결정합니다.

## [전체코드](https://github.com/ComeBiga/DownWellGame/tree/CatDown_README/DownWell/Assets/99.EnemyEditor/Scripts)
### EnemyAct.cs
```c#
public abstract class EnemyAct : MonoBehaviour
{
    ...

    public bool UpdateAct()
    {
        ...
        
        doNextAct = Act(null);

        ...
    }
    
    ...
    public abstract bool Act(Rigidbody2D rigidbody);
    ...
}
```

### EnemyAction.cs

```c#
public class EnemyAction : MonoBehaviour
{
    ...
    public List<EnemyActionState> actionStates;

    private string currentStateName = "";
    private EnemyActionState currentState;

    ...

    private void Update()
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return;

        currentState.UpdateActionState();

        if(currentState.CheckTransition(out currentStateName))
        {
            currentState = actionStates.Find(s => s.stateName == currentStateName);
        }
    }
    
    ...
}
```

```c#
public class EnemyActionState
{
    public string stateName = "";
    public List<EnemyAct> acts;
    public List<EnemyTransition> transitions;

    ...

    public void UpdateActionState()
    {
        if(acts[index].UpdateAct())
        {
            ...
        }
    }

    public bool CheckTransition(out string stateTo)
    {
        stateTo = "";

        foreach(var t in transitions)
        {
            // 만약 조건에 만족한다면
            if (t.Decide(out stateTo))
            {
                ...
                return true;
            }
        }

        return false;
    }

    ...
}
```

```c#
public class EnemyTransition
{
    public EnemyDecision decision;
    public string trueState = "";
    public string falseState = "";
    public UnityEvent onTransition;

    public bool Decide(out string state)
    {
        state = (decision.Decide() == true) ? trueState : falseState;

        return (state != "") ? true : false;
    }
}
```
