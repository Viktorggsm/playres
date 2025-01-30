using System;

namespace playres
{
    class Program
    {
        static void Main(string[] args)
        {
            int [] chips  = new int[] {1, 5, 9, 10, 5};
            //int[] chips = new int[] { 1, 2, 3 };
            //int[] chips = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 2 };
            int max, min; // максиамальнве и минимальные знач массива
            int indexMax = 0;   
            int indexMin = 0;
            int next;    // индекс массива с права без переполнния
            int nextOver; // индекс массива с права с переполнением и опять начиная с нуля
            int previouse; // преведущий индекс массива
            int previouseOver; // преведущий индекс массива с переходом через ноль
            int counter = 0; // количество переходов фишек
            int step;     // минимальное количество переходов от мак до мин значения
            bool good;   // когда у всех одинаковое количество фишек
;            
            Console.Write("Раздача фишек :");     // 
            //Console.WriteLine();
            for (int i=0; i<chips.Length;i++)                      // 
            {                                                       //   Вывод на экран начальных значений фишек
                Console.Write($" {chips[i]}");                     //
            }                                                      // 
            Console.WriteLine();                                    //
            max = min = 0;
            while (true)
               {
                step = max = indexMax = indexMin = next = nextOver = previouseOver = 0;
                min = 32600;
                good = true;
                for (int i = 0; i < chips.Length; i++)
                {
                    if (chips[i] >  max) { max = chips[i]; indexMax = i; };
                    if (chips[i] < min) { min = chips[i]; indexMin = i; };
                }

                next = Math.Abs(indexMax - indexMin);            //разность между индексами min max без переолнения и перехода через ноль массива chips[]
                nextOver = (chips.Length - indexMax + indexMin); //   разность м/у индексами max min но походя через последего и начиная с нуля          
                previouseOver = (indexMax - 0 + (chips.Length - indexMin));       // разность м/у индексами max min но походя через ноль и начиная с последнего         

                chips[indexMax] = chips[indexMax] - 1; // убавляем еденицу от макс.значения 
                chips[indexMin] = chips[indexMin] + 1;  // добовляем одну фишку миним.значению
                step = Math.Min(Math.Min(next, nextOver), previouseOver);// выбираем минимальное число передвиженией фишек м/у min max индексами () 
                counter = counter + step; //количество преходов
                
                Console.Write("Фишки игроков :");
                
                for (int i = 0; i < chips.Length; i++)
                {
                    Console.Write( chips[i]+" ");
                }
                Console.Write("Переходов : "+counter);
                Console.WriteLine();
                //Console.Read();

                for (int i = 0; i < chips.Length; i++)                                //
                {                                                                       //
                    if (chips[i] == chips[chips.Length - 1]) good = good && true;       //
                    else good = good && false;                                          //  проверка на одинаковость у игроков фишек
                }                                                                       //  
                if (good == true)                                                       //
                    {                                                                   //
                    //Console.WriteLine("У игроков одинаковое количество фишек");         //   вывод количества переходов 
                    Console.WriteLine("Общее количество переходов :" +counter);         //
                    Console.WriteLine("Пауза .Нажмите Enter. ");   
                    Console.Read();                                                     //
                    break; 
                    }                                                                  //
                    
            }

        }

    }
}
