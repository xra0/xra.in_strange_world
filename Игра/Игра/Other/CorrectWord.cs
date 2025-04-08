using System.Linq;

namespace Игра
{
    static class Correct
    {
        /// <summary>
        /// Возвращает правильное окончание слова в зависимости от числа.
        /// </summary>
        public static string CorrectWordEnd(char number, string[] words)
        {
            switch (number)
            {
                case '1': return words[0];
                case '2':
                case '3':
                case '4': return words[1];
                default: return words[2];
            }
        }

        /// <summary>
        /// Возвращает правильное окончание слова для исчисляемого, в зависимости от числа.
        /// </summary>
        public static string CorrectSpell(string number, string[] words)
        {
            int numberLength = number.Length; //Получаем длину строки
            switch (number.Length)
            {
                case 1: return CorrectWordEnd(number[0], words);
                case 2:
                    if (number[0] == 1)
                        return words[2];
                    return CorrectWordEnd(number[1], words);
                default: return CorrectWordEnd(number.Last(), words);
            }
        }
    }
}
