namespace Day1;

public class Dial
{
    public uint Value { get; private set; } = 50;
    public uint ZeroesSeen { get; private set; }

    public void Rotate(Direction direction, uint distance)
    {
        // Clamp distance to a range of 0-99.
        if (distance > 99)
        {
            ZeroesSeen += distance / 100;
            distance %= 100;
        }

        switch (direction)
        {
            case Direction.Left:
                // Prevent underflow.
                if (distance > Value)
                {
                    if (Value != 0) // Prevent double-counts.
                        ZeroesSeen++;

                    Value += 100;
                }

                Value -= distance;
                break;

            case Direction.Right:
                Value += distance;

                // Wrap around.
                if (Value > 99)
                {
                    Value -= 100;
                    if (Value != 0) // Prevent double-counts.
                        ZeroesSeen++;
                }

                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        if (Value == 0)
            ZeroesSeen++;
    }
}