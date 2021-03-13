using System;
using System.Collections.Generic;
using System.Text;

namespace LoginnigTypesLessonHW.Services
{
    public class MenuService
    {

        public static int SelectIndex(int collectionLength)
        {
            Console.Write("\nВыберите индекс: ");
            int.TryParse(Console.ReadLine(), out int choose);
            if (!IsRightIndex(choose, collectionLength))
            {
                throw new ArgumentOutOfRangeException("Вы выбрали неправильный индекс!!!");
            }
            return choose;
        }

        public static void ShowElements(ICollection<string> elements)
        {
            int iterator = new int();
            foreach (var element in elements)
            {
                Console.WriteLine($"{++iterator}. {element}");
            }
        }

        public static bool IsRightIndex(int index, int length)
        {
            if (index == 0 || index > length)
                return false;
            return true;
        }
    }
}
