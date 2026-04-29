using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM_Stock_Management_System
{
    internal class Employee
    {
        public string Employee_id { get; set; }
        public string Employee_name { get; set; }
        public string Employee_resident_number { get; set; }// 주민번호
        public string Employee_gender { get; set; }
        public string Employee_address { get; set; }
        public string Employee_phone_number { get; set; }
        public string Employee_rank { get; set; } //직급
        public string Employee_birth { get; set; }
        public string Employee_account { get; set; }
        public string Employee_duty { get; set; } //현재 직무
        public string Employee_status { get; set; } //출퇴근여부

        public void GoToWork()
        {
            Employee_status = "출근";
            Console.WriteLine($"{Employee_id} {Employee_name} 직원이 {Employee_status}하였습니다.");  
        }

        public void LeaveWork()
        {
            Employee_status = "퇴근";
            Console.WriteLine($"{Employee_id} {Employee_name} 직원이 {Employee_status}하였습니다.");
        }

        public void ManageStock()
        {
            Employee_duty = "재고관리중";
            Console.WriteLine($"{Employee_id} {Employee_name} 직원이 {Employee_duty}입니다.");
            
        }

        public void CheckOut()
        {
            Employee_duty = "계산대에서 일하는 중";
            Console.WriteLine($"{Employee_id} {Employee_name} 직원이 {Employee_duty}입니다.");
        }

        public void Cleaning()
        {
            Employee_duty = "청소중";
            Console.WriteLine($"{Employee_id} {Employee_name} 직원이 {Employee_duty}입니다.");
        }

        public void DisposalProcessing()
        {
            Employee_duty = "폐기처리중";
            Console.WriteLine($"{Employee_id} {Employee_name} 직원이 {Employee_duty}입니다.");
        }
        


    }
}
