
public static class RandomGenerator
{
    private static System.Random s_random = new System.Random();

    public static float Range(float minimum, float maximum) => (float)(s_random.NextDouble() * (maximum - minimum) + minimum);
}
