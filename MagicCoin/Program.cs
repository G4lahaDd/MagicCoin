internal class Program
{
    record MagicCoin(double statProbatility, int index);

    private static void Main(string[] args)
    {
        //кол-во монет
        int N = 70;
        //Кол-во попыток на одну монету
        //Чем выше это значение, тем выше точность поиска
        int K = 100;
        double[] probabilities = new double[N];

        Random random = new Random();
        for (int i = 0; i < N; i++)
        {
            // вероятость выпадения решки от 0.2 до 0.8
            probabilities[i] = 0.8 - random.NextDouble() * 0.6;
        }

        var index = GetIndexOfMagicCoin(probabilities, K);

        Console.WriteLine($"statistic: {probabilities[index]}, real: {probabilities.Max()}");
    }

    /// <summary>
    /// Ищет монету с максимальным значением вероятности выпадения решки, на основе статистической вероятности
    /// 
    /// Сложность - O(n^2)
    /// </summary>
    /// <param name="probabilities">Массив вероятностей выпадения решки</param>
    /// <param name="k">Количество попыток на одну монету</param>
    /// <returns>Индекс монеты с максимальной вероятностью</returns>
    private static int GetIndexOfMagicCoin(double[] probabilities, int k)
    {
        Random random = new Random();
        //Отсортированный список статистических вероятностей
        List<MagicCoin> coins = new List<MagicCoin>();

        double statisticProb;// Статистическая вероятность
        int count;// Кол-во благоприятных исходов
        for (int i = 0; i < probabilities.Length; i++)
        {

            count = 0;
            for (int j = 0; j < k; j++)
            {
                if (random.NextDouble() <= probabilities[i]) count++;
            }
            //Вычисление статистической вероятности
            statisticProb = (double)count / k;
            
            MagicCoin coin = new MagicCoin(statisticProb, i);
            // Вставка монеты в список по значению вероятности
            var index = coins.FindIndex(x => x.statProbatility > coin.statProbatility);// O(N)

            if (index < 0)
            {
                coins.Add(coin);
            }
            else
            {
                coins.Insert(index, coin);
            }
        }

        return coins[^1].index;
    }
}