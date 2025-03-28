﻿namespace Game.Scripts.Interfaces
{
    public interface IActivable
    {
        public bool IsActiveState { get; }

        public bool SetTrueActiveState();
        public bool SetFalseActiveState();
    }
}