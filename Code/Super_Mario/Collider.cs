namespace Mario;

internal static class Collider
{
    public static bool CheckTwoColliders(IRigidble firstCollider, IRigidble secondCollider)
    {
        if (firstCollider == null || secondCollider == null)
            throw new ArgumentException("One of the colliders is null");

        int firstColliderBorderTop = firstCollider.ColliderCenterY - firstCollider.ColliderHeight / 2;
        int firstColliderBorderDown = firstCollider.ColliderCenterY + firstCollider.ColliderHeight / 2;

        int firstColliderBorderLeft = firstCollider.ColliderCenterX - firstCollider.ColliderWidth / 2;
        int firstColliderBorderRight = firstCollider.ColliderCenterY + firstCollider.ColliderWidth / 2;

        int secondColliderBorderTop = secondCollider.ColliderCenterY - secondCollider.ColliderHeight / 2;
        int secondColliderBorderDown = secondCollider.ColliderCenterY + secondCollider.ColliderHeight / 2;

        int secondColliderBorderLeft = secondCollider.ColliderCenterX - secondCollider.ColliderWidth / 2;
        int secondColliderBorderRight = secondCollider.ColliderCenterX + secondCollider.ColliderWidth / 2;

        bool xRangeIsSame = false;

        bool yRangeIsSame = false;

        for (int i = firstColliderBorderTop; i <= firstColliderBorderDown; i++)
        {
            for (int j = secondColliderBorderTop; j <= secondColliderBorderDown; j++)
            {
                if (i == j)
                    yRangeIsSame = true;
            }
        }

        for (int i = firstColliderBorderLeft; i <= firstColliderBorderRight; i++)
        {
            for (int j = secondColliderBorderLeft; j <= secondColliderBorderRight; j++)
            {
                if (i == j)
                    xRangeIsSame = true;
            }
        }

        if (
            firstColliderBorderLeft == secondColliderBorderRight && yRangeIsSame 
            || firstColliderBorderRight == secondColliderBorderLeft && yRangeIsSame
            || firstColliderBorderTop == secondColliderBorderDown && xRangeIsSame
            || firstColliderBorderDown == secondColliderBorderTop && xRangeIsSame
            )
            return true;

        return false;
    }

    public static bool CheckColliders(IRigidble collider, IRigidble[] colliders)
    {
        for (int index = 0; index < colliders.Length; index++)
        {
            if (collider == null || colliders[index] == null)
                throw new ArgumentException("One of the colliders is null");

            int firstColliderBorderTop = collider.ColliderCenterY - collider.ColliderHeight / 2;
            int firstColliderBorderDown = collider.ColliderCenterY + collider.ColliderHeight / 2;

            int firstColliderBorderLeft = collider.ColliderCenterX - collider.ColliderWidth / 2;
            int firstColliderBorderRight = collider.ColliderCenterX + collider.ColliderWidth / 2;

            int secondColliderBorderTop = colliders[index].ColliderCenterY - colliders[index].ColliderHeight / 2;
            int secondColliderBorderDown = colliders[index].ColliderCenterY + colliders[index].ColliderHeight / 2;

            int secondColliderBorderLeft = colliders[index].ColliderCenterX - colliders[index].ColliderWidth / 2;
            int secondColliderBorderRight = colliders[index].ColliderCenterX + colliders[index].ColliderWidth / 2;

            bool xRangeIsSame = false;

            bool yRangeIsSame = false;

            for (int i = firstColliderBorderTop; i <= firstColliderBorderDown; i++)
            {
                for (int j = secondColliderBorderTop; j <= secondColliderBorderDown; j++)
                {
                    if (i == j)
                        yRangeIsSame = true;
                }
            }

            for (int i = firstColliderBorderLeft; i <= firstColliderBorderRight; i++)
            {
                for (int j = secondColliderBorderLeft; j <= secondColliderBorderRight; j++)
                {
                    if (i == j)
                        xRangeIsSame = true;
                }
            }

            if (
                firstColliderBorderLeft == secondColliderBorderRight && yRangeIsSame
                || firstColliderBorderRight == secondColliderBorderLeft && yRangeIsSame
                || firstColliderBorderTop == secondColliderBorderDown && xRangeIsSame
                || firstColliderBorderDown == secondColliderBorderTop && xRangeIsSame
                )
                return true;
        }

        return false;
    }

    public static bool CheckColliders(IRigidble collider, List<IRigidble> colliders)
    {
        for (int index = 0; index < colliders.Count; index++)
        {
            if (collider == null || colliders[index] == null)
                throw new ArgumentException("One of the colliders is null");

            int firstColliderBorderTop = collider.ColliderCenterY - collider.ColliderHeight / 2;
            int firstColliderBorderDown = collider.ColliderCenterY + collider.ColliderHeight / 2;

            int firstColliderBorderLeft = collider.ColliderCenterX - collider.ColliderWidth / 2;
            int firstColliderBorderRight = collider.ColliderCenterX + collider.ColliderWidth / 2;

            int secondColliderBorderTop = colliders[index].ColliderCenterY - colliders[index].ColliderHeight / 2;
            int secondColliderBorderDown = colliders[index].ColliderCenterY + colliders[index].ColliderHeight / 2;

            int secondColliderBorderLeft = colliders[index].ColliderCenterX - colliders[index].ColliderWidth / 2;
            int secondColliderBorderRight = colliders[index].ColliderCenterX + colliders[index].ColliderWidth / 2;

            bool xRangeIsSame = false;

            bool yRangeIsSame = false;

            for (int i = firstColliderBorderTop; i <= firstColliderBorderDown; i++)
            {
                for (int j = secondColliderBorderTop; j <= secondColliderBorderDown; j++)
                {
                    if (i == j)
                        yRangeIsSame = true;
                }
            }

            for (int i = firstColliderBorderLeft; i <= firstColliderBorderRight; i++)
            {
                for (int j = secondColliderBorderLeft; j <= secondColliderBorderRight; j++)
                {
                    if (i == j)
                        xRangeIsSame = true;
                }
            }

            if (
                firstColliderBorderLeft == secondColliderBorderRight && yRangeIsSame
                || firstColliderBorderRight == secondColliderBorderLeft && yRangeIsSame
                || firstColliderBorderTop == secondColliderBorderDown && xRangeIsSame
                || firstColliderBorderDown == secondColliderBorderTop && xRangeIsSame
                )
                return true;
        }

        return false;
    }

    public static bool CheckColliders(IRigidble collider, List<Entity> colliders)
    {
        for (int index = 0; index < colliders.Count; index++)
        {
            if (collider == null || colliders[index] == null)
                throw new ArgumentException("One of the colliders is null");

            int firstColliderBorderTop = collider.ColliderCenterY - collider.ColliderHeight / 2;
            int firstColliderBorderDown = collider.ColliderCenterY + collider.ColliderHeight / 2;

            int firstColliderBorderLeft = collider.ColliderCenterX - collider.ColliderWidth / 2;
            int firstColliderBorderRight = collider.ColliderCenterX + collider.ColliderWidth / 2;

            int secondColliderBorderTop = colliders[index].ColliderCenterY - colliders[index].ColliderHeight / 2;
            int secondColliderBorderDown = colliders[index].ColliderCenterY + colliders[index].ColliderHeight / 2;

            int secondColliderBorderLeft = colliders[index].ColliderCenterX - colliders[index].ColliderWidth / 2;
            int secondColliderBorderRight = colliders[index].ColliderCenterX + colliders[index].ColliderWidth / 2;

            bool xRangeIsSame = false;

            bool yRangeIsSame = false;

            for (int i = firstColliderBorderTop; i <= firstColliderBorderDown; i++)
            {
                for (int j = secondColliderBorderTop; j <= secondColliderBorderDown; j++)
                {
                    if (i == j)
                        yRangeIsSame = true;
                }
            }

            for (int i = firstColliderBorderLeft; i <= firstColliderBorderRight; i++)
            {
                for (int j = secondColliderBorderLeft; j <= secondColliderBorderRight; j++)
                {
                    if (i == j)
                        xRangeIsSame = true;
                }
            }

            if (
                firstColliderBorderLeft == secondColliderBorderRight && yRangeIsSame
                || firstColliderBorderRight == secondColliderBorderLeft && yRangeIsSame
                || firstColliderBorderTop == secondColliderBorderDown && xRangeIsSame
                || firstColliderBorderDown == secondColliderBorderTop && xRangeIsSame
                )
                return true;
        }

        return false;
    }

}
