﻿namespace PawnPlus.Core.Classes
{
    enum PawnFlags
    {
        Closeing = 0x1,
        Saved = 0x2,
        FileOpen = 0x4        
    };

    class States
    {
        public States() { }
        public static PawnFlags UIStates;
    }
}