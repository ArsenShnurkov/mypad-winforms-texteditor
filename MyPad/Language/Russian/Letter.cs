using System;
using System.Text;

namespace Russian
{
    // Буква
    // у букв есть произношение и название
    // а у ципр только название
    public class Letter : Character
    {
        /// <summary>
        /// А
        /// </summary>
        static readonly public Letter L00 = new Letter();
        /// <summary>
        /// Б
        /// </summary>
        static readonly public Letter L01 = new Letter();
        /// <summary>
        /// В
        /// </summary>
        static readonly public Letter L02 = new Letter();
        /// <summary>
        /// Г
        /// </summary>
        static readonly public Letter L03 = new Letter();
        /// <summary>
        /// Д
        /// </summary>
        static readonly public Letter L04 = new Letter();
        /// <summary>
        /// Е
        /// </summary>
        static readonly public Letter L05 = new Letter();
        /// <summary>
        /// Ё
        /// </summary>
        static readonly public Letter L06 = new Letter();
        /// <summary>
        /// Ж
        /// </summary>
        static readonly public Letter L07 = new Letter();
        /// <summary>
        /// З
        /// </summary>
        static readonly public Letter L08 = new Letter();
        /// <summary>
        /// И
        /// </summary>
        static readonly public Letter L09 = new Letter();
        /// <summary>
        /// Й
        /// </summary>
        static readonly public Letter L10 = new Letter();
        /// <summary>
        /// К
        /// </summary>
        static readonly public Letter L11 = new Letter();
        /// <summary>
        /// Л
        /// </summary>
        static readonly public Letter L12 = new Letter();
        /// <summary>
        /// М
        /// </summary>
        static readonly public Letter L13 = new Letter();
        /// <summary>
        /// Н
        /// </summary>
        static readonly public Letter L14 = new Letter();
        /// <summary>
        /// О
        /// </summary>
        static readonly public Letter L15 = new Letter();
        /// <summary>
        /// П
        /// </summary>
        static readonly public Letter L16 = new Letter();
        /// <summary>
        /// Р
        /// </summary>
        static readonly public Letter L17 = new Letter();
        /// <summary>
        /// С
        /// </summary>
        static readonly public Letter L18 = new Letter();
        /// <summary>
        /// Т
        /// </summary>
        static readonly public Letter L19 = new Letter();
        /// <summary>
        /// У
        /// </summary>
        static readonly public Letter L20 = new Letter();
        /// <summary>
        /// Ф
        /// </summary>
        static readonly public Letter L21 = new Letter();
        /// <summary>
        /// Х
        /// </summary>
        static readonly public Letter L22 = new Letter();
        /// <summary>
        /// Ц
        /// </summary>
        static readonly public Letter L23 = new Letter();
        /// <summary>
        /// Ч
        /// </summary>
        static readonly public Letter L24 = new Letter();
        /// <summary>
        /// Ш
        /// </summary>
        static readonly public Letter L25 = new Letter();
        /// <summary>
        /// Щ
        /// </summary>
        static readonly public Letter L26 = new Letter();
        /// <summary>
        /// Ъ
        /// </summary>
        static readonly public Letter L27 = new Letter();
        /// <summary>
        /// Ы
        /// </summary>
        static readonly public Letter L28 = new Letter();
        /// <summary>
        /// Ь
        /// </summary>
        static readonly public Letter L29 = new Letter();
        /// <summary>
        /// Э
        /// </summary>
        static readonly public Letter L30 = new Letter();
        /// <summary>
        /// Ю
        /// </summary>
        static readonly public Letter L31 = new Letter();
        /// <summary>
        /// Я
        /// </summary>
        static readonly public Letter L32 = new Letter();

        public Letter ()
        {
        }
        public char GetChar(uint index, Register register)
        {
            switch (RegisterCode.GetIndexOfRegister(register))
            {
            case RegisterCode.Upper:
                return GetLetter_19180101_old_style_2015_base_0_capitalised_register (index);
            case RegisterCode.Lower:
                return GetLetter_19180101_old_style_2015_base_0_lowercase_register (index);
            default:
                throw new IndexOutOfRangeException ("unknown register");
            }
        }
        uint GetCodeOfChar(char c, Encoding e)
        {
            var array = new char[]{ c };
            var buf = e.GetBytes (array);
            uint res = 0;
            foreach (var abyte in buf)
            {
                res = res * 256 + abyte;
            }
            return res;
        }
        // https://ru.wikipedia.org/wiki/Реформа_русской_орфографии_1918_года 
        // А. В. Луначарский = советский Народный комиссар по просвещению
        // Декрет опубликован (без даты) 23 декабря 1917 года (5 января 1918 года),
        // «всем правительственным и государственным изданиям» (среди прочих) предписывалось
        // с 1 января (ст. ст.) 1918 года «печататься согласно новому правописанию»
        char GetLetter_19180101_old_style_2015_base_0_capitalised_register(uint idx)
        {
            switch (idx)
            {
            case LetterCode.Letter00:
                return 'А';
            case LetterCode.Letter01:
                return 'Б';
            case LetterCode.Letter02:
                return 'В';
            case LetterCode.Letter03:
                return 'Г';
            case LetterCode.Letter04:
                return 'Д';
            case LetterCode.Letter05:
                return 'Е';
            case LetterCode.Letter06:
                return 'Ё';
            case LetterCode.Letter07:
                return 'Ж';
            case LetterCode.Letter08:
                return 'З';
            case LetterCode.Letter09:
                return 'И';
            case LetterCode.Letter10:
                return 'Й';
            case LetterCode.Letter11:
                return 'К';
            case LetterCode.Letter12:
                return 'Л';
            case LetterCode.Letter13:
                return 'М';
            case LetterCode.Letter14:
                return 'Н';
            case LetterCode.Letter15:
                return 'О';
            case LetterCode.Letter16:
                return 'П';
            case LetterCode.Letter17:
                return 'Р';
            case LetterCode.Letter18:
                return 'С';
            case LetterCode.Letter19:
                return 'Т';
            case LetterCode.Letter20:
                return 'У';
            case LetterCode.Letter21:
                return 'Ф';
            case LetterCode.Letter22:
                return 'Х';
            case LetterCode.Letter23:
                return 'Ц';
            case LetterCode.Letter24:
                return 'Ч';
            case LetterCode.Letter25:
                return 'Ш';
            case LetterCode.Letter26:
                return 'Щ';
            case LetterCode.Letter27:
                return 'Ъ';
            case LetterCode.Letter28:
                return 'Ы';
            case LetterCode.Letter29:
                return 'Ь';
            case LetterCode.Letter30:
                return 'Э';
            case LetterCode.Letter31:
                return 'Ю';
            case LetterCode.Letter32:
                return 'Я';
            default:
                throw new IndexOutOfRangeException ("index should be in range 0..32");
            }
        }
        char GetLetter_19180101_old_style_2015_base_0_lowercase_register(uint idx)
        {
            switch (idx)
            {
            case LetterCode.Letter00:
                return 'а';
            case LetterCode.Letter01:
                return 'б';
            case LetterCode.Letter02:
                return 'в';
            case LetterCode.Letter03:
                return 'г';
            case LetterCode.Letter04:
                return 'д';
            case LetterCode.Letter05:
                return 'е';
            case LetterCode.Letter06:
                return 'ё';
            case LetterCode.Letter07:
                return 'ж';
            case LetterCode.Letter08:
                return 'з';
            case LetterCode.Letter09:
                return 'и';
            case LetterCode.Letter10:
                return 'й';
            case LetterCode.Letter11:
                return 'к';
            case LetterCode.Letter12:
                return 'л';
            case LetterCode.Letter13:
                return 'м';
            case LetterCode.Letter14:
                return 'н';
            case LetterCode.Letter15:
                return 'о';
            case LetterCode.Letter16:
                return 'п';
            case LetterCode.Letter17:
                return 'р';
            case LetterCode.Letter18:
                return 'с';
            case LetterCode.Letter19:
                return 'т';
            case LetterCode.Letter20:
                return 'у';
            case LetterCode.Letter21:
                return 'ф';
            case LetterCode.Letter22:
                return 'х';
            case LetterCode.Letter23:
                return 'ц';
            case LetterCode.Letter24:
                return 'ч';
            case LetterCode.Letter25:
                return 'ш';
            case LetterCode.Letter26:
                return 'щ';
            case LetterCode.Letter27:
                return 'ъ';
            case LetterCode.Letter28:
                return 'ы';
            case LetterCode.Letter29:
                return 'ь';
            case LetterCode.Letter30:
                return 'э';
            case LetterCode.Letter31:
                return 'ю';
            case LetterCode.Letter32:
                return 'я';
            default:
                throw new IndexOutOfRangeException ("index should be in range 0..32");
            }
        }
    }
    public class LetterCode
    {
        public const uint Letter00 = 0;
        public const uint Letter01 = 1;
        public const uint Letter02 = 2;
        public const uint Letter03 = 3;
        public const uint Letter04 = 4;
        public const uint Letter05 = 5;
        public const uint Letter06 = 6;
        public const uint Letter07 = 7;
        public const uint Letter08 = 8;
        public const uint Letter09 = 9;
        public const uint Letter10 = 10;
        public const uint Letter11 = 11;
        public const uint Letter12 = 12;
        public const uint Letter13 = 13;
        public const uint Letter14 = 14;
        public const uint Letter15 = 15;
        public const uint Letter16 = 16;
        public const uint Letter17 = 17;
        public const uint Letter18 = 18;
        public const uint Letter19 = 19;
        public const uint Letter20 = 20;
        public const uint Letter21 = 21;
        public const uint Letter22 = 22;
        public const uint Letter23 = 23;
        public const uint Letter24 = 24;
        public const uint Letter25 = 25;
        public const uint Letter26 = 26;
        public const uint Letter27 = 27;
        public const uint Letter28 = 28;
        public const uint Letter29 = 29;
        public const uint Letter30 = 30;
        public const uint Letter31 = 31;
        public const uint Letter32 = 32;
    }
}

