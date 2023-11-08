using UnityEngine;

public static class CameraExtensions
{
    public enum Corner
    {
        TopRight,
        BottomLeft
    }

    public enum Side
    {
        Left,
        Right,
        Top,
        Bottom,
        None
    }

    /// <summary>
    ///     Calculates the screen-bounds based on the current camera.
    /// </summary>
    /// <param name="self">The camera to calculate it from.</param>
    /// <param name="cornerToReturn">The corner to return.</param>
    /// <returns></returns>
    public static Vector3 GetScreenBounds(this Camera self, Corner cornerToReturn)
    {
        return cornerToReturn switch
        {
            Corner.TopRight => self.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)),
            Corner.BottomLeft => self.ScreenToWorldPoint(Vector3.zero),
            _ => Vector3.zero
        };
    }

    /// <summary>
    ///     Calculates the screen center based on the screen bounds.
    ///     Note: Note sure if the Z correctly returns the center-z.
    /// </summary>
    /// <returns>A vector3 representing the center of the screen.</returns>
    public static Vector3 GetScreenCenter(this Camera self)
    {
        float centerX = Screen.width * 0.5f;
        float centerY = Screen.height * 0.5f;

        Vector3 calculatedCenter = self.ScreenToWorldPoint(new Vector3(centerX, centerY, 0));

        return calculatedCenter;
    }

    /// <summary>
    ///     Returns true if the object is outside of the screen.
    /// </summary>
    /// <param name="self">This camera</param>
    /// <param name="location">The location to check</param>
    /// <param name="outBound">The side where it goes out of the screen</param>
    public static bool CheckLocationWithScreenBounds(this Camera self, Vector3 location, out Side outBound)
    {
        // Returns true if outside of screen.
        Vector3 bottomLeft = self.GetScreenBounds(Corner.BottomLeft);
        Vector3 topRight = self.GetScreenBounds(Corner.TopRight);

        bool x = false;
        bool y = false;

        if (location.x > topRight.x)
        {
            x = true;
            outBound = Side.Right;
        }

        else if (location.x < bottomLeft.x)
        {
            x = true;
            outBound = Side.Left;
        }

        else if (location.y > topRight.y)
        {
            y = true;
            outBound = Side.Top;
        }

        else if (location.y < bottomLeft.y)
        {
            y = true;
            outBound = Side.Bottom;
        }
        else
        {
            outBound = Side.None;
        }

        return x || y;
    }
}
