using System.Collections.Generic;
public interface INeedTarget
{
    void SetTarget(Unit targets);
    void SetPotentialTarget(List<Unit> potentialTargets);
    float GetViewDistance();
}