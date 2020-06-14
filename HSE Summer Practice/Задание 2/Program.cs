/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача:
 * ------
 * №458 "Шифровка - 2"
 * https://acmp.ru/index.asp?main=task&id_task=458
 */

using System;
using System.IO;
using System.Text;

namespace Задача_2
{
	class Program
	{
		private const string InputFile = "INPUT.TXT";
		private const string OutputFile = "OUTPUT.TXT";

		static void Main(string[] args)
		{
			var InputFileStrings = File.ReadAllLines(InputFile, Encoding.GetEncoding(866));
			int rectHeight = Convert.ToInt32(InputFileStrings[0]);

			int[] lineNumbers = new int[rectHeight];
			var notConvertedLineNumbers = InputFileStrings[1].Split();
			for (int i = 0; i < rectHeight; i++)
			{
				lineNumbers[i] = Convert.ToInt32(notConvertedLineNumbers[i]) - 1;
			}

			char[][] decodingTable = new char[rectHeight][];
			string encodedWord = InputFileStrings[2];
			int positiont = 0;
			for (int i = 0; i < lineNumbers.Length; i++)
			{
				int length;
				// Отсчитываем сколько символов извлекаем в таблицу из слова:
				if (lineNumbers[i] < encodedWord.Length % rectHeight)
				{
					length = encodedWord.Length / rectHeight + 1;
				}
				else
				{
					length = encodedWord.Length / rectHeight;
				}
				char[] decodingLine = encodedWord.Substring(positiont, length).ToCharArray();
				// Записываем в соответствующую строку таблицы часть символов слова:
				decodingTable[lineNumbers[i]] = new char[decodingLine.Length];
				decodingTable[lineNumbers[i]] = decodingLine;

				positiont += length;
			}

			// Деодируем сообщение:
			string decodedWord = String.Empty;
			int columnCount = (encodedWord.Length + rectHeight - 1) / rectHeight;
			for (int columnNumber = 0; columnNumber < columnCount; columnNumber++)
			{
				for (int rowNumber = 0; rowNumber < rectHeight; rowNumber++)
				{
					if (columnNumber < decodingTable[rowNumber].Length)
					{
						decodedWord += decodingTable[rowNumber][columnNumber];
					}
				}
			}

			File.WriteAllText(OutputFile, decodedWord, Encoding.GetEncoding(866));
		}
	}
}
