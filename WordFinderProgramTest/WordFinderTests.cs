using System;
using System.Collections.Generic;
using WordFinderProgram;
using Xunit;

namespace WordFinderProgramTest
{
	public class WordFinderTests
	{
		[Fact]
		public void Constructor_NullMatrix_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() => new WordFinder(null));
		}

        [Fact]
        public void Constructor_EmptyMatrix_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new WordFinder(new List<string>()));
        }

        [Fact]
        public void Constructor_MatrixWithDifferentRowLengths_ThrowsArgumentException()
        {
            var matrix = new List<string> { "abc", "abcd", "abc" };
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Fact]
        public void Constructor_MatrixExceedsMaxSize_ThrowsArgumentException()
        {
            var matrix = new List<string>();
            for (int i = 0; i < 65; i++)
            {
                matrix.Add(new string('a', 65));
            }
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Fact]
        public void Find_NullWordstream_ThrowsArgumentNullException()
        {
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordFinder = new WordFinder(matrix);
            Assert.Throws<ArgumentNullException>(() => wordFinder.Find(null));
        }

        [Fact]
        public void Find_EmptyWordstream_ReturnsEmptyList()
        {
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(new List<string>());
            Assert.Empty(result);
        }

        [Fact]
        public void Find_WordstreamWithEmptyWord_ThrowsArgumentException()
        {
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordFinder = new WordFinder(matrix);
            Assert.Throws<ArgumentException>(() => wordFinder.Find(new List<string> { "abc", "" }));
        }

        [Fact]
        public void Find_WordsFoundInMatrix_ReturnsTopWords()
        {
            var matrix = new List<string>
        {
            "testwords",
            "xxxxxxxxa",
            "xxxxxxxxb",
            "xxxxxxxxc",
            "xxxxxxxxd",
            "xxxxxxxxe",
            "xxxxxxxxf",
            "xxxxxxxxg"
        };
            var wordFinder = new WordFinder(matrix);
            var wordstream = new List<string> { "test", "words", "other", "this", "here" };

            var result = wordFinder.Find(wordstream);

            var expected = new List<string> { "test", "words" };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_NoWordsFoundInMatrix_ReturnsEmptyList()
        {
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordFinder = new WordFinder(matrix);
            var wordstream = new List<string> { "xyz", "uvw" };

            var result = wordFinder.Find(wordstream);

            Assert.Empty(result);
        }

        [Fact]
        public void Find_MultipleWordsWithSameCount_ReturnsAlphabetically()
        {
            var matrix = new List<string>
            {
                "abcd",
                "efgh",
                "ijkl",
                "mnop"
            };
            var wordFinder = new WordFinder(matrix);
            var wordstream = new List<string> { "ab", "ei", "ij", "gk", "dh", "gh" };

            var result = wordFinder.Find(wordstream);

            var expected = new List<string> { "ab", "dh", "ei", "gh", "gk", "ij" };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Find_WordsFoundVerticallyInMatrix_ReturnsTopWords()
        {
            var matrix = new List<string>
            {
                "chwxz",
                "hello",
                "aladt",
                "tcomx"
            };
            var wordFinder = new WordFinder(matrix);
            var wordstream = new List<string> { "chat", "hello", "down", "zotx" };

            var result = wordFinder.Find(wordstream);

            var expected = new List<string> { "chat", "hello", "zotx" };
            Assert.Equal(expected, result);
        }
    }
}
