﻿namespace Proyecto_Arqui.Classes.Moesi
{
    public sealed class MoesiMemory
    {
        private readonly static MoesiMemory _instance = new MoesiMemory();
        public int[] memory;
        private MoesiMemory()
        {
            memory = new int[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        }

        public static MoesiMemory Instance
        {
            get { return _instance; }
        }

        public int get_from_address(int address)
        {
            return memory[address];
        }
        public void write_to_address(int address, int value)
        {
            memory[address] = value;
        }
    }
}
