﻿using System.Diagnostics;

namespace Proyecto_Arqui.Classes
{
    public class CPU
    {
        private int[] list_register;
        public Cache cache;
        public int id;
        public string instruction_to_execute;
        public CPU(int _id)
        {
            list_register = new int[8];
            cache = new Cache();
            this.id = _id;

            Thread thread = new Thread(new ThreadStart(check_interconnect));
            thread.Start();
        }
        public void write(int address, int reg)
        {
            cache.write_to_address(address, list_register[reg], this.id);
            Console.WriteLine($"Data is in register:{reg}, written to address: {address}\n");
            Debug.WriteLine($"Data is in register:{reg}, written to address: {address}\n");
        }
        public void read(int address, int reg)
        {
            list_register[reg] = cache.get_from_address(address, this.id)[2];
            Console.WriteLine($"Value of data is {list_register[reg]}\n");
            Debug.WriteLine($"Value of data is {list_register[reg]}\n");
        }
        public void increment_reg(int reg)
        {
            list_register[reg] ++;
        }
        public void execute_inst(string inst)
        {
            string[] inst_parsed = inst.Split(' ');
            string instruction_to_execute = inst_parsed[0];
            int reg_to_exec = Int16.Parse(inst_parsed[1]);
            int address_to_exec = Int16.Parse(inst_parsed[2]);
            if (string.Compare(instruction_to_execute, "read") == 0)
            {
                read(address_to_exec, reg_to_exec);
            }else if (string.Compare(instruction_to_execute, "write") == 0)
            {
                write(address_to_exec, reg_to_exec);
            }
            else if (string.Compare(instruction_to_execute, "increment") == 0)
            {
                increment_reg(reg_to_exec);
            }
        }

        private void check_interconnect()
        {
            while (true)
            {
                if(instruction_to_execute != null && Interconnect.Instance.inst_active == false && Interconnect.Instance.active_cpu == -1)
                {
                    Debug.WriteLine($"CPU:{id}, blocking Interconnect and executing");
                    Console.WriteLine($"CPU:{id}, blocking Interconnect and executing");
                    Interconnect.Instance.inst_active = true;
                    Interconnect.Instance.active_cpu = id;
                    execute_inst(instruction_to_execute);
                    instruction_to_execute = null;
                    Interconnect.Instance.active_cpu = -1;
                    Interconnect.Instance.inst_active = false;
                }
                else
                {
                    Debug.WriteLine($"CPU:{id}, sleeping");
                    Console.WriteLine($"CPU:{id}, sleeping");
                    TimeSpan interval = new TimeSpan(0, 0, 2);
                    Thread.Sleep(interval);
                }
            }

        }

    }
}
