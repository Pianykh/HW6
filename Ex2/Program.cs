using System;
using System.Collections;
using System.Threading;
/*
* 2.Организовать очередь на прием к доктору с помощью Queue, чтобы пациент проводил у доктора от 5 до 15 секунд рандомно. 
* Кол-во пациентов должно быть не менее 5-ти. Все действия должны выводиться в консоль.
*/

namespace Ex2
{
    internal class Program
    {
        static void Main()
        {
            var random = new Random();
            var queue = new Queue();
            var patientsNames = new[] { "Вергилий", "Аристотель", "Платон", "Сократ", "Демокрит", "Архимед" };
            foreach (var name in patientsNames)
                queue.Enqueue(name);
            while(queue.Count > 0)
            {
                Console.WriteLine($"Всего пациентов в очереди: {queue.Count}");
                Console.WriteLine($"К врачу зашел пациент {queue.Peek()}");
                Thread.Sleep(1000 * (random.Next(11) + 5));
                Console.WriteLine($"Пациент {queue.Dequeue()} вышел");
            }
        }
    }
}
