namespace PawnPlus.Core.Classes
{
    enum PawnFlags
    {
        Saved = 0x1
    };


    class States
    {
        public States()
        {

        }

        public static PawnFlags UIStates
        {
            get; set;
        }
        
    }
}
