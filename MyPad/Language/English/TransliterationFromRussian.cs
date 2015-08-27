using System;
using System.Collections.Generic;

namespace English
{
    using Russian;

    // Необходимость в транслитерации возникла в конце XIX в. при создании прусских научных библиотек для включения в единый каталог работ,
    // написанных на языках с латинской, кириллической, арабской, индийскими и другими системами письма. Инструкции по транслитерации, 
    // составленные для нужд этих библиотек, послужили в XX в. основой стандарта для перевода нелатинских систем письма на латиницу

    // https://ru.wikipedia.org/wiki/Практическая_транскрипция
    // https://ru.wikipedia.org/wiki/Транслитерация 
    // В противоположность транскрипции, предназначаемой для максимально точной передачи звуков языка, 
    // транслитерация касается письменной формы языка: текст, написанный на том или ином алфавите, передаётся алфавитом другой какой-либо системы.
    // При этом обычно принимается во внимание только соответствие букв двух алфавитов, а звуки, скрывающиеся за ними, не учитываются.
    // Транслитерация применяется преимущественно по отношению к мёртвым языкам, как санскрит, древнеперсидский и др.
    //
    // https://en.wikipedia.org/wiki/Orthographic_transcription
    // transcription method that employs the standard spelling system of each target language
    // Standard transcription schemes for linguistic purposes include the International Phonetic Alphabet (IPA), and its ASCII equivalent, SAMPA.
    //
    // Official transliteration ISO 9 (GOST 7.79-2000)
    //
    // After transcribing a word from one language to the script of another language:
    //
    // - one or both languages may develop further. The original correspondence between the sounds of the two languages may change,
    //   and so the pronunciation of the transcribed word develops in a different direction than the original pronunciation.
    // - the transcribed word may be adopted as a loan word in another language with the same script.
    // This often leads to a different pronunciation and spelling than a direct transcription.
    //
    // https://en.wikipedia.org/wiki/English_phonetic_alphabet
    public class Transliteration
    {
        public English.Letter[] englishSeq;
        public Russian.Letter[] russianSeq; 
        public Transliteration(English.Letter[] side2, Russian.Letter[] side1)
        {
            englishSeq = side2;
            russianSeq = side1;
        }
        public Transliteration(English.Letter side2, Russian.Letter side1)
        {
            englishSeq = new English.Letter[]{side2};
            russianSeq = new Russian.Letter[]{side1};
        }
        public Transliteration(English.Letter side21, English.Letter side22, Russian.Letter side1)
        {
            englishSeq = new English.Letter[]{side21, side22};
            russianSeq = new Russian.Letter[]{side1};
        }
    }

    // Вообще-то транскрипция строится на основе соответствия звуков,
    // где это видно в этом коде? нигде...
    class TransliterationList
    {
        // https://ru.wikipedia.org/wiki/Англо-русская_практическая_транскрипция
        // Гиляревский, 1985, стр. 60—96; Рыбакин, 2000, стр. 13—20.
        public Transliteration[] List = new Transliteration[] {
            new Transliteration(English.Letter.L00, Russian.Letter.L00),
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

/* транслитерация
A       A       Andrey (Андрей)         
Б       B       Boris (Борис)   
В       V       Valery (Валерий)        
Г       G       Gleb (Глеб)     
Д       D       Dmitry (Дмитрий)        
Е       Ye/E    Yelena, Elena (Елена)   
Ё       Yo/E    Pyotr, Petr (Петр)      
Ж       Zh      Zhanna (Жанна)  
З       Z       Zinaida (Зинаида)       
И       I       Irina (Ирина)   
Й       Y       Timofey (Тимофей)       
K       K       Konstantin (Константин)         
Л       L       Larisa (Лариса)         
М       М       Margarita (Маргарита)   
Н       N       Nikolay (Николай)       
О       О       Olga (Ольга)
П       P       Pavel (Павел)
Р       R       Roman (Роман)
С       S       Sergey (Сергей)
T       T       Tatyana (Татьяна)
У       U       Ulyana (Ульяна)
Ф       F       Filipp (Филипп)
Х       Kh      Khariton (Харитон)
Ц       Ts      Tsarev (Царев)
Ч       Ch      Chaykin (Чайкин)
Ш       Sh      Sharov (Шаров)
Щ       Shch    Shchepkin (Щепкин)
Ы       Y       Myskin (Мыскин)
Э       E       Eldar (Эльдар)
Ю       Yu      Yury (Юрий)
Я       Ya      Yaroslav (Ярослав)
*/