/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача:
 * ------
 * №729 "Эксперимент"
 * https://acmp.ru/index.asp?main=task&id_task=729 
 */

using System;
using System.IO;

namespace Задача_1
{
	class Program
	{
		private const string InputFile = "INPUT.TXT";
		private const string OutputFile = "OUTPUT.TXT";

		public static int[] FlasksValues;
		public static int[] FlasksIndexes;
		public static int ActionsCount;
		public static int FlasksCount;

		static void Main(string[] args)
		{
			// Буферы для хранения ввода-вывода
			string[] inputContent;
			string[] inputStrings;
			string[] outputString = new string[10];

			// Считываем кол-во колб и действий
			inputContent = File.ReadAllLines(InputFile);
			inputStrings = inputContent[0].Split();
			FlasksCount = Convert.ToInt32(inputStrings[0]);
			ActionsCount = Convert.ToInt32(inputStrings[1]);
			FlasksIndexes = new int[FlasksCount];
			FlasksValues = new int[FlasksCount];

			// Заполняем объёмы колб и инициируем массив с индексами подсоединённых колб
			inputStrings = inputContent[1].Split(' ');
			for (int i = 0; i < FlasksCount; i++)
			{
				FlasksValues[i] = Convert.ToInt32(inputStrings[i]);
				FlasksIndexes[i] = i;
			}

			for (int i = 0; i < ActionsCount; i++)
			{
				inputStrings = inputContent[i + 2].Split(' ');
				int first = Convert.ToInt32(inputStrings[0]) - 1;
				int second = Convert.ToInt32(inputStrings[1]) - 1;

				if (FlasksIndexes[first] < FlasksIndexes[second])
				{
					int indexToReplace = FlasksIndexes[first];
					for (int j = 0; j < FlasksCount; j++)
					{
						if (FlasksIndexes[j] == indexToReplace)
						{
							FlasksIndexes[j] = FlasksIndexes[second];
						}
					}
				}

				if (FlasksIndexes[first] > FlasksIndexes[second])
				{
					int indexToReplace = FlasksIndexes[second];
					for (int j = 0; j < FlasksCount; j++)
					{
						if (FlasksIndexes[j] == indexToReplace)
						{
							FlasksIndexes[j] = FlasksIndexes[first];
						}
					}
				}
			}

			int count = 0;
			for (int i = 0; i < FlasksCount; i++)
			{
				int summ = 0;
				for (int j = 0; j < FlasksCount; j++)
				{
					if (FlasksIndexes[j] == i)
					{
						summ += FlasksValues[j];
					}
				}
				if (summ > 0)
				{
					outputString[count] += $"{i + 1} {summ}";
					count++;
				}
			}

			File.WriteAllLines(OutputFile, outputString);
		}
	}
}
