using System.Collections.Generic;

public class SpawnUIPresenter
{
    private readonly SpawnUIView view;
    //spawn Manager와 의존성 추가 필요.

    public SpawnUIPresenter(SpawnUIView view)
    {
        this.view = view;
    }
}
