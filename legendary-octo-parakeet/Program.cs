int[] inputBuffer = { };

void showHelp()
{
    string action = inputBuffer.Length > 0 ? "продолжить" : "начать";
    Console.WriteLine($"Нажмите 1 + Enter, для выполнения команды 1: {action} ввод элементов массива");
    if (inputBuffer.Length > 0)
    {
        Console.WriteLine("Нажмите 2 + Enter, для выполнения команды 2: завершить ввод элементов массива и вывести результат");
        Console.WriteLine("Нажмите 3 + Enter, для выполнения команды 3: очистить массив");
    }
    Console.WriteLine("Нажмите 4 + Enter, для выполнения команды 4: задать параметры генерации массива случайных чисел и вывести результат");
    Console.WriteLine("Нажмите 5 + Enter, для выхода из программы");
}

int[] filterEvens(int[] array)
{
    int[] result = { };
    int[] tmpArray = new int[10];
    int tal = 0;

    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] % 2 != 0)
        {
            continue;
        }
        tmpArray[tal++] = array[i];
        if (tal >= tmpArray.Length)
        {
            int oldSize = result.Length;
            Array.Resize(ref result, oldSize + tal);
            Array.ConstrainedCopy(tmpArray, 0, result, oldSize, tal);
            tal = 0;
        }
    }

    if (tal > 0)
    {
        int oldSize = result.Length;
        Array.Resize(ref result, oldSize + tal);
        Array.ConstrainedCopy(tmpArray, 0, result, oldSize, tal);
    }

    return result;
}

string arrayToString(int[] array)
{
    return "{" + String.Join(", ", array) + "}";
}

void startInput()
{
    Console.WriteLine($"Введите целое число что бы добавить элемент в массив {arrayToString(inputBuffer)}");
    Console.WriteLine("Что бы выйти из режима ввода массива нажмите Enter");
    while (true)
    {
        string? nextInput = Console.ReadLine();
        if (nextInput == "")
        {
            break;
        }
        int nextNumber;
        try
        {
            nextNumber = int.Parse(nextInput!.Replace(" ", ""));
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine($"Извините, не могу распознать число {nextInput}");
            continue;
        }
        catch (FormatException)
        {
            Console.WriteLine($"Извините, не могу распознать число {nextInput} ошибка формата");
            continue;
        }
        catch (OverflowException)
        {
            Console.WriteLine($"Извините, число {nextInput} выходит за рамки интервала {int.MinValue} .. {int.MaxValue}");
            continue;
        }
        Array.Resize(ref inputBuffer, inputBuffer.Length + 1);
        inputBuffer[inputBuffer.Length - 1] = nextNumber;
    }
}

void finishInput()
{
    Console.WriteLine("Входные данные");
    Console.WriteLine(arrayToString(inputBuffer));
    int[] resultArray = filterEvens(inputBuffer);
    Console.WriteLine("Выходные данные");
    Console.WriteLine(arrayToString(resultArray));
}

void clearInputArray()
{
    inputBuffer = new int[]{};
}

int getRandomArrayLengthFromInput()
{
    int result = 10;
    Console.WriteLine("Введите длину случайного массива целых чисел");
    while (true)
    {
        string? inputString = Console.ReadLine();
        try
        {
            result = int.Parse(inputString!.Replace(" ", ""));
            if (result <= 0)
            {
                Console.WriteLine($"К сожалению, длина массива должна быть положительным числом, больше нуля");
                continue;
            }
            break;
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine($"Извините, не могу распознать число {inputString}");
            continue;
        }
        catch (FormatException)
        {
            Console.WriteLine($"Извините, не могу распознать число {inputString} ошибка формата");
            continue;
        }
        catch (OverflowException)
        {
            Console.WriteLine($"Извините, число {inputString} выходит за рамки интервала {int.MinValue} .. {int.MaxValue}");
            continue;
        }
    }
    return result;
}

void randomTest()
{
    int inputBufferLength = getRandomArrayLengthFromInput();
    inputBuffer = new int[inputBufferLength];
    Random random = new Random();
    for (int i = 0; i < inputBufferLength; i++) {
        inputBuffer[i] = random.Next();
    }
    Console.WriteLine("Входные данные");
    Console.WriteLine(arrayToString(inputBuffer));
    int[] resultArray = filterEvens(inputBuffer);
    Console.WriteLine("Выходные данные");
    Console.WriteLine(arrayToString(resultArray));
}

Console.WriteLine("Программа предназначена для фильтрации массива целых чисел по критерию четности.");
showHelp();
while (true)
{
    string? nextInput = Console.ReadLine();
    if (nextInput == "1")
    {
        startInput();
    }
    else if (nextInput == "2" && inputBuffer.Length > 0)
    {
        finishInput();
    }
    else if (nextInput == "3" && inputBuffer.Length > 0)
    {
        clearInputArray();
    }
    else if (nextInput == "4")
    {
        randomTest();
    }
    else if (nextInput == "5")
    {
        Console.WriteLine("Спасибо за использование приложения");
        break;
    }
    showHelp();
}
