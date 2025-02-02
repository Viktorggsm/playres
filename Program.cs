//
// программа    ищет в массиве  фишек максимумы и минимумы и передает от максимального значения к минимальному считая переходы
// программа вычисляет наименьший маршрут от максимального к минимальному либо по кругу (через последнего и нулевого игрока) либо наоборот(через нулевого к последнему)
// программа учитывает ситуации когда при переходах фишек образуется два или более максимумов (из них выбираеся самый кротчайший к минимальному) 
// программа учитывает ситуации когда  два или более минимумов { 7, 2, 4, 10, 2 };(из них выбираеся самый кротчайший от максимального) 
// для этого созаны массивы с индексами максимуов arrMax  и массив с индексами минимумов arrMin
using System;

namespace playres
{
    class Program
    {
        static void Main(string[] args)
        {
            //int [] chips  = new int[] {1, 5, 9, 10, 5};
            //int[] chips = new int[] { 1, 2, 3 };
            //int[] chips = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 2 };
             int[] chips = new int[] { 6, 2, 4, 10, 3 };
            //int[] chips = new int[] { 6, 3, 4, 9, 3 };
            //int[] chips = new int[] { 7, 2, 4, 10, 2 };
            int max, min; // максиамальнве и минимальные знач массива
            int indexMax = 0;   
            int indexMin = 0;
            int next;    // индекс массива с права без переполнния
            int nextOver; // индекс массива с права с переполнением и опять начиная с нуля
            int previouseOver; // преведущий индекс массива с переходом через ноль
            int counter = 0; // количество переходов фишек
            int step;     // минимальное количество переходов от мак до мин значения
            bool good;   // когда у всех одинаковое количество фишек
            int indexArrMax; //номер в массиве индексов максимумов
            int indexArrMin;  //номер в массиве индексов минимумов
            int[] arrMax = new int[chips.Length];    // массив максимумов
            int[] arrMin = new int[chips.Length];    // массив минимумов
            int stepMin=32000;
            Console.Write("Раздача фишек :");                      // 
            for (int i=0; i<chips.Length;i++)                      // 
            {                                                      //   Вывод на экран начальных значений фишек
                Console.Write($" {chips[i]}");                     //
            }                                                      // 
            Console.WriteLine();                                   //
            max = min = 0;
            while (true)                                           // основной цикл  выход из него когда у всех  равное количество фишек
               {
                 max = indexMax = indexMin = next = nextOver = previouseOver = 0;
                 min = 32600;
                good = true;
                indexArrMax = 0;
                indexArrMin = 0;
                step = 0;
                //stepMin = 32000;
                for (int i = 0; i < chips.Length; i++) arrMin[i] = arrMax[i] = 0; //инициализация массивов
                
                for (int i = 0; i < chips.Length; i++)                            // в цикле поиск максимумов и минимумов и запись их в массивы arrMax[] ArrMin[]
                {    
                    if (chips[i] > max) {
                        max = chips[i];                // max значение массива
                        indexMax = i;                  // индекс массива фишек 
                        arrMax[0] = indexMax;          // запись в массив максимувов индекса массива максимумов
                    }
                    else if (chips[i] == max)
                    {
                        indexMax = i;
                        arrMax[++indexArrMax] = indexMax; }


                    if (chips[i] < min)
                        { min = chips[i];
                        indexMin = i;
                        arrMin[0] = indexMin;
                        
                    }

                   else if (chips[i] == min)
                    {
                        indexMin = i;
                        arrMin[++indexArrMin] = indexMin;
                    }
                 }
              

                for (int iMax = 0; iMax < indexArrMax+1; iMax++)        // перебераем массив с индексами максимальных значений
                {
                    stepMin = 32000;                                   //уставка начального минимального шага между индесами мак и мин значений 
                    
                    for (int iMin = 0; iMin < indexArrMin + 1; iMin++) // перебираем массив с индексами минимальных значений
                    {
                    
                          next = Math.Abs(arrMax[iMax] - arrMin[iMin]);             //разность между индексами min max без переолнения и перехода через ноль массива chips[]
                        nextOver = (chips.Length - arrMax[iMax] + arrMin[iMin]);    //   разность м/у индексами max min но походя через последего и начиная с нуля          
                        previouseOver = (arrMax[iMax] - 0 + (chips.Length - arrMin[iMin]));       // разность м/у индексами max min но походя через ноль и начиная с последнего         
                        step = Math.Min(Math.Min(next, nextOver), previouseOver);    // выбираем минимальное число передвижений фишек м/у min max индексами ()
                                                                                
                        if (step < stepMin)     //поиск самого короткого шага от максимума до минимума против и по часовой стрелке игрового стола 
                        {
                            stepMin = step;
                            indexMin = arrMin[iMin];  //сохранение индексов массива max и min  при которых кротчайщий переход фишек
                            indexMax = arrMax[iMax];  //
                        }

                    }
                }

                chips[indexMax] = chips[indexMax] - 1; // убавляем еденицу от макс.значения 
                chips[indexMin] = chips[indexMin] + 1;  // добавляем одну фишку миним.значению
             
                counter = counter + stepMin; // общее количество преходов фишек

                Console.Write("Фишки игроков :");           //
                for (int i = 0; i < chips.Length; i++)      //
                {                                            //
                    Console.Write( chips[i]+" ");            //   Вывод на экран результатов переходов фишек при передвижениях 
                }                                            //
                Console.Write("Переходов : "+stepMin);       //
                Console.WriteLine();                         //
                //Console.Read(); // пауза при каждом переходе

                for (int i = 0; i < chips.Length; i++)                                  //
                {                                                                       //
                    if (chips[i] == chips[chips.Length - 1]) good = good && true;       //
                    else good = good && false;                                          //  проверка на равенство у игроков фишек
                }                                                                       //  
                if (good == true)                                                       //
                    {                                                                   //
                                                                                        //   вывод количества переходов 
                    Console.WriteLine("Общее количество переходов :" +counter);         //
                    Console.WriteLine("Пауза. Нажмите Enter. ");                        //   
                    Console.Read();                                                     //
                    break;    //выход из основного цикла. Конец.
                    }                                                                  //
                    
            }

        }

    }
}
