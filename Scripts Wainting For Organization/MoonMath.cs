using System;
using UnityEngine;
public static class MoonMath
{
    public static class Movement
    {
        public static float AccelerateSpeed(float speed, float acceleration, float maxSpeed, bool left)
        {
            if (left && speed < -maxSpeed)
            {
                return speed;
            }
            if (!left && speed > maxSpeed)
            {
                return speed;
            }
            return (!left) ? Mathf.Min(maxSpeed, speed + acceleration * Time.deltaTime) : Mathf.Max(-maxSpeed, speed - acceleration * Time.deltaTime);
        }
        public static float DecelerateSpeed(float speed, float deceleration)
        {
            return (speed <= 0f) ? Mathf.Min(0f, speed + deceleration * Time.deltaTime) : Mathf.Max(0f, speed - deceleration * Time.deltaTime);
        }
        public static float ApplyGravity(float speed, float gravity, float maxSpeed)
        {
            return Mathf.Max(-maxSpeed, speed - gravity * Time.deltaTime);
        }
    }
    public static class Line
    {
        public static Vector3 ClosestPointOnLineSegmentToPoint(Vector3 p1, Vector3 p2, Vector3 p)
        {
            Vector3 vector = p2 - p1;
            if (vector.sqrMagnitude < Mathf.Epsilon)
            {
                return (p1 + p2) / 2f;
            }
            float num = ((p.x - p1.x) * vector.x + (p.y - p1.y) * vector.y) / vector.sqrMagnitude;
            num = Mathf.Clamp01(num);
            return Vector3.Lerp(p1, p2, num);
        }
        public static float DistancePointToLine(Vector3 p1, Vector3 p2, Vector3 p)
        {
            return Vector3.Distance(p, MoonMath.Line.ClosestPointOnLineSegmentToPoint(p1, p2, p));
        }
    }
    public static class Normal
    {
        public static bool WithinDegrees(Vector2 normal1, Vector2 normal2, float degrees)
        {
            return Vector3.Dot(normal1, normal2) >= Mathf.Cos(0.0174532924f * degrees);
        }
    }
    public static class Vector
    {
        public static Vector2 ApplyCircleDeadzone(Vector2 axis, float deadzoneRadius)
        {
            if (axis.magnitude < deadzoneRadius)
            {
                return Vector2.zero;
            }
            return axis.normalized * (axis.magnitude - deadzoneRadius) / (1f - deadzoneRadius);
        }
        public static Vector2 ApplyRectangleDeadzone(Vector2 axis, float deadzoneX, float deadzoneY)
        {
            axis.x = Mathf.Sign(axis.x) * Mathf.Max(Mathf.Abs(axis.x) - deadzoneX, 0f) / (1f - deadzoneX);
            axis.y = Mathf.Sign(axis.y) * Mathf.Max(Mathf.Abs(axis.y) - deadzoneY, 0f) / (1f - deadzoneY);
            return axis;
        }
        public static Vector3 Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }
        public static Vector3 Divide(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        public static Vector2 RotateTowards(Vector2 angleVector, Vector2 targetVector, float delta)
        {
            float num = MoonMath.Angle.AngleFromVector(angleVector);
            float target = MoonMath.Angle.AngleFromVector(targetVector);
            num = Mathf.MoveTowardsAngle(num, target, Time.deltaTime * delta);
            return MoonMath.Angle.VectorFromAngle(num) * angleVector.magnitude;
        }
        public static bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
        {
            bool flag = MoonMath.Vector.sign(pt, v1, v2) < 0f;
            bool flag2 = MoonMath.Vector.sign(pt, v2, v3) < 0f;
            bool flag3 = MoonMath.Vector.sign(pt, v3, v1) < 0f;
            return flag == flag2 && flag2 == flag3;
        }
        private static float sign(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
        }
        public static float Distance(Vector3 start, Vector3 target)
        {
            float num = start.x - target.x;
            float num2 = start.y - target.y;
            float num3 = start.z - target.z;
            return Mathf.Sqrt(num * num + num2 * num2 + num3 * num3);
        }
        public static float Distance(Vector3 start, Vector2 target)
        {
            float num = start.x - target.x;
            float num2 = start.y - target.y;
            float z = start.z;
            return Mathf.Sqrt(num * num + num2 * num2 + z * z);
        }
    }
    public static class Angle
    {
        public static float Wrap(float angle)
        {
            while (angle >= 360f)
            {
                angle -= 360f;
            }
            while (angle < 0f)
            {
                angle += 360f;
            }
            return angle;
        }
        public static float Wrap180(float angle)
        {
            while (angle >= 180f)
            {
                angle -= 360f;
            }
            while (angle < -180f)
            {
                angle += 360f;
            }
            return angle;
        }
        public static float Difference(float value1, float value2)
        {
            return Mathf.Min(Mathf.Abs(value1 - value2), 360f - Mathf.Abs(value1 - value2));
        }
        public static float AngleSubtract(float start, float target)
        {
            float num = MoonMath.Angle.Wrap(target);
            float num2 = MoonMath.Angle.Wrap(start);
            int num3 = (num <= num2) ? -1 : 1;
            if (num3 == -1)
            {
                num = MoonMath.Angle.Wrap(start);
                num2 = MoonMath.Angle.Wrap(target);
            }
            if (Mathf.Abs(num - num2) < 360f - Mathf.Abs(num - num2))
            {
                return Mathf.Abs(num - num2) * (float)num3;
            }
            return (360f - Math.Abs(num - num2)) * (float)num3 * -1f;
        }
        public static float RotateTowards(float startDegrees, float targetDegrees, float degrees)
        {
            if (Mathf.Abs(degrees) > MoonMath.Angle.Difference(startDegrees, targetDegrees))
            {
                return targetDegrees;
            }
            if (MoonMath.Angle.Difference(MoonMath.Angle.Wrap(startDegrees + degrees), targetDegrees) < MoonMath.Angle.Difference(MoonMath.Angle.Wrap(startDegrees - degrees), targetDegrees))
            {
                return MoonMath.Angle.Wrap(startDegrees + degrees);
            }
            return MoonMath.Angle.Wrap(startDegrees - degrees);
        }
        public static Vector2 Rotate(Vector2 v, float angle)
        {
            if (angle == 0f)
            {
                return v;
            }
            float f = angle * 0.0174532924f;
            float num = Mathf.Cos(f);
            float num2 = Mathf.Sin(f);
            return new Vector2(v.x * num - v.y * num2, v.x * num2 + v.y * num);
        }
        public static Vector2 Unrotate(Vector2 v, float angle)
        {
            if (angle == 0f)
            {
                return v;
            }
            return MoonMath.Angle.Rotate(v, -angle);
        }
        public static float AngleFromVector(Vector2 delta)
        {
            delta.Normalize();
            return Mathf.Atan2(delta.y, delta.x) * 57.29578f;
        }
        public static float AngleFromDirection(Vector2 delta)
        {
            return Mathf.Atan2(delta.y, delta.x) * 57.29578f;
        }
        public static Vector2 VectorFromAngle(float angle)
        {
            return new Vector2(Mathf.Cos(angle * 0.0174532924f), Mathf.Sin(angle * 0.0174532924f));
        }
    }
    public static class Float
    {
        public static float Normalize(float x)
        {
            return (x != 0f) ? Mathf.Sign(x) : 0f;
        }
        public static float MoveTowards(float start, float target, float distance)
        {
            if (target > start)
            {
                return Mathf.Min(target, start + distance);
            }
            return Mathf.Max(target, start - distance);
        }
        public static float ClampedAdd(float start, float offset, float min, float max)
        {
            if (offset > 0f && start < max)
            {
                return Mathf.Min(max, start + offset);
            }
            if (offset < 0f && start > min)
            {
                return Mathf.Max(min, start + offset);
            }
            return start;
        }
        public static float ClampedSubtract(float start, float offset, float min, float max)
        {
            if (start < min)
            {
                return Mathf.Min(min, start - offset);
            }
            if (start > max)
            {
                return Mathf.Max(max, start - offset);
            }
            return start;
        }
        public static float ClampedDecrease(float start, float amount, float min, float max)
        {
            if (start < min)
            {
                return Mathf.Min(min, start + amount);
            }
            if (start > max)
            {
                return Mathf.Max(max, start - amount);
            }
            return start;
        }
        public static float Wrap(float value, float min, float max)
        {
            value -= min;
            max -= min;
            value -= Mathf.Floor(value / max) * max;
            return value + min;
        }
        public static float AbsoluteMax(float a, float b)
        {
            return (Mathf.Abs(a) <= Mathf.Abs(b)) ? b : a;
        }
        public static float AbsoluteMin(float a, float b)
        {
            return (Mathf.Abs(a) >= Mathf.Abs(b)) ? b : a;
        }
        public static float AbsoluteDifference(float a, float b)
        {
            return Mathf.Abs(a - b);
        }
    }
    public static class Physics
    {
        public static float SpeedFromHeightAndGravity(float gravity, float height)
        {
            return Mathf.Sqrt(height * 2f * gravity);
        }
    }
    public static class Rectangle
    {
        public static Rect Absolute(Rect rect)
        {
            return Rect.MinMaxRect(Mathf.Min(rect.xMin, rect.xMax), Mathf.Min(rect.yMin, rect.yMax), Mathf.Max(rect.xMin, rect.xMax), Mathf.Max(rect.yMin, rect.yMax));
        }
    }
    public static class Int
    {
        public static int GreatestCommonDenominator(int x, int y)
        {
            while (y != 0)
            {
                int num = x % y;
                x = y;
                y = num;
            }
            return x;
        }
    }
}

