using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinderProgram
{
    public class WordFinder
    {
        private char[,] _matrix;  // 2D array to store the matrix of characters
        private int _rows;        // Number of rows in the matrix
        private int _cols;        // Number of columns in the matrix

        /// <summary>
        /// Constructor to initialize the WordFinder with a matrix of characters.
        /// </summary>
        /// <param name="matrix">The matrix represented as IEnumerable of strings.</param>
        /// <exception cref="ArgumentException">Thrown when matrix is null, empty, exceeds 64x64 dimensions, or has rows with varying lengths.</exception>
        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null || !matrix.Any())
            {
                throw new ArgumentException("Matrix cannot be null or empty.");
            }

            // Convert matrix to a list for easier manipulation
            var matrixList = matrix.ToList();

            _rows = matrixList.Count;
            _cols = matrixList[0].Length;

            // Validate matrix size constraint
            if (_rows > 64 || _cols > 64)
            {
                throw new ArgumentException("Matrix size cannot exceed 64x64.");
            }

            // Initialize the 2D array to store the matrix
            _matrix = new char[_rows, _cols];

            // Populate the matrix from the input list of strings
            for (int i = 0; i < _rows; i++)
            {
                // Check if all rows have the same length as the first row
                if (matrixList[i].Length != _cols)
                {
                    throw new ArgumentException("All rows in the matrix must have the same number of characters.");
                }

                // Fill the matrix with characters from the input strings
                for (int j = 0; j < _cols; j++)
                {
                    _matrix[i, j] = matrixList[i][j];
                }
            }
        }

        /// <summary>
        /// Finds the top 10 most repeated words from the wordstream that exist in the matrix.
        /// </summary>
        /// <param name="wordstream">The wordstream represented as IEnumerable of strings.</param>
        /// <returns>The top 10 most repeated words found in the matrix from the wordstream.</returns>
        /// <exception cref="ArgumentNullException">Thrown when wordstream is null.</exception>
        /// <exception cref="ArgumentException">Thrown when any word in wordstream is null or empty.</exception>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (wordstream == null)
            {
                throw new ArgumentNullException(nameof(wordstream), "Wordstream cannot be null.");
            }

            // Create a list of distinct words from the wordstream
            var wordList = wordstream.Distinct().ToList();

            // Dictionary to count occurrences of each word in the matrix
            var wordCount = new Dictionary<string, int>();

            foreach (var word in wordList)
            {
                if (string.IsNullOrEmpty(word))
                {
                    throw new ArgumentException("Words in the wordstream cannot be null or empty.");
                }

                // Check if word exists in the matrix and update its count
                if (WordExistsInMatrix(word))
                {
                    if (wordCount.ContainsKey(word))
                    {
                        wordCount[word]++;
                    }
                    else
                    {
                        wordCount[word] = 1;
                    }
                }
            }

            // Return the top 10 most repeated words from the wordstream found in the matrix
            return wordCount.OrderByDescending(kvp => kvp.Value)
                            .ThenBy(kvp => kvp.Key)
                            .Take(10)
                            .Select(kvp => kvp.Key)
                            .ToList();
        }

        /// <summary>
        /// Checks if a word exists horizontally or vertically in the matrix starting from a given position.
        /// </summary>
        /// <param name="word">The word to search for.</param>
        /// <returns>True if the word exists in the matrix; false otherwise.</returns>
        private bool WordExistsInMatrix(string word)
        {
            int wordLength = word.Length;

            // Iterate through each position in the matrix
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    // Check horizontally and vertically from the current position
                    if (CheckHorizontal(i, j, word, wordLength) || CheckVertical(i, j, word, wordLength))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a word exists horizontally in the matrix starting from a given position.
        /// </summary>
        /// <param name="row">Starting row index.</param>
        /// <param name="col">Starting column index.</param>
        /// <param name="word">The word to search for.</param>
        /// <param name="wordLength">Length of the word.</param>
        /// <returns>True if the word exists horizontally in the matrix from the given position; false otherwise.</returns>
        private bool CheckHorizontal(int row, int col, string word, int wordLength)
        {
            // Check if word can fit horizontally from the current position
            if (col + wordLength > _cols)
            {
                return false;
            }

            // Check if the characters match horizontally
            for (int i = 0; i < wordLength; i++)
            {
                if (_matrix[row, col + i] != word[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a word exists vertically in the matrix starting from a given position.
        /// </summary>
        /// <param name="row">Starting row index.</param>
        /// <param name="col">Starting column index.</param>
        /// <param name="word">The word to search for.</param>
        /// <param name="wordLength">Length of the word.</param>
        /// <returns>True if the word exists vertically in the matrix from the given position; false otherwise.</returns>
        private bool CheckVertical(int row, int col, string word, int wordLength)
        {
            // Check if word can fit vertically from the current position
            if (row + wordLength > _rows)
            {
                return false;
            }

            // Check if the characters match vertically
            for (int i = 0; i < wordLength; i++)
            {
                if (_matrix[row + i, col] != word[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
