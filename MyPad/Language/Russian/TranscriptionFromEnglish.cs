using System;
using System.Collections.Generic;

namespace Russian
{
    using English;

    // https://ru.wikipedia.org/wiki/Практическая_транскрипция
    public class Transcription
    {
        public English.Letter[] englishSeq;
        public Russian.Letter[] russianSeq; 
        public Transcription(English.Letter[] side2, Russian.Letter[] side1)
        {
            englishSeq = side2;
            russianSeq = side1;
        }
        public Transcription(English.Letter side2, Russian.Letter side1)
        {
            englishSeq = new English.Letter[]{side2};
            russianSeq = new Russian.Letter[]{side1};
        }
        public Transcription(English.Letter side21, English.Letter side22, Russian.Letter side1)
        {
            englishSeq = new English.Letter[]{side21, side22};
            russianSeq = new Russian.Letter[]{side1};
        }
    }

    public class TranscriptionCode
    {
    }

    // Вообще-то транскрипция строится на основе соответствия звуков,
    // где это видно в этом коде? нигде...
    class TranscriptionList
    {
        // https://ru.wikipedia.org/wiki/Англо-русская_практическая_транскрипция
        // Гиляревский, 1985, стр. 60—96; Рыбакин, 2000, стр. 13—20.
        public Transcription[] List = new Transcription[] {
            new Transcription(English.Letter.L00, Russian.Letter.L00),
        };

        public Russian.Letter[] TranscriptToRussian(English.Letter[] letters)
        {
            return new Russian.Letter[]{ };
        }
        public English.Letter[] TranscriptToEnglish(Russian.Letter[] letters)
        {
            return new English.Letter[]{ };
        }
    }
}

