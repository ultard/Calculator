double currentValue = 0; // Текущее значение на "дисплее"
double memory = 0;       // Значение в памяти
string? pendingOperation = null; // Ожидаемая операция для бинарных операций
var isNewInput = true;  // Флаг для ввода нового числа

Console.WriteLine("Классический калькулятор на C#. Введите 'exit' для выхода.");
Console.WriteLine("Поддерживаемые операции: +, -, *, /, %, 1/x, x^2, sqrt, M+, M-, MR, MC, OP");

while (true)
{
    Console.Write("Введите число или операцию: ");
    var input = Console.ReadLine()?.Trim();

    if (input?.ToLower() == "exit")
    {
        break;
    }
    
    if (double.TryParse(input, out var number))
    {
        if (isNewInput)
        {
            currentValue = number;
        }
        else if (pendingOperation != null)
        {
             currentValue = ApplyBinaryOperation(currentValue, number, pendingOperation);
        }
        
        isNewInput = false;
        Console.WriteLine($"Текущее значение: {currentValue}");
        continue;
    }
    
    switch (input)
    {
        case "+":
        case "-":
        case "*":
        case "/":
        case "%":
            pendingOperation = input;
            isNewInput = false;
            Console.WriteLine($"Ожидаемая операция: {pendingOperation}");
            break;

        case "1/x":
            if (currentValue == 0)
            {
                Console.WriteLine("Ошибка: деление на ноль.");
            }
            else
            {
                currentValue = 1 / currentValue;
                Console.WriteLine($"Текущее значение: {currentValue}");
            }
            break;

        case "x^2":
            currentValue = Math.Pow(currentValue, 2);
            Console.WriteLine($"Текущее значение: {currentValue}");
            break;

        case "sqrt":
            if (currentValue < 0)
            {
                Console.WriteLine("Ошибка: квадратный корень из отрицательного числа.");
            }
            else
            {
                currentValue = Math.Sqrt(currentValue);
                Console.WriteLine($"Текущее значение: {currentValue}");
            }
            break;

        case "M+":
            memory += currentValue;
            Console.WriteLine($"Память: {memory}");
            break;

        case "M-":
            memory -= currentValue;
            Console.WriteLine($"Память: {memory}");
            break;

        case "MR":
            currentValue = memory;
            Console.WriteLine($"Текущее значение из памяти: {currentValue}");
            break;
        
        case "MC":
            memory = 0;
            currentValue = memory;
            Console.WriteLine($"Текущее значение из памяти: {currentValue}");
            break;

        case "OP":
            Console.WriteLine($"Текущая операция: {pendingOperation ?? "Не задана"}");
            break;
        
        default:
            Console.WriteLine("Неизвестная операция.");
            break;
    }
}

return;

double ApplyBinaryOperation(double a, double b, string op)
{
    switch (op)
    {
        case "+": return a + b;
        case "-": return a - b;
        case "*": return a * b;
        case "/":
            if (b != 0) return a / b;
            
            Console.WriteLine("Ошибка: деление на ноль.");
            return a;
        case "%": return a % b;
        default:
            Console.WriteLine("Неизвестная бинарная операция.");
            return a;
    }
}