using System;

namespace Russian
{
    public class Register
    {
        static readonly public Register Lower = new Register();
        static readonly public Register Upper = new Register();
        public Register ()
        {
        }
    }
    class RegisterCode
    {
        public const uint Lower = 0;
        public const uint Upper = 1;
        static public Register GetRegister(uint index)
        {
            switch (index)
            {
            case RegisterCode.Lower:
                return Register.Lower;
            case RegisterCode.Upper:
                return Register.Upper;
            default:
                throw new ArgumentException ("index should be 0..1");
            }
        }
        static public uint GetIndexOfRegister(Register r)
        {
            if (r == Register.Lower)
            {
                return RegisterCode.Lower;
            }
            if (r == Register.Upper)
            {
                return RegisterCode.Upper;
            }
            throw new ArgumentException ("unknown register");
        }
    }
}

