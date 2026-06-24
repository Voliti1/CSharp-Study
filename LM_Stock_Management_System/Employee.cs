using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM_Stock_Management_System
{
    internal class Employee : Person
    {
        public string BirthDate { get; set; } 
        public string Duty { get; set; }
        public string EmployeeID { get; set; }
        public string IsWorking { get; set; }
        public string Position { get; set; }

        public void GoToWork()
        {
            IsWorking = "출근";
            Console.WriteLine($"{EmployeeID} {Name} 직원이 {IsWorking}하였습니다.");  
        }

        public void LeaveWork()
        {
            IsWorking = "퇴근";
            Console.WriteLine($"{EmployeeID} {Name} 직원이 {IsWorking}하였습니다.");
        }

        public void ManageStock()
        {
            Duty = "재고관리중";
            Console.WriteLine($"{EmployeeID} {Name} 직원이 {Duty}입니다.");
            
        }

        public void CheckOut()
        {
            Duty = "계산대에서 일하는 중";
            Console.WriteLine($"{EmployeeID} {Name} 직원이 {Duty}입니다.");
        }

        public void Cleaning()
        {
            Duty = "청소중";
            Console.WriteLine($"{EmployeeID} {Name} 직원이 {Duty}입니다.");
        }

        public void DisposalProcessing()
        {
            Duty = "폐기처리중";
            Console.WriteLine($"{EmployeeID} {Name} 직원이 {Duty}입니다.");
        }
        


    }
}
