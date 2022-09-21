

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class OurExtensions
{
    public static GameObject[] CheckParents(params GameObject[] objects)
    {
        List<GameObject> parents = new List<GameObject>(objects.Length);
        Transform[] transforms = objects.Select(go => go.transform).ToArray();
        for (int objectIndex = 0; objectIndex < transforms.Length; objectIndex++)
        {
            if (!IsChildOfAny(transforms[objectIndex], transforms))
                parents.Add(transforms[objectIndex].gameObject);
        }

        return parents.ToArray();
    }

    public static bool IsChildOfAny(Transform potentialChild, params Transform[] potentialParents)
    {
        for (int index = 0; index < potentialParents.Length; index++)
        {
            if (IsParentOf(potentialParents[index], potentialChild))
                return true;
        }
        return false;
    }

    public static bool IsParentOf(Transform potentialParent, Transform potentialChild)
    {
        if (potentialChild.parent == null)
            return false;

        if (potentialChild.parent == potentialParent)
            return true;

        return IsParentOf(potentialParent, potentialChild.parent);
    }

}
