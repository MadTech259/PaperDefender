namespace Core
{
    public static class Guids
    {
        private static long cached = 0;

        public  static long Create() => cached++;
    }
}